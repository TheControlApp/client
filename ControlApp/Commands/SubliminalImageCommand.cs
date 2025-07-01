using System.Diagnostics;
using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class SubliminalImageCommand(string content) : Command(Type.SubliminalImage, content) {
    public override void Execute(string senderId) {
        if (Utils.IsFile(content)){
            Process.Start(new ProcessStartInfo{
                FileName = content,
                UseShellExecute = true
            });
        }
        string? filePath = ServerCommunicator.GetFile(content);
        if (filePath != null) {
            new Subliminal(filePath, message: false).Show();
        }
    }
}