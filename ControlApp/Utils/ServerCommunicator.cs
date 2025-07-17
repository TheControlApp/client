using ControlApp.Exceptions;
using ControlApp.Exceptions.LoginExceptions;
using ControlApp.Exceptions.RegisterExceptions;
using ControlApp.Models;
using ControlApp.Subroutines;
using FluentFTP;
using HtmlAgilityPack;
using System.CodeDom;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ControlApp;

public abstract class ServerCommunicator : HttpClient {
    private const string URL_AUTH_REGISTER = "auth/register";
    private const string URL_AUTH_LOGIN = "auth/login";
    private const string MEDIA_TYPE_APP_JSON = "application/json";
    private static readonly HttpClient _httpClient = new HttpClient();
	private static readonly FtpClient _ftpClient = new FtpClient("ftp://home240474283.1and1-data.host/", "acc929431981", Utils.Decrypt("6scM67YJ+Ezzz0RKCeIxbT9TAfSbRE++1T"));

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
    public static async Task<string> LoginAndGetTokenAsync(Login loginModel)
    {
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
            Utils.LogError("Error during login: " + ex.Message);
			throw;
        }
		// process answer
        if (response.IsSuccessStatusCode)
        {
            // Assuming the backend returns the token in the response body
            return await response.Content.ReadAsStringAsync();
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
            Utils.LogError("Error during register: " + ex.Message);
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

	private static async Task<HtmlNode?> GetCommand(string command)
	{
		if (_httpClient.Timeout != TimeSpan.FromMilliseconds(1000)) _httpClient.Timeout = TimeSpan.FromMilliseconds(1000);
		string url = ConfigurationManager.AppSettings["SiteUrl"] + $"AppCommand.aspx?vrs=012&cmd={command}";
		Utils.LogInfo("Getting response from URL: " + url);
		var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
        // Add the token to the request header
        string token = SecureTokenStorage.ReadToken();
        if (string.IsNullOrEmpty(token)) {
            Utils.LogError("No token found for GetCommand.");
            return null;
        }
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

		HttpResponseMessage message;
		try {
			message = await _httpClient.SendAsync(requestMessage);
		} catch (Exception ex) {
			Utils.LogWarning("Error during command: \"" + command + "\"" + ex.Message);
			return null;
		}
		if (!message.IsSuccessStatusCode) {
			Utils.LogError($"Server returned error code {message.StatusCode} during command {command}");
			return null;
		}
		HtmlDocument document = new();
		Utils.LogInfo("Server successfully replied");
		document.LoadHtml(await message.Content.ReadAsStringAsync());
		return document.DocumentNode.SelectSingleNode("//body/form/div[not(@class)]");
	}


	private static string? GetChildWithId(HtmlNode? node, string nodeId) {
		if (node == null) {
			Utils.LogError("Got null as argument, returning null...");
			return null;
		}
		HtmlNode? selectedNode = node.SelectSingleNode($"//span[@id=\"{nodeId}\"]");
		if (selectedNode != null) return selectedNode.InnerText;
		Utils.LogError($"No \"{nodeId}\" node found for {node}");
		return null;
	}
	
	public static bool SendFtpFile(string fileName) {
		CustomMessage message = new CustomMessage("Sending file to server", "", 0, false);
		message.Show();
		try {
			string justfilename = Path.GetFileName(fileName);
			_ftpClient.UploadFile(fileName, justfilename);
		} catch (Exception ex) {
			MessageBox.Show("Sending file to server failed", "Send Failed");
			Utils.LogWarning("Error during FTP: " + ex.Message);
			return false;
		}
		message.Dispose();
		return true;
	}

	public static async Task<string[]?> GetOutstanding() {
		string? result = GetChildWithId(await GetCommand("Outstanding"), "result");
		return result == null ? null : Utils.SeparateArrayString(result);
	}

	public static async Task<bool> DeleteOutstanding() {
		return await GetCommand("Delete") != null;
	}

	public static async Task<bool> SendCommand(string destUser, string command, bool groupSend)
    {
        // Remove fromuser and frompword from URL, rely on token.
        // The backend will identify the "from" user via the token.
        byte allint = Convert.ToByte(groupSend);
        string url = ConfigurationManager.AppSettings["SiteUrl"] + $"AppSendContent.aspx?UserNm={destUser}&comm={command}&all={allint}";
        Utils.LogInfo("Getting response from: " + url);
        try
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            string token = SecureTokenStorage.ReadToken();
            if (string.IsNullOrEmpty(token)) return false;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await _httpClient.SendAsync(requestMessage);
        }
        catch (HttpRequestException e)
        {
            Utils.LogError("Could not get a response from server: " + e.Message);
            return false;
        }
        catch (Exception e)
        {
            Utils.LogError("Error while sending command: " + e.Message);
            return false;
        }
        Utils.LogInfo("Server successfully replied");
        return true;
    }

	public static bool SendBlockReport(string senderid, string command, string report) {
		string url = ConfigurationManager.AppSettings["SiteUrl"] + $"BlockReport.aspx?vrs=012&sender={senderid}&report={report}&content={command}";
        Utils.LogInfo("Blocking user : " + url);
		
		try
		{
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            string token = SecureTokenStorage.ReadToken();
            if (string.IsNullOrEmpty(token)) return false;
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			_httpClient.Send(requestMessage);
		}
		catch (HttpRequestException e)
		{
			Utils.LogError("Could not get a response from server: " + e.Message);
			return false;
		}
		catch (Exception e)
		{
			Utils.LogError("Error while sending report: " + e.Message);
			return false;
		}
		return true;
	}

	public static async Task<string[]?> GetLatestItem() {
		HtmlNode? node = await GetCommand("Content");
		if (node == null) return null;
		string? childNode = GetChildWithId(node, "result");
		if (childNode == null) return null;
		string[] output = Utils.SeparateArrayString(childNode);
		return output;
	}

	/*	Keeping this piece of code here for archival purposes. Who knows, maybe it'll be useful someday.
	public static string[] GetRelations() {
		string result = GetChildWithId(GetCommand("Relations"), "result");
		return result == null ? null : Utils.SeparateArrayString(result);
	}

	public static bool AcceptInvite(string dom) {
		return GetCommand("Accept" + dom) != null;
	}

	public static bool RejectInvite(string dom) {
		return GetCommand("Reject" + dom) != null;
	}

	public static string[] getInvites() {
		string result = GetChildWithId(GetCommand("Invite"), "result");
		return result == null ? null : Utils.SeparateArrayString(result);
	}
	*/

	public static bool ThumbsUp(string user) {
		return GetCommand("Thumbs" + user) != null;
	}

	public static string? GetFile(string url) {
		Utils.LogInfo("Getting file " + url);
		string filename = url.Substring(url.LastIndexOf('/') + 1);
		Utils.LogInfo("File name: " + filename);
		if (!Utils.IsFile(filename)) {
			Utils.LogInfo($"{filename} is not a file, returning null");
			return null;
		}
		string filePath = Path.Join(ConfigurationManager.AppSettings["LocalDrive"], filename);
		using CustomMessage message = new CustomMessage("Downloading image, please wait", "", 0, false);
		message.Show();
		try {
			using Task<Stream> streamTask = _httpClient.GetStreamAsync(url);
			Stream stream = streamTask.Result;
			using FileStream fs = new FileStream(filePath, FileMode.Create);
			stream.CopyTo(fs);
		}
		catch (Exception ex) {
			Utils.LogWarning("Error getting file for \"" + url + "\":" + ex.Message);
			return null;
		}
		message.Hide();
		Utils.LogInfo("Successfully got file from " + url);
		return filePath;
	}
}