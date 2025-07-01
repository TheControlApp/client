namespace ControlApp.Commands;

public abstract class Command(Command.Type type, string content) {
	public static readonly string[] bannedSites = ["booru.allthefallen.moe", "mega.nz", "media.mstdn.jp", "thecontrolapp.co.uk/Pages/ControlPC", "paradroid-gamma.vercel", "imagekit.io/tools/asset-public-link", "paradroid-gamma.web.app"];
	protected static readonly string[] bannedWords = ["money", "pay"];

    public enum Type : UInt32 {
        Popup           = 0b1,
        Audio           = 0b10,
        SendDelete      = 0b100,
        WatchForMe      = 0b1000,
        Twitter         = 0b10000,
        Wallpaper       = 0b100000,
        Runnable        = 0b1000000,
        Website         = 0b10000000,
        MessageBox      = 0b100000000,
        SubliminalImage = 0b1000000000,
        SubliminalText  = 0b10000000000,
        SubliminalLoop  = 0b100000000000,
        Webcam          = 0b1000000000000,
        MouseDisable    = 0b10000000000000,
        Download        = 0b100000000000000,
        Screenshot      = 0b1000000000000000,
        TTS             = 0b10000000000000000,
        WriteForMe      = 0b100000000000000000,
        InputDisable    = 0b1000000000000000000,
        Spinner         = 0b10000000000000000000
    }

    public Type type = type;
    public string content = content;

    public static Command ParseCommand(string parseString) {
        char parsedTypeChar = parseString[0];
        string commandContent = parseString.Substring(2);
        if (commandContent.StartsWith("FTP")) commandContent = string.Concat("https://www.thecontrolapp.co.uk/storage/", commandContent.AsSpan(3));
        return  parsedTypeChar switch { // alphabetically ordered
            'A' => new AudioCommand(commandContent),
            'D' => new DownloadCommand(commandContent),
            'F' => new WriteForMeCommand(commandContent),
            'L' => new SubliminalLoopCommand(commandContent),
            'M' => new MessageBoxCommand(commandContent),
            'O' or 'U' => new PopupCommand(commandContent), // U not really necessary, but here for compatibility reasons
            'P' => new WallpaperCommand(commandContent),
            'R' => new RunnableCommand(commandContent),
            'S' => new SubliminalImageCommand(commandContent),
            'V' => new SubliminalTextCommand(commandContent),
            'W' => new WebsiteCommand(commandContent),
            '1' => new ScreenshotCommand(commandContent),
            '2' => new WatchForMeCommand(commandContent),
            '3' => new TwitterCommand(commandContent),
            '4' => new SendDeleteCommand(commandContent),
            '5' => new TTSCommand(commandContent),
            '6' => new WebcamCommand(commandContent),
            '7' => new MouseDisableCommand(commandContent),
            '8' => new InputDisableCommand(commandContent),
            '9' => new SpinnerCommand(commandContent),
            _ => throw new ArgumentException("Command type " + parsedTypeChar + " not defined")
        };
    }

    public override string ToString() {
        return GetCode(type) + "=" + content;
    }

    protected static string GetLastItemFromUrl(string content) {
        return content.Substring(content.LastIndexOf('/') + 1);
    }

    public static char GetCode(Type type) {
        return type switch {
            Type.Popup           => 'O',
            Type.Audio           => 'A',
            Type.SendDelete      => '4',
            Type.WatchForMe      => '2',
            Type.Twitter         => '3',
            Type.Wallpaper       => 'P',
            Type.Runnable        => 'R',
            Type.Website         => 'W',
            Type.MessageBox      => 'M',
            Type.SubliminalImage => 'S',
            Type.SubliminalText  => 'V',
            Type.SubliminalLoop  => 'L',
            Type.Webcam          => '6',
            Type.MouseDisable    => '7',
            Type.InputDisable    => '8',
            Type.Download        => 'D',
            Type.Screenshot      => '1',
            Type.TTS             => '5',
            Type.WriteForMe      => 'F',
            Type.Spinner         => '9',
            _ => throw new ArgumentException("Argument is a composite type")
        };
    }

    public char GetCode() {
        return GetCode(type);
    }
    
    public abstract void Execute(string senderId);
}