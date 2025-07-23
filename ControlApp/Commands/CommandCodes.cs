namespace ControlApp.Commands;

/// <summary>
/// Centralizes the string identifiers for all command types used in the JSON payload.
/// This prevents the use of "magic strings" throughout the application, making the code
/// safer and easier to maintain.
/// </summary>
public static class CommandCodes
{
    // User Interface and Interaction Commands
    public const string PopupText = "popup-text"; // MessageBoxCommand
    public const string PopupMedia = "popup-media"; // PopupCommand
    public const string WriteForMe = "write-for-me";
    public const string SendDelete = "send-delete";
    public const string Spinner = "spinner";
    public const string WatchForMe = "watch-for-me";

    // Subliminal Commands
    public const string SubliminalImage = "subliminal-image";
    public const string SubliminalText = "subliminal-text";
    public const string SubliminalLoop = "subliminal-loop";

    // System and File Operation Commands
    public const string Audio = "audio";
    public const string Download = "download";
    public const string Runnable = "runnable";
    public const string Wallpaper = "wallpaper";
    public const string Website = "website";

    // Surveillance and Control Commands
    public const string Screenshot = "screenshot";
    public const string Webcam = "webcam";
    public const string MouseDisable = "mouse-disable";
    public const string InputDisable = "input-disable";
    public const string TTS = "tts";

    // Social Media Commands
    public const string Twitter = "twitter";
}