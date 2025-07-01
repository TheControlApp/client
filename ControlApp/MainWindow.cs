using System.Configuration;
using System.Media;
using System.Runtime.InteropServices;
using ControlApp.Commands;
using ControlApp.Subroutines;

namespace ControlApp;

public partial class MainWindow : Form {
    public static string? username = ConfigurationManager.AppSettings["UserName"];
    public static string? password = ConfigurationManager.AppSettings["Password"];
	public static bool verified;

	private static string[] userBlacklist;

	private static string[]? lastSender;

	private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");

	public MainWindow() {
		InitializeComponent();
		string? blacklistString = ConfigurationManager.AppSettings["BlackList"];
		if (blacklistString != null) {
			userBlacklist = Utils.SeparateString(blacklistString);
		}
		verified = false;
		Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
		string? downloadPath = ConfigurationManager.AppSettings["LocalDrive"];
		if (String.IsNullOrEmpty(downloadPath)) {
			downloadPath = GetDownloadPath() + "\\";
		} else if (!downloadPath.EndsWith('\\')) {
			downloadPath += "\\";
		}
		settings.Remove("LocalDrive");
		settings.Add("LocalDrive", downloadPath);
		configuration.Save(ConfigurationSaveMode.Full);
		ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
		UpdateTimerState();
	}

    public static void RefreshCredentialCache() {
        username = ConfigurationManager.AppSettings["UserName"];
        password = ConfigurationManager.AppSettings["Password"];
    }

    public void UpdateTimerState() {
	    if (Utils.CheckEnabled("RunAll")) {
		    timer.Start();
	    } else {
		    timer.Stop();
	    }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
	    if (e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.None) return;
	    e.Cancel = true;
	    Hide();
    }

    protected override void OnLoad(EventArgs e) {
		base.OnLoad(e);
		usernameInput.Text = username;
	}
    
	private void timer1_Tick(object sender, EventArgs e) {
		Utils.LogInfo("Doing periodic check");
		CheckNext();
		if (Utils.CheckEnabled("RunAll")) {
			Utils.LogInfo("Running waiting commands");
			RunNextCommand();
		}
		CheckNext();
	}

	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern int SHGetKnownFolderPath(ref Guid id, int flags, nint token, out nint path);

	private static string GetDownloadPath() {
		if (Environment.OSVersion.Version.Major < 6) {
			throw new NotSupportedException();
		}
		nint pathPtr = IntPtr.Zero;
		try {
			SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
			return Marshal.PtrToStringUni(pathPtr);
		} finally {
			Marshal.FreeCoTaskMem(pathPtr);
		}
	}


	private void CheckNext() {
		Cursor.Show();
		Cursor? cursor = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		Utils.LogInfo("Checking waiting commands");
		string[]? result = ServerCommunicator.GetOutstanding();
		if (result == null || result.Length == 0) return;
		commandCountTextBox.Text = result[0];
		nextUserLabel.Text = result[1];
		verified = result[2] == "1";
		scoreInput.Text = result[3];
		if (result[0] != "0" && !CustomMessage.IsTtsDisabled() && Utils.CheckEnabled("OutstandRemind")) {
			try {
				new CustomMessage($"You have {result[0]} outstanding {(result[0] == "1" ? "command" : "commands")}.", "", 0, ttsCommand: true).Show();
			} catch (Exception ex) {
				Utils.LogWarning("Error while checking for count: " + ex.Message + "\n" + ex.StackTrace);
			}
		}
		Cursor.Current = cursor;
	}

	private async Task RunNextCommand() {
		Utils.LogInfo("Running next command");
		timer.Stop();
		timer.Start();
		string[]? result = ServerCommunicator.GetLatestItem();
		if (result != null) {
			lastSender = result;
			RunCommands(lastSender[1].Split("|||"), lastSender[0]);
		}
	}

	private void RunCommands(string[] commandArray, string senderUsername) {
		Command[] commands = HandleLines(commandArray);
		Console.WriteLine($"Processed command array contains {commands.Length} elements");
		if (commands.Length == 0) return;
		SystemSounds.Beep.Play();
		foreach (Command command in commands) {
			Utils.LogInfo("Executing " + command);
			command.Execute(senderUsername);
		}
	}

	private Command[] HandleLines(string[] lines) {
		List<Command> returnList = new List<Command>();
		Command.Type disallowedCommands = (Command.Type) Convert.ToUInt32(ConfigurationManager.AppSettings["DisAllowedCommands"]); // TODO: Add exception handling
		string? configBlacklistOutput = ConfigurationManager.AppSettings["BlackList"];
		bool blacklistFound = false;
		if (!String.IsNullOrWhiteSpace(configBlacklistOutput)) {
			blacklistFound = true;
			userBlacklist = Utils.SeparateString(configBlacklistOutput);
		}
		foreach (string line in lines) {
			if (line == "") continue;
			string DecryptedCommand = Utils.Decrypt(line);
			Command parsedCommand;
			try {
				parsedCommand = Command.ParseCommand(DecryptedCommand);
			} catch (ArgumentException e) {
				Utils.LogInfo("Exception in command parser: " + e.Message + ", skipping...");
				continue;
			}

			if ((disallowedCommands & parsedCommand.type) != 0 || string.IsNullOrEmpty(parsedCommand.content)) {
				Utils.LogInfo($"Command {parsedCommand} skipped because it is not allowed");
				continue;
			}
			bool containsBlacklisted = false;
			foreach (string element in Command.bannedSites) {
				if (parsedCommand.content.Contains(element)) {
					new CustomMessage("Command contains banned sites, skipping...", "", 3, false).ShowDialog();
					containsBlacklisted = true;
				}
			}
			if (containsBlacklisted) continue;
			if (blacklistFound) {
				foreach (string element in userBlacklist) {
					if (parsedCommand.content.Contains(element)) {
						new CustomMessage("Command contains blacklisted terms, skipping...", "", 3, false).ShowDialog();
						containsBlacklisted = true;
						break;
					}
				}
				if (containsBlacklisted) continue;
			}
			returnList.Add(parsedCommand);
		}
		return returnList.ToArray();
	}

	private void RunLastButtonClick(object sender, EventArgs e) {
		if (lastSender == null) return;
		RunCommands(lastSender[1].Split("|||"), lastSender[0]);
	}

	private void otherToolStripMenuItem_Click(object sender, EventArgs e) {
		using (Other ot = new Other()) {
			ot.ShowDialog();
		}
		sendCommandTab.PopulateUserList();
	}

	private void thumbsUpButton_Click(object sender, EventArgs e) {
		if (lastSender != null && lastSender[0] != "-1") {
			ServerCommunicator.ThumbsUp(lastSender[0]);
		}
	}

	public static string? GetLastSenderId() {
		return lastSender == null ? null : lastSender[0];
	}
}
