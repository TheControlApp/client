using ControlApp.Services;
using System.Configuration;
using System.Text.Json;

namespace ControlApp.Commands;

public abstract class Command(string commandType)
{
	public static readonly List<string> bannedSites = ["booru.allthefallen.moe", "mega.nz", "media.mstdn.jp", ConfigurationManager.AppSettings["SiteUrl"] + "Pages/ControlPC", "paradroid-gamma.vercel", "imagekit.io/tools/asset-public-link", "paradroid-gamma.web.app"];
	protected static readonly List<string> bannedWords = ["money", "pay"];

    public string CommandType { get; private set; } = commandType;

    public static Command? ParseJsonCommand(CommandStructure commandStructure)
    {
        List<string> disallowedCommands = ConfigurationService.GetDisallowedCommandsList();
        if (disallowedCommands.Contains(commandStructure.Type)) {
            return null;
        }

        return commandStructure.Type switch
        {
            CommandCodes.Audio => new AudioCommand(),
            CommandCodes.Download => new DownloadCommand(),
            CommandCodes.WriteForMe => new WriteForMeCommand(),
            CommandCodes.SubliminalLoop => new SubliminalLoopCommand(),
            CommandCodes.PopupText => new MessageBoxCommand(),
            CommandCodes.PopupMedia => new PopupCommand(),
            CommandCodes.Wallpaper => new WallpaperCommand(),
            CommandCodes.Runnable => new RunnableCommand(),
            CommandCodes.SubliminalImage => new SubliminalImageCommand(),
            CommandCodes.SubliminalText => new SubliminalTextCommand(),
            CommandCodes.Website => new WebsiteCommand(),
            CommandCodes.Screenshot => new ScreenshotCommand(),
            CommandCodes.WatchForMe => new WatchForMeCommand(),
            CommandCodes.Twitter => new TwitterCommand(),
            CommandCodes.SendDelete => new SendDeleteCommand(),
            CommandCodes.TTS => new TTSCommand(),
            CommandCodes.Webcam => new WebcamCommand(),
            CommandCodes.MouseDisable => new MouseDisableCommand(),
            CommandCodes.InputDisable => new InputDisableCommand(),
            CommandCodes.Spinner => new SpinnerCommand(),
            _ => null
        };
    }

    public abstract void Execute(string senderId, JsonElement content);
}