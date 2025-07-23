using ControlApp.Commands;
using ControlApp.Models;
using ControlApp.Services;
using ControlApp.Utils;
using System.Configuration;
using System.Windows.Forms;

namespace ControlApp;

public partial class CommandOptionsForm : Form
{ // TODO: Maybe merge "Config" with this as a new tab?
    public CommandOptionsForm()
    {
        InitializeComponent();
        Load += Options_Load;
        saveAndExitButton.Click += saveAndExitButton_Click;
    }

    private void Options_Load(object sender, EventArgs e)
    {
        var settings = ConfigurationService.CommandSettings;

        // Load General -> Disallowed Commands
        // Note: The checkboxes represent "allowed", so the logic is inverted from the "Disallow" property name.
        downloadCheckbox.Checked = !settings.General.DisallowedCommands.DisallowDownloads;
        wallpaperCheckbox.Checked = !settings.General.DisallowedCommands.DisallowWallpapers;
        runnableCheckbox.Checked = !settings.General.DisallowedCommands.DisallowRunnableFiles;
        websiteCheckbox.Checked = !settings.General.DisallowedCommands.DisallowOpenWebsite;
        popupCheckbox.Checked = !settings.General.DisallowedCommands.DisallowPopUps;
        messageCheckbox.Checked = !settings.General.DisallowedCommands.DisallowMessages;
        subliminalCheckbox.Checked = !settings.General.DisallowedCommands.DisallowSubliminals;
        audioCheckbox.Checked = !settings.General.DisallowedCommands.DisallowAudios;
        writeForMeCheckbox.Checked = !settings.General.DisallowedCommands.DisallowWriteForMe;
        screenshotCheckbox.Checked = !settings.General.DisallowedCommands.DisallowScreenshots;
        twitterCheckbox.Checked = !settings.General.DisallowedCommands.DisallowTwitterPoster;
        watchForMeCheckbox.Checked = !settings.General.DisallowedCommands.DisallowWatchForMe;
        sendDeleteCheckbox.Checked = !settings.General.DisallowedCommands.DisallowSendOrDelete;
        webcamCheckbox.Checked = !settings.General.DisallowedCommands.DisallowWebcam;
        ttsCheckbox.Checked = !settings.General.DisallowedCommands.DisallowTTS;
        disableMouseCheckbox.Checked = !settings.General.DisallowedCommands.DisallowDisableMouse;
        disableInputCheckbox.Checked = !settings.General.DisallowedCommands.DisallowDisableInput;
        reminderCheckbox.Checked = !settings.General.DisallowedCommands.DisallowOutstandingReminders;

        // Load General -> Miscellaneous
        webcamCountCheckbox.Checked = settings.General.MiscellaneousConfigs.WebcamCountdown;
        showBlockedCheckbox.Checked = settings.General.MiscellaneousConfigs.Showblocked;
        darkModeCheckbox.Checked = settings.General.MiscellaneousConfigs.DarkMode;
        autoRunSentExeCheckBox.Checked = settings.General.MiscellaneousConfigs.AutoRunSentExe;
        runAllOutstandingCheckBox.Checked = settings.General.MiscellaneousConfigs.RunAllOutstanding;
        delayBetweenOutstandingCommandsNumericUpDown.Value = settings.General.MiscellaneousConfigs.RunAllOutstandingDelaySeconds;
        textBoxDownloadFolder.Text = settings.General.MiscellaneousConfigs.DownloadsFolderPath;

        // Load PopUps
        parallelRadioButton.Checked = settings.PopUps.PopupsDisplayBehavior == PopupsDisplayBehavior.ALL;
        serialRadioButton.Checked = settings.PopUps.PopupsDisplayBehavior != PopupsDisplayBehavior.ONE;
        longPopupRadioButton.Checked = settings.PopUps.PopupsDisplayLength == PopupsDisplayLength.LONG;
        shortPopupRadioButton.Checked = settings.PopUps.PopupsDisplayLength != PopupsDisplayLength.LONG;

        // Load PopUps -> Panel Config
        seethroughRadioButton.Checked = settings.PopUps.PanelConfig.PopupsOpacity == PanelPopupsOpacity.SEETHROUGH;
        opaqueRadioButton.Checked = settings.PopUps.PanelConfig.PopupsOpacity != PanelPopupsOpacity.SEETHROUGH;

        clickableRadioButton.Checked = settings.PopUps.PanelConfig.PopupsClickability == PanelPopupsClickability.CLICKABLE;
        clickthroughRadioButton.Checked = settings.PopUps.PanelConfig.PopupsClickability == PanelPopupsClickability.CLICKTHROUGH;
        normalRadioButton.Checked = settings.PopUps.PanelConfig.PopupsClickability == PanelPopupsClickability.STANDARD;

        movingRadioButton.Checked = settings.PopUps.PanelConfig.PopupsMovement == PanelPopupsMovement.MOVING;
        stillRadioButton.Checked = settings.PopUps.PanelConfig.PopupsMovement != PanelPopupsMovement.MOVING;

        fullscreenCheckbox.Checked = settings.PopUps.PanelConfig.PopupsFullScreen;

        // Load Wallpaper
        scaleStretchRadioButton.Checked = settings.WallPaper.Fitting == WallpaperFitting.STRETCH;
        scaleToFitRadioButton.Checked = settings.WallPaper.Fitting != WallpaperFitting.STRETCH;
    }

