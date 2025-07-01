using System.Diagnostics;
using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class RunnableCommand(string content) : Command(Type.Runnable, content) {
    public override void Execute(string senderId) {
        if (!Utils.IsExecutableFile(content)) return;
        string? filename = ServerCommunicator.GetFile(content);
        if (filename == null) return;
        if (Utils.CheckEnabled("AutoRun")) {
            Process.Start(filename);
        } else {
            new CustomMessage("Executable downloaded! Find it here : " + filename, "", 4, false).ShowDialog();
        }
    }
}