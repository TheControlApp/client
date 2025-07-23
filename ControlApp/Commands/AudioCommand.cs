using ControlApp.Subroutines;
using ControlApp.Utils;
using System.Text.Json;

namespace ControlApp.Commands;

public class AudioCommand : Command {
    public AudioCommand() : base(CommandCodes.Audio) { }
    public override void Execute(string senderId, JsonElement content) {
        string fileSource = content.GetProperty("url").ToString();
        if (!Utilities.IsAudioFile(fileSource)) {
            Utilities.LogError($"Non-audio file {fileSource} passed onto AudioPopup, skipping...");
            return;
        }
        string? filePath = ServerCommunicator.GetFile(fileSource);
        if (filePath != null) new AudioPopup(filePath).Show();
    }
}