using System.Configuration;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ControlApp.Commands;
using ControlApp.Subroutines;

namespace ControlApp;

public partial class MainWindow : Form {
    public static string? username = ConfigurationManager.AppSettings["UserName"];
    public static string? password = ConfigurationManager.AppSettings["Password"];
	public static bool verified;

	private static string[]? userBlacklist;

	private static string[]? lastSender;

	private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");

	public MainWindow() {
		InitializeComponent();
		verified = false;
		Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		KeyValueConfigurationCollection settings = configuration.AppSettings.Settings;
		string? downloadPath = ConfigurationManager.AppSettings["LocalDrive"];
		if (string.IsNullOrEmpty(downloadPath)) {
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

    public void RefreshCredentialCache() {
        usernameInput.Text = ConfigurationManager.AppSettings["UserName"];
    }

    private void UpdateTimerState() {
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
    
	private async void timer1_Tick(object sender, EventArgs e) {
		Utils.LogInfo("Doing periodic check");
		await CheckNext();
		if (Utils.CheckEnabled("RunAll")) {
			Utils.LogInfo("Running waiting commands");
			await RunNextCommand();
		}
		await CheckNext();
	}

	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern uint SHGetKnownFolderPath(ref Guid id, int flags, nint token, out nint path);

	private static string GetDownloadPath() {
		if (Environment.OSVersion.Version.Major < 6) {
			throw new NotSupportedException();
		}
		uint sysCallCode = SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out nint pathPtr);
		if (sysCallCode != 0) throw new IOException("System call failed with error code " + sysCallCode); // if sys call returns something other than S_OK...
		string? result = Marshal.PtrToStringUni(pathPtr);
		Marshal.FreeCoTaskMem(pathPtr);
		if (result == null) {
			throw new IOException("System could not retrieve download destination");
		}
		return result;
	}


	private async Task CheckNext() {
		Cursor.Show();
		Cursor? cursor = Cursor.Current;
		Cursor.Current = Cursors.WaitCursor;
		Utils.LogInfo("Checking waiting commands");
		string[]? result = await ServerCommunicator.GetOutstanding();
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
		string[]? result = await ServerCommunicator.GetLatestItem();
		if (result != null) {
			lastSender = result;
			RunCommands(lastSender[1].Split("|||"), lastSender[0]);
		}
	}

	private void RunCommands(string[] commandArray, string senderUsername) {
		Command[] commands = HandleLines(commandArray);
		if (commands.Length == 0) return;
		SystemSounds.Beep.Play();
		foreach (Command command in commands) {
			Utils.LogInfo("Executing " + command);
			command.Execute(senderUsername);
		}
	}

	private Command[] HandleLines(string[] lines) {
		List<Command> returnList = new List<Command>();
		uint disallowedCommands = Convert.ToUInt32(ConfigurationManager.AppSettings["DisAllowedCommands"]);
		if (disallowedCommands >= Command.GetSmallestIllegalType()) disallowedCommands = Command.DANGEROUS_COMMANDS;
		string? configBlacklistOutput = ConfigurationManager.AppSettings["BlackList"];
		bool blacklistFound = false;
		if (!string.IsNullOrWhiteSpace(configBlacklistOutput)) {
			blacklistFound = true;
			userBlacklist = Utils.SeparateArrayString(configBlacklistOutput);
		}
		foreach (string line in lines) {
			if (line == "") continue;
			string? decryptedCommand = Utils.Decrypt(line);
			if (decryptedCommand == null) {
				Utils.LogInfo($"Decryption failed, skipping...");
				continue;
			}
			Command parsedCommand;
			try {
				parsedCommand = Command.ParseCommand(decryptedCommand);
			}
			catch (ArgumentException e) {
				Utils.LogInfo($"Exception in command parser: {e.Message}, skipping...");
				continue;
			}

			if ((disallowedCommands & (uint) parsedCommand.type) != 0 || string.IsNullOrEmpty(parsedCommand.content)) {
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
				Debug.Assert(userBlacklist != null, nameof(userBlacklist) + " != null");
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
		sendCommandTab.PopulateDestUserList();
	}

	private void thumbsUpButton_Click(object sender, EventArgs e) {
		if (lastSender != null && lastSender[0] != "-1") {
			ServerCommunicator.ThumbsUp(lastSender[0]);
		}
	}

	public static string? GetLastSenderId() {
		return lastSender?[0];
	}
}