    private void saveAndExitButton_Click(object sender, EventArgs e)
    {
        var settings = ConfigurationService.CommandSettings;

        // Save General -> Disallowed Commands
        settings.General.DisallowedCommands.DisallowDownloads = !downloadCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowWallpapers = !wallpaperCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowRunnableFiles = !runnableCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowOpenWebsite = !websiteCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowPopUps = !popupCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowMessages = !messageCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowSubliminals = !subliminalCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowAudios = !audioCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowWriteForMe = !writeForMeCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowScreenshots = !screenshotCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowTwitterPoster = !twitterCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowWatchForMe = !watchForMeCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowSendOrDelete = !sendDeleteCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowWebcam = !webcamCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowTTS = !ttsCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowDisableMouse = !disableMouseCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowDisableInput = !disableInputCheckbox.Checked;
        settings.General.DisallowedCommands.DisallowOutstandingReminders = !reminderCheckbox.Checked;

        // Save General -> Miscellaneous
        settings.General.MiscellaneousConfigs.WebcamCountdown = webcamCountCheckbox.Checked;
        settings.General.MiscellaneousConfigs.Showblocked = showBlockedCheckbox.Checked;
        settings.General.MiscellaneousConfigs.DarkMode = darkModeCheckbox.Checked;
        settings.General.MiscellaneousConfigs.AutoRunSentExe = autoRunSentExeCheckBox.Checked;
        settings.General.MiscellaneousConfigs.RunAllOutstanding = runAllOutstandingCheckBox.Checked;
        settings.General.MiscellaneousConfigs.RunAllOutstandingDelaySeconds = (int)delayBetweenOutstandingCommandsNumericUpDown.Value;
        settings.General.MiscellaneousConfigs.DownloadsFolderPath = textBoxDownloadFolder.Text;

        // Save PopUps
        settings.PopUps.PopupsDisplayBehavior = parallelRadioButton.Checked ? PopupsDisplayBehavior.ALL : PopupsDisplayBehavior.ONE;
        settings.PopUps.PopupsDisplayLength = longPopupRadioButton.Checked ? PopupsDisplayLength.LONG : PopupsDisplayLength.SHORT;

        // Save PopUps -> Panel Config
        settings.PopUps.PanelConfig.PopupsOpacity = seethroughRadioButton.Checked ? PanelPopupsOpacity.SEETHROUGH : PanelPopupsOpacity.STANDARD;

        if (clickableRadioButton.Checked)
        {
            settings.PopUps.PanelConfig.PopupsClickability = PanelPopupsClickability.CLICKABLE;
        }
        else if (clickthroughRadioButton.Checked)
        {
            settings.PopUps.PanelConfig.PopupsClickability = PanelPopupsClickability.CLICKTHROUGH;
        }
        else
        {
            settings.PopUps.PanelConfig.PopupsClickability = PanelPopupsClickability.STANDARD;
        }

        settings.PopUps.PanelConfig.PopupsMovement = movingRadioButton.Checked ? PanelPopupsMovement.MOVING : PanelPopupsMovement.NONE;
        settings.PopUps.PanelConfig.PopupsFullScreen = fullscreenCheckbox.Checked;

        // Save Wallpaper
        settings.WallPaper.Fitting = scaleStretchRadioButton.Checked ? WallpaperFitting.STRETCH : WallpaperFitting.FITSCREEN;

        // Persist all changes to the file
        ConfigurationService.SaveSettings();
        Close();
    }

    private void OnClickSelectNewDownloadFolderButton(object sender, EventArgs e)
    {
        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            textBoxDownloadFolder.Text = folderBrowserDialog1.SelectedPath;
        }
    }

    private void fullscreenCheckbox_changed(object sender, EventArgs e)
    {
        if (fullscreenCheckbox.Checked)
        {
            serialRadioButton.Checked = true;
        }
        parallelRadioButton.Enabled = !fullscreenCheckbox.Checked;
    }

    private void parallelRadioButton_Changed(object sender, EventArgs e)
    {
        if (parallelRadioButton.Checked)
        {
            fullscreenCheckbox.Checked = false;
        }
        fullscreenCheckbox.Enabled = !parallelRadioButton.Checked;
    }

    private void disableInputCheckbox_Changed(object sender, EventArgs e)
    {
        if (disableInputCheckbox.Checked)
        {
            disableMouseCheckbox.Checked = true;
        }
        disableMouseCheckbox.Enabled = !disableInputCheckbox.Checked;
    }

    void shortPopupRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        longPopupRadioButton.Checked = !shortPopupRadioButton.Checked;
    }

    void longPopupRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        shortPopupRadioButton.Checked = !longPopupRadioButton.Checked;
    }
}
