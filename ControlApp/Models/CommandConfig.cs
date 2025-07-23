namespace ControlApp.Models
{
    public class CommandConfig
    {
        public GeneralCommandConfig General { get; set; }
        public PopUpsCommandConfig PopUps { get; set; }
        public WallPaperCommandConfig WallPaper { get; set; }
    }

    public class GeneralCommandConfig
    {
        public DisAllowGeneralCommandConfig DisallowedCommands { get; set; }
        public MiscellaneousGeneralCommandConfig MiscellaneousConfigs { get; set; }

    }

    public class PopUpsCommandConfig
    {
        public PopupsDisplayLength PopupsDisplayLength { get; set; }
        public PopupsDisplayBehavior PopupsDisplayBehavior { get; set; }
        public PanelPopUpsCommandConfig PanelConfig { get; set; }

    }

    public class DisAllowGeneralCommandConfig
    {
        public bool DisallowDownloads { get; set; }
        public bool DisallowWallpapers { get; set; }
        public bool DisallowRunnableFiles { get; set; }
        public bool DisallowOpenWebsite { get; set; }
        public bool DisallowPopUps { get; set; }
        public bool DisallowMessages { get; set; }
        public bool DisallowSubliminals { get; set; }
        public bool DisallowAudios { get; set; }
        public bool DisallowWriteForMe { get; set; }
        public bool DisallowScreenshots { get; set; }
        public bool DisallowTwitterPoster { get; set; }
        public bool DisallowWatchForMe { get; set; }
        public bool DisallowSendOrDelete { get; set; }
        public bool DisallowWebcam { get; set; }
        public bool DisallowTTS { get; set; }
        public bool DisallowDisableMouse { get; set; }
        public bool DisallowDisableInput { get; set; }
        public bool DisallowOutstandingReminders { get; set; }
    }

    public class MiscellaneousGeneralCommandConfig
    {
        public bool WebcamCountdown { get; set; }
        public bool Showblocked { get; set; }
        public bool DarkMode { get; set; }
        public bool AutoRunSentExe { get; set; }
        public bool RunAllOutstanding { get; set; }
        public int RunAllOutstandingDelaySeconds { get; set; }
        public string DownloadsFolderPath { get; set; }
    }

    public enum PopupsDisplayLength
    {
        SHORT,
        MEDIUM,
        LONG
    }

    public enum PopupsDisplayBehavior
    {
        ONE,
        ALL
    }

    public class PanelPopUpsCommandConfig
    {
        public PanelPopupsMovement PopupsMovement { get; set; }
        public PanelPopupsClickability PopupsClickability { get; set; }
        public PanelPopupsOpacity PopupsOpacity { get; set; }
        public bool PopupsFullScreen { get; set; }
    }

    public enum PanelPopupsMovement
    {
        NONE,
        MOVING
    }
    public enum PanelPopupsClickability
    {
        STANDARD,
        CLICKTHROUGH,
        CLICKABLE
    }
    public enum PanelPopupsOpacity
    {
        STANDARD,
        SEETHROUGH
    }

    public class WallPaperCommandConfig
    {
        public WallpaperFitting Fitting { get; set; }
    }

    public enum WallpaperFitting
    {
        STRETCH,
        FITSCREEN
    }
}
