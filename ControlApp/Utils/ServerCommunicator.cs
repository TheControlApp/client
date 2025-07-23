using ControlApp.Exceptions;
using ControlApp.Exceptions.LoginExceptions;
using ControlApp.Exceptions.RegisterExceptions;
using ControlApp.Models;
using ControlApp.Services;
using ControlApp.Subroutines;
using FluentFTP;
using HtmlAgilityPack;
using System.CodeDom;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ControlApp.Utils;

public abstract class ServerCommunicator : HttpClient {
    private const string URL_AUTH_REGISTER = "auth/register";
    private const string URL_AUTH_LOGIN = "auth/login";
    private const string MEDIA_TYPE_APP_JSON = "application/json";
    private const string URL_API_CONFIG = "api/server-config";
    private static readonly HttpClient _httpClient = new HttpClient();
	private static readonly FtpClient _ftpClient = new FtpClient("ftp://home240474283.1and1-data.host/", "acc929431981", "6scM67YJ+Ezzz0RKCeIxbT9TAfSbRE++1T");

    /// <summary>
    /// Asynchronously attempts to log in a user and retrieve an authentication token.
    /// </summary>
    /// <remarks>
    /// This method sends the provided login credentials to the authentication endpoint specified in the application's configuration. It handles several specific HTTP status codes to provide detailed exception information for different failure scenarios.
    /// </remarks>
    /// <param name="loginModel">A <c>Login</c> object containing the user's credentials, such as username and password.</param>
    /// <returns>
    /// A <c>Task<string></c> that represents the asynchronous operation. Upon successful authentication, the task's result is the authentication token returned by the server.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown when an error occurs during the HTTP POST request. The error is logged before the exception is re-thrown.
    /// </exception>
    /// <exception cref="WrongLoginOrPaswordException">
    /// Thrown if the server responds with a 400 (Bad Request) or 403 (Forbidden) status code, indicating incorrect login credentials or a banned user.
    /// </exception>
    /// <exception cref="UpgradeAppVerionException">
    /// Thrown if the server responds with a 426 (Upgrade Required) status code, indicating that the client application version is outdated.
    /// </exception>
    /// <exception cref="UnknownLoginException">
    /// Thrown if the server returns an unsuccessful status code that is not one of the specifically handled cases (400, 403, 426).
    /// </exception>
    public static async Task<LoginResponse> LoginAndGetTokenAsync(Login loginModel)
    {
        //return null;
        HttpResponseMessage response;
        // fetch token
        try
        {
            string loginUrl = ConfigurationManager.AppSettings["SiteUrl"] + URL_AUTH_LOGIN;
            string jsonContent = JsonSerializer.Serialize(loginModel);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, MEDIA_TYPE_APP_JSON);

            response = await _httpClient.PostAsync(loginUrl, httpContent);
        }
        catch (Exception ex)
        {
            Utilities.LogError("Error during login: " + ex.Message);
            throw;
        }
        // process answer
        if (response.IsSuccessStatusCode)
        {
            // Assuming the backend returns the token in the response body
            return JsonSerializer.Deserialize<LoginResponse>(await response.Content.ReadAsStringAsync())!;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden) // user is banned
        {
            throw new WrongLoginOrPaswordException();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UpgradeRequired) // user is on an older version of the app
        {
            throw new UpgradeAppVerionException();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) // wrong login or password
        {
            throw new WrongLoginOrPaswordException();
        }
        throw new UnknownLoginException();
    }

    /// <summary>
    /// Validates the stored token with the server by fetching the associated user account.
    /// This is the primary method for session validation on application startup.
    /// </summary>
    /// <returns>The UserAccount if the token is valid and the request succeeds; otherwise, null.</returns>
    public static async Task<UserAccount> ValidateTokenAndGetAccountAsync()
    {
        string token = SecureTokenStorage.ReadToken();
        if (string.IsNullOrEmpty(token))
        {
            // No token stored, so no session to validate.
            return null;
        }

        try
        {
            // 1. Create a new HttpRequestMessage.
            //    Using a specific endpoint for fetching the current user's data is standard practice.
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "api/account/me");

            // 2. Add the stored token to the Authorization header.
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Utilities.LogInfo("Validating stored token with the server...");

            // 3. Send the request to the server.
            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            // 4. Handle the server's response.
            if (response.IsSuccessStatusCode)
            {
                // Status 200 OK: The token is valid.
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response directly into our UserAccount model.
                UserAccount account = JsonSerializer.Deserialize<UserAccount>(jsonResponse,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                Utilities.LogInfo($"Token validated successfully for user '{account?.Username}'.");
                return account;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                // Status 401 Unauthorized or 403 Forbidden: The token is invalid, expired, or revoked.
                Utilities.LogWarning($"Token validation failed with status {response.StatusCode}. The stored token is invalid or expired.");
                return null;
            }
            else
            {
                // Handle other potential server errors (500, 404, etc.)
                Utilities.LogError($"Token validation request failed with an unexpected status code: {response.StatusCode}");
                return null;
            }
        }
        catch (HttpRequestException ex)
        {
            // This catches network-related errors (e.g., no internet connection, DNS failure).
            Utilities.LogError($"A network error occurred during token validation: {ex.Message}");
            return null;
        }
        catch (JsonException ex)
        {
            // This catches errors if the server returns malformed JSON.
            Utilities.LogError($"Failed to parse the user account response from the server: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            // Catch-all for any other unexpected errors.
            Utilities.LogError($"An unexpected error occurred during token validation: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Fetches the complete server-side configuration, including banned words and sites.
    /// This method performs a single authenticated API call.
    /// </summary>
    /// <returns>A ServerConfig object containing the fetched lists.</returns>
    /// <exception cref="AuthenticationException">Thrown if no valid token is available.</exception>
    /// <exception cref="NetworkException">Thrown on network-level failures.</exception>
    /// <exception cref="ServerApiException">Thrown on non-success HTTP status codes from the server.</exception>
    /// <exception cref="ApiResponseParseException">Thrown if the server's JSON response is malformed.</exception>
    public static async Task<ServerConfig> FetchServerConfigurationAsync()
    {
        Utilities.LogInfo($"Fetching server configuration from endpoint: {URL_API_CONFIG}");

        // La méthode GetAsync gère déjà l'authentification et les erreurs réseau/serveur de base.
        HttpResponseMessage response = await _httpClient.GetAsync(ConfigurationManager.AppSettings["SiteUrl"] + URL_API_CONFIG);

        if (response.IsSuccessStatusCode)
        {
            string jsonResponse = await response.Content.ReadAsStringAsync();
            try
            {
                var config = JsonSerializer.Deserialize<ServerConfig>(jsonResponse,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // S'assure que les listes ne sont jamais nulles, même si le JSON est incomplet.
                config.BannedWords ??= new List<string>();
                config.BannedSites ??= new List<string>();

                return config;
            }
            catch (JsonException ex)
            {
                // Le JSON est invalide, ce qui est une erreur critique.
                throw new ApiResponseParseException("Failed to parse the server configuration from a successful response.");
            }
        }
        else
        {
            // GetAsync ne lance pas d'exception pour les codes d'erreur HTTP, nous le faisons ici.
            string errorBody = await response.Content.ReadAsStringAsync();
            throw new ServerApiException($"The server returned an unexpected error while fetching configuration: {response.ReasonPhrase}");
        }
    }
    /// <summary>
    /// Asynchronously attempts to register a new user.
    /// </summary>
    /// <remarks>
    /// This method sends the new user's details to the registration endpoint specified in the application's configuration. It handles several specific HTTP status codes to provide detailed exception information for different failure scenarios, including validation errors and conflicts.
    /// </remarks>
    /// <param name="registerModel">A <c>Register</c> object containing the new user's information, such as username, password, and email.</param>
    /// <returns>
    /// A <c>Task<bool></c> that represents the asynchronous operation. Upon successful registration, the task's result is a boolean value deserialized from the server's response.
    /// </returns>
    /// <exception cref="Exception">
    /// Thrown when an error occurs during the HTTP POST request. The error is logged before the exception is re-thrown.
    /// </exception>
    /// <exception cref="UserNameOrEmailAlreadyInUseException">
    /// Thrown if the server responds with a 409 (Conflict) status code, indicating the chosen username or email is already registered.
    /// </exception>
    /// <exception cref="UpgradeAppVerionException">
    /// Thrown if the server responds with a 426 (Upgrade Required) status code, indicating the client application version is outdated.
    /// </exception>
    /// <exception cref="BadEmailException">
    /// Thrown if the server responds with a 400 (Bad Request) status code and the response body is "bad-email", indicating an invalid email format.
    /// </exception>
    /// <exception cref="BadPasswordException">
    /// Thrown if the server responds with a 400 (Bad Request) status code and the response body is "bad-password", indicating the password does not meet security requirements.
    /// </exception>
    /// <exception cref="UnauthorizedUserNameException">
    /// Thrown if the server responds with a 400 (Bad Request) status code and the response body is "bad-username", indicating the username is invalid or disallowed.
    /// </exception>
    /// <exception cref="UnauthorizedScreenNameException">
    /// Thrown if the server responds with a 400 (Bad Request) status code and the response body is "bad-displayname", indicating the screen name is invalid or disallowed.
    /// </exception>
    /// <exception cref="UnknownRegisterException">
    /// Thrown if the server returns an unsuccessful status code that is not one of the specifically handled cases.
    /// </exception>
    public static async Task<bool> RegisterAsync(Register registerModel)
	{
		HttpResponseMessage response;
        try
        {
            string loginUrl = ConfigurationManager.AppSettings["SiteUrl"] + URL_AUTH_REGISTER;
            string jsonContent = JsonSerializer.Serialize(registerModel);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, MEDIA_TYPE_APP_JSON);

            response = await _httpClient.PostAsync(loginUrl, httpContent);

            
        }
        catch (Exception ex)
        {
            Utilities.LogError("Error during register: " + ex.Message);
			throw;
        }
        if (response.IsSuccessStatusCode)
        {
            // Assuming the backend returns a boolean in the response body
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Conflict) // username or email is already used
        {
            throw new UserNameOrEmailAlreadyInUseException();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.UpgradeRequired) // user is on an older version of the app
        {
            throw new UpgradeAppVerionException();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) // wrong or bad data provided by user
        {
			switch(await response.Content.ReadAsStringAsync())
			{
				case "bad-email":
					throw new BadEmailException();
				case "bad-password":
                    throw new BadPasswordException();
				case "bad-username":
					throw new UnauthorizedUserNameException();
				case "bad-displayname":
					throw new UnauthorizedScreenNameException();
            }
        }
        throw new UnknownRegisterException();
    }
	
	public static bool SendFtpFile(string fileName) {
		CustomMessage message = new CustomMessage("Sending file to server", "", 0, false);
		message.Show();
		try {
			string justfilename = Path.GetFileName(fileName);
			_ftpClient.UploadFile(fileName, justfilename);
		} catch (Exception ex) {
			MessageBox.Show("Sending file to server failed", "Send Failed");
			Utilities.LogWarning("Error during FTP: " + ex.Message);
			return false;
		}
		message.Dispose();
		return true;
	}

    /// <summary>
    /// Sends a request to the server to block a specific user.
    /// </summary>
    /// <param name="senderIdToBlock">The username of the user to block.</param>
    /// <returns>True if the server acknowledged the block request successfully; otherwise, false.</returns>
    public static async Task<bool> BlockSenderAsync(string senderIdToBlock)
    {
        if (string.IsNullOrEmpty(senderIdToBlock))
        {
            Utilities.LogError("BlockSenderAsync called with no senderId.");
            return false;
        }

        try
        {
            // This assumes a RESTful API endpoint like: POST /api/user/block
            // The user to block is sent in the request body for clarity and security.
            var endpoint = "api/user/block";
            var content = new { username = senderIdToBlock }; // Anonymous object for the JSON body

            string jsonContent = JsonSerializer.Serialize(content);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Create and send an authenticated POST request
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["SiteUrl"] + "api/user/block");
            string token = SecureTokenStorage.ReadToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new AuthenticationException("Cannot block sender: No token available.");
            }
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = httpContent;

            Utilities.LogInfo($"Sending block request for user: {senderIdToBlock}");
            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                Utilities.LogInfo($"Successfully blocked user: {senderIdToBlock}");
                return true;
            }
            else
            {
                // Throw an exception with details from the server response
                string errorBody = await response.Content.ReadAsStringAsync();
                throw new ServerApiException($"Server failed to block user '{senderIdToBlock}'.");
            }
        }
        catch (Exception ex)
        {
            // Catch any exception (Authentication, Network, ServerApi) and log it.
            Utilities.LogError($"An error occurred while trying to block sender '{senderIdToBlock}': {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Sends a report against a specific user to the server.
    /// </summary>
    /// <param name="senderIdToReport">The username of the user being reported.</param>
    /// <param name="commandPayloadJson">The JSON of the command payload that is being reported, for context.</param>
    /// <returns>True if the server acknowledged the report successfully; otherwise, false.</returns>
    public static async Task<bool> ReportSenderAsync(string senderIdToReport, string commandPayloadJson)
    {
        if (string.IsNullOrEmpty(senderIdToReport))
        {
            Utilities.LogError("ReportSenderAsync called with no senderId.");
            return false;
        }

        try
        {
            // Assumes a RESTful API endpoint like: POST /api/user/report
            var endpoint = "api/user/report";
            var content = new
            {
                username = senderIdToReport,
                // Including the payload gives administrators full context for the report.
                commandPayload = commandPayloadJson
            };

            string jsonContent = JsonSerializer.Serialize(content);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);
            string token = SecureTokenStorage.ReadToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new AuthenticationException("Cannot report sender: No token available.");
            }
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = httpContent;

            Utilities.LogInfo($"Sending report for user: {senderIdToReport}");
            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                Utilities.LogInfo($"Successfully reported user: {senderIdToReport}");
                return true;
            }
            else
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                throw new ServerApiException($"Server failed to process report for user '{senderIdToReport}'.");
            }
        }
        catch (Exception ex)
        {
            Utilities.LogError($"An error occurred while trying to report sender '{senderIdToReport}': {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Sends a "like" or "thumbs up" action to the server for a specific user.
    /// </summary>
    /// <param name="senderIdToLike">The username of the user to "like".</param>
    /// <returns>True if the server acknowledged the request successfully; otherwise, false.</returns>
    public static async Task<bool> LikeSenderAsync(string senderIdToLike)
    {
        if (string.IsNullOrEmpty(senderIdToLike))
        {
            Utilities.LogError("LikeSenderAsync called with no senderId.");
            return false;
        }

        try
        {
            // Assumes a RESTful API endpoint like: POST /api/user/like
            var endpoint = "api/user/like";
            var content = new { username = senderIdToLike };

            string jsonContent = JsonSerializer.Serialize(content);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, endpoint);
            string token = SecureTokenStorage.ReadToken();
            if (string.IsNullOrEmpty(token))
            {
                throw new AuthenticationException("Cannot like sender: No token available.");
            }
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = httpContent;

            Utilities.LogInfo($"Sending 'like' request for user: {senderIdToLike}");
            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                Utilities.LogInfo($"Successfully 'liked' user: {senderIdToLike}");
                return true;
            }
            else
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                throw new ServerApiException($"Server failed to process 'like' for user '{senderIdToLike}'.");
            }
        }
        catch (Exception ex)
        {
            Utilities.LogError($"An error occurred while trying to like sender '{senderIdToLike}': {ex.Message}");
            return false;
        }
    }

	public static string? GetFile(string url) {
		Utilities.LogInfo("Getting file " + url);
		string filename = url.Substring(url.LastIndexOf('/') + 1);
		Utilities.LogInfo("File name: " + filename);
		if (!Utilities.IsFile(filename)) {
			Utilities.LogInfo($"{filename} is not a file, returning null");
			return null;
		}
		string filePath = Path.Join(ConfigurationService.CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath, filename);
		using CustomMessage message = new CustomMessage("Downloading image, please wait", "", 0, false);
		message.Show();
		try {
			using Task<Stream> streamTask = _httpClient.GetStreamAsync(url);
			Stream stream = streamTask.Result;
			using FileStream fs = new FileStream(filePath, FileMode.Create);
			stream.CopyTo(fs);
		}
		catch (Exception ex) {
			Utilities.LogWarning("Error getting file for \"" + url + "\":" + ex.Message);
			return null;
		}
		message.Hide();
		Utilities.LogInfo("Successfully got file from " + url);
		return filePath;
	}
}