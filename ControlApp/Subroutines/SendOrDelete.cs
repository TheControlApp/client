using System.Configuration;

namespace ControlApp.Subroutines;

public partial class SendOrDelete : Form {
	private string candidateFile;

	private string senderId;

	public SendOrDelete(string senderId) {
		string? location = ConfigurationManager.AppSettings["LocalDrive"];
		if (location == null) return;
		List<string> candidateList = new List<string>();
		foreach (string file in Directory.GetFiles(location)) {
			FileInfo info = new FileInfo(file);
			if ((!Utils.IsAnimatedFile(file) && !Utils.IsImageFile(file)) || info.Length >= 1000000) continue;
			candidateList.Add(file);
		}
		if (candidateList.Count <= 0) return;
		Random rnd = new Random();
		candidateFile = candidateList[rnd.Next(candidateList.Count)];
		this.senderId = senderId;
		
		InitializeComponent();
	}

	private void deleteButton_Click(object sender, EventArgs e) {
		File.Delete(candidateFile);
		string command = $"M={MainWindow.username} chose to delete.";
		ServerCommunicator.SendCommand(senderId, Utils.Encrypt(command), groupSend: false);
		Close();
	}

	private void SendOrDelete_Load(object sender, EventArgs e) {
		axWindowsMediaPlayer1.URL = candidateFile;
		axWindowsMediaPlayer1.Ctlenabled = false;
		axWindowsMediaPlayer1.uiMode = "None";
		axWindowsMediaPlayer1.settings.autoStart = true;
		axWindowsMediaPlayer1.settings.setMode("loop", varfMode: true);
	}

	private void sendButton_CLick(object sender, EventArgs e) {
		if (ServerCommunicator.SendFtpFile(candidateFile)) {
			string messageCommand = $"M={MainWindow.username} chose to send.";
			string popupCommand = "U=FTP" + Path.GetFileName(candidateFile);
			ServerCommunicator.SendCommand(senderId,
				Utils.Encrypt(messageCommand) + "|||" + Utils.Encrypt(popupCommand), groupSend: false);
		}

		Close();
	}
}
