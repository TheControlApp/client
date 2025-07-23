using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using ControlApp.Utils;
using System.Text.Json;

namespace ControlApp.Commands;

public class DownloadCommand : Command {
    public DownloadCommand() : base(CommandCodes.Download) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        string filename = ServerCommunicator.GetFile(url)!;
        if (filename != null) {
            new CustomMessage("File downloaded! Find it here : " + filename, "", 3, false).ShowDialog();
        }
    }
}