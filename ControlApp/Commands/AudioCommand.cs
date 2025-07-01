using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class AudioCommand(string content) : Command(Type.Audio, content) {
    public override void Execute(string senderId) {
        string fileSource = content;
        if (!Utils.IsAudioFile(fileSource)) {
            Utils.LogError($"Non-audio file {fileSource} passed onto AudioPopup, skipping...");
            return;
        }
        string? filePath = ServerCommunicator.GetFile(fileSource);
        if (filePath != null) new AudioPopup(filePath).Show();
    }
}