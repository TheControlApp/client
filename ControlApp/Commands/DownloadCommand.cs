using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class DownloadCommand(string content) : Command(Type.Download, content) {
    public override void Execute(string senderId) {
        string filename = ServerCommunicator.GetFile(content);
        if (filename != null) {
            new CustomMessage("File downloaded! Find it here : " + filename, "", 3, false).ShowDialog();
        }
    }
}