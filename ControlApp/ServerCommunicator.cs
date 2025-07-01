using System.Configuration;
using FluentFTP;
using HtmlAgilityPack;
using ControlApp.Subroutines;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ControlApp;

public abstract class ServerCommunicator : HttpClient {
    private static readonly HttpClient _httpClient = new HttpClient();
	private static readonly FtpClient _ftpClient = new FtpClient("ftp://home240474283.1and1-data.host/", "acc929431981", Utils.Decrypt("6scM67YJ+Ezzz0RKCeIxbT9TAfSbRE++1T"));

	private static HtmlNode? GetCommand(string command) {
		if (_httpClient.Timeout != TimeSpan.FromMilliseconds(1000)) _httpClient.Timeout = TimeSpan.FromMilliseconds(1000);
		string url = $"https://www.thecontrolapp.co.uk/AppCommand.aspx?UserNm={MainWindow.username}&Pwd={MainWindow.password}&vrs=012&cmd={command}";
		Utils.LogInfo("Getting response from URL: " + url);
		HttpResponseMessage message;
		try {
			message = _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url)).Result;
		} catch (Exception ex) {
			Utils.LogWarning("Error during command: \"" + command + "\"" + ex.Message);
			return null;
		}
		if (!message.IsSuccessStatusCode) {
			Utils.LogError($"Server returned error code {message.StatusCode} during command {command}");
			return null;
		}
		HtmlDocument document = new HtmlDocument();
		Utils.LogInfo("Server successfully replied");
		document.LoadHtml(message.Content.ReadAsStringAsync().Result);
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
			new CustomMessage("Sending file to server failed", "", 4, false).ShowDialog();
			Utils.LogWarning("Error during FTP: " + ex.Message);
			return false;
		}
		message.Dispose();
		return true;
	}

	public static string[]? GetOutstanding() {
		string? result = GetChildWithId(GetCommand("Outstanding"), "result");
		return result == null ? null : Utils.SeparateString(result);
	}

	public static bool DeleteOutstanding() {
		return GetCommand("Delete") != null;
	}

	public static bool SendCommand(string destUser, string command, bool groupSend) {
		string username = MainWindow.username ?? string.Empty;
		string password = MainWindow.password ?? string.Empty;
		byte allint = Convert.ToByte(groupSend);
		string url = $"https://www.thecontrolapp.co.uk/AppSendContent.aspx?UserNm={destUser}&comm={command}&all={allint}&fromuser={username}&frompword={password}";
		Utils.LogInfo("Getting response from: " + url);
		try {
			_httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
		} catch (HttpRequestException e) {
			Utils.LogError("Could not get a response from server: " + e.Message);
			return false;
		} catch (Exception e) {
			Utils.LogError("Error while sending command: " + e.Message);
			return false;
		}
		Utils.LogInfo("Server successfully replied");
		return true;
	}

	public static bool SendBlockReport(string senderid, string command, string report) {
		string url = $"https://www.thecontrolapp.co.uk/BlockReport.aspx?usernm={MainWindow.username}&pwd={MainWindow.password}&vrs=012&sender={senderid}&report={report}&content={command}";
		try {
			_httpClient.Send(new HttpRequestMessage(HttpMethod.Get, url));
		} catch (HttpRequestException e) {
			Utils.LogError("Could not get a response from server: " + e.Message);
			return false;
		} catch (Exception e) {
			Utils.LogError("Error while sending report: " + e.Message);
			return false;
		}
		return true;
	}

	public static string[]? GetLatestItem() {
		HtmlNode? node = GetCommand("Content");
		if (node == null) return null;
		string? childNode = GetChildWithId(node, "result");
		if (childNode == null) return null;
		string[] output = Utils.SeparateString(childNode);
		return output;
	}

	/*	Keeping this piece of code here for archival purposes. Who knows, maybe it'll be useful someday.
	public static string[] GetRelations() {
		string result = GetChildWithId(GetCommand("Relations"), "result");
		return result == null ? null : Utils.SeparateString(result);
	}

	public static bool AcceptInvite(string dom) {
		return GetCommand("Accept" + dom) != null;
	}

	public static bool RejectInvite(string dom) {
		return GetCommand("Reject" + dom) != null;
	}

	public static string[] getInvites() {
		string result = GetChildWithId(GetCommand("Invite"), "result");
		return result == null ? null : Utils.SeparateString(result);
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
		using CustomMessage cm = new CustomMessage("Downloading image, please wait", "", 0, false);
		cm.Show();
		try {
			using Task<Stream> s = _httpClient.GetStreamAsync(url);
			using FileStream fs = new FileStream(filePath, FileMode.Create);
			s.Result.CopyTo(fs);
		} catch (Exception ex) {
			Utils.LogWarning("Error getting file for \"" + url + "\":" + ex.Message);
			return null;
		}
		cm.Hide();
		Utils.LogInfo("Successfully got file from " + url);
		return filePath;
	}
}