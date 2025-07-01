using System.Configuration;
using ControlApp.Commands;

namespace ControlApp;

public partial class Options : Form { // TODO: Maybe merge "Config" with this as a new tab?
	public Options() {
		InitializeComponent();
	}

	private void Options_Load(object sender, EventArgs e) {
		string? popupStyle = ConfigurationManager.AppSettings["PopStyle"];
		parallelRadioButton.Checked = popupStyle == "Parallel"; // not checking for null to make serial the default
		serialRadioButton.Checked = !parallelRadioButton.Checked;
		string? scaleMode = ConfigurationManager.AppSettings["PaperStyle"];
		scaleStretchRadioButton.Checked = scaleMode == "Stretch"; // not checking for null to make stretch the default
		scaleToFitRadioButton.Checked = !scaleStretchRadioButton.Checked;
		string? poptypestr = ConfigurationManager.AppSettings["PopType"];
		poptypestr ??= "nnnn"; // fancy little thing I just learned of that assigns the rhs to the lhs only if lhs is null, required because we're making method calls later on
		seethroughRadioButton.Checked = poptypestr[0] == 's';
		opaqueRadioButton.Checked = !seethroughRadioButton.Checked; // not checking for 'n' to make opaque the default
		clickthroughRadioButton.Checked = poptypestr[1] == 't';
		clickableRadioButton.Checked = poptypestr[1] == 'c';
		normalRadioButton.Checked = !clickthroughRadioButton.Checked && !clickableRadioButton.Checked; // not checking for 'n' to make normal the default
		movingRadioButton.Checked = poptypestr[2] == 'n';
		stillRadioButton.Checked = !movingRadioButton.Checked; // not checking for 'm' to make non-moving the default in case of exception
		fullscreenCheckbox.Checked = poptypestr[3] == 'f';
		showBlockedCheckbox.Checked = Utils.CheckEnabled("Showblocked");
		longPopupRadioButton.Checked = ConfigurationManager.AppSettings["PopSet"] == "Long";
		shortPopupRadioButton.Checked = !longPopupRadioButton.Checked;
		reminderCheckbox.Checked = Utils.CheckEnabled("OutstandRemind");
		sendDeleteCheckbox.Checked = Utils.CheckEnabled("SendDelete");
		webcamCountCheckbox.Checked = Utils.CheckEnabled("WebCnt");
		twitterCheckbox.Checked = Utils.CheckEnabled("twitter");
		watchForMeCheckbox.Checked = Utils.CheckEnabled("Watch4me");
		audioCheckbox.Checked = Utils.CheckEnabled("Audios");
		writeForMeCheckbox.Checked = Utils.CheckEnabled("Writeformes");
		wallpaperCheckbox.Checked = Utils.CheckEnabled("Wallpapers");
		runnableCheckbox.Checked = Utils.CheckEnabled("Runables");
		screenshotCheckbox.Checked = Utils.CheckEnabled("ScreenShots");
		websiteCheckbox.Checked = Utils.CheckEnabled("OpenWeb");
		popupCheckbox.Checked = Utils.CheckEnabled("PopUps");
		messageCheckbox.Checked = Utils.CheckEnabled("Messages");
		subliminalCheckbox.Checked = Utils.CheckEnabled("Subliminals");
		webcamCheckbox.Checked = Utils.CheckEnabled("Webcam");
		ttsCheckbox.Checked = Utils.CheckEnabled("TTS");
		disableMouseCheckbox.Checked = Utils.CheckEnabled("DisM");
		disableInputCheckbox.Checked = Utils.CheckEnabled("DisableInput");
	}

	private void saveAndExitButton_Click(object sender, EventArgs e) {
		Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		KeyValueConfigurationCollection appSettings = configuration.AppSettings.Settings;
		appSettings.Remove("Downloads");
		appSettings.Remove("Wallpapers");
		appSettings.Remove("Runables");
		appSettings.Remove("OpenWeb");
		appSettings.Remove("PopUps");
		appSettings.Remove("Messages");
		appSettings.Remove("Subliminals");
		appSettings.Remove("PopSet");
		appSettings.Remove("Audios");
		appSettings.Remove("Writeformes");
		appSettings.Remove("Screenshots");
		appSettings.Remove("Watch4me");
		appSettings.Remove("twitter");
		appSettings.Remove("PaperStyle");
		appSettings.Remove("Popstyle");
		appSettings.Remove("SendDelete");
		appSettings.Remove("OutstandRemind");
		appSettings.Remove("PopType");
		appSettings.Remove("Showblocked");
		appSettings.Remove("Webcam");
		appSettings.Remove("TTS");
		appSettings.Remove("DisM");
		appSettings.Remove("WebCnt");
		appSettings.Remove("DisallowedCommands");
		appSettings.Remove("DisableInput");
		Command.Type disallowedCommands = 0;
		appSettings.Add("Showblocked", showBlockedCheckbox.Checked.ToString());
		string popupTypeString = seethroughRadioButton.Checked ? "s" : "n";
		if (clickthroughRadioButton.Checked) {
			popupTypeString += "t";
		} else if (clickableRadioButton.Checked) {
			popupTypeString += "c";
		} else if (normalRadioButton.Checked) {
			popupTypeString += "n";
		}
		popupTypeString += (movingRadioButton.Checked ? 'y' : 'n');
		popupTypeString += (fullscreenCheckbox.Checked ? 'f' : 'n');
		appSettings.Add("PopType", popupTypeString);
		appSettings.Add("OutstandRemind", reminderCheckbox.Checked.ToString());
		appSettings.Add("Popstyle", serialRadioButton.Checked ? "Serial" : "Parallel");
		appSettings.Add("PaperStyle", scaleStretchRadioButton.Checked ? "Stretch" : "Fit");
		appSettings.Add("SendDelete", sendDeleteCheckbox.Checked.ToString());
		if (sendDeleteCheckbox.Checked) disallowedCommands |= Command.Type.SendDelete;
		appSettings.Add("twitter", twitterCheckbox.Checked.ToString());
		if (twitterCheckbox.Checked) disallowedCommands |= Command.Type.Twitter;
		appSettings.Add("Watch4me", watchForMeCheckbox.Checked.ToString());
		if (watchForMeCheckbox.Checked) disallowedCommands |= Command.Type.WatchForMe;
		appSettings.Add("Screenshots", screenshotCheckbox.Checked.ToString());
		if (screenshotCheckbox.Checked) disallowedCommands |= Command.Type.Screenshot;
		appSettings.Add("Audios", audioCheckbox.Checked.ToString());
		if (audioCheckbox.Checked) disallowedCommands |= Command.Type.Audio;
		appSettings.Add("Writeformes", writeForMeCheckbox.Checked.ToString());
		if (writeForMeCheckbox.Checked) disallowedCommands |= Command.Type.WriteForMe;
		appSettings.Add("Downloads", downloadCheckbox.Checked.ToString());
		if (downloadCheckbox.Checked) disallowedCommands |= Command.Type.Download;
		appSettings.Add("Wallpapers", wallpaperCheckbox.Checked.ToString());
		if (wallpaperCheckbox.Checked) disallowedCommands |= Command.Type.Wallpaper;
		appSettings.Add("Runables", runnableCheckbox.Checked.ToString());
		if (runnableCheckbox.Checked) disallowedCommands |= Command.Type.Runnable;
		appSettings.Add("OpenWeb", websiteCheckbox.Checked.ToString());
		if (websiteCheckbox.Checked) disallowedCommands |= Command.Type.Website;
		appSettings.Add("PopUps", popupCheckbox.Checked.ToString());
		if (popupCheckbox.Checked) disallowedCommands |= Command.Type.Popup;
		appSettings.Add("Messages", messageCheckbox.Checked.ToString());
		if (messageCheckbox.Checked) disallowedCommands |= Command.Type.MessageBox;
		appSettings.Add("Subliminals", subliminalCheckbox.Checked.ToString());
		if (subliminalCheckbox.Checked) disallowedCommands |= Command.Type.SubliminalImage | Command.Type.SubliminalText | Command.Type.SubliminalLoop;
		appSettings.Add("Webcam", webcamCheckbox.Checked.ToString());
		if (webcamCheckbox.Checked) disallowedCommands |= Command.Type.Webcam;
		appSettings.Add("TTS", ttsCheckbox.Checked.ToString());
		if (ttsCheckbox.Checked) disallowedCommands |= Command.Type.TTS;
		appSettings.Add("DisM", disableMouseCheckbox.Checked.ToString());
		if (disableMouseCheckbox.Checked) disallowedCommands |= Command.Type.MouseDisable;
		appSettings.Add("DisableInput", disableInputCheckbox.Checked.ToString());
		if (disableInputCheckbox.Checked) disallowedCommands |= Command.Type.InputDisable;
		appSettings.Add("DisallowedCommands", disallowedCommands.ToString());
		appSettings.Add("PopSet", longPopupRadioButton.Checked ? "Long" : "Short");
		appSettings.Add("WebCnt", disableMouseCheckbox.Checked.ToString());
		configuration.Save(ConfigurationSaveMode.Full);
		ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
		MainWindow.RefreshCredentialCache();
		Close();
	}
}
