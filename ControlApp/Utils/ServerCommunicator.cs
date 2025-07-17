using System.Configuration;
using FluentFTP;
using HtmlAgilityPack;
using ControlApp.Subroutines;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using ControlApp.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ControlApp;

public abstract class ServerCommunicator : HttpClient {
    private static readonly HttpClient _httpClient = new HttpClient();
	private static readonly FtpClient _ftpClient = new FtpClient("ftp://home240474283.1and1-data.host/", "acc929431981", Utils.Decrypt("6scM67YJ+Ezzz0RKCeIxbT9TAfSbRE++1T"));

	public static async Task<string> LoginAndGetTokenAsync(Login loginModel)
    {
        try
        {
            string loginUrl = ConfigurationManager.AppSettings["SiteUrl"] + "api/login"; // Replace with your actual login endpoint
            string jsonContent = JsonSerializer.Serialize(loginModel);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(loginUrl, httpContent);

            if (response.IsSuccessStatusCode)
            {
                // Assuming the backend returns the token in the response body
                return await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex)
        {
            Utils.LogError("Error during login: " + ex.Message);
        }
        return null;
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