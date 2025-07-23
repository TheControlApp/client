using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using System.Text.Json;

namespace ControlApp.Commands;

public class WatchForMeCommand : Command {
    public WatchForMeCommand(): base(CommandCodes.WatchForMe) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        new WatchForMe(url, senderId).Show();
    }
}