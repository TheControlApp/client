using ControlApp.Subroutines;
using System.Text.Json;

namespace ControlApp.Commands;

public class SendDeleteCommand : Command {
    public SendDeleteCommand() :base(CommandCodes.SendDelete){ }
    public override void Execute(string senderId, JsonElement content) {
        new SendOrDelete(senderId).Show();
    }
}