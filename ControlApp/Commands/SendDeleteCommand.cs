using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class SendDeleteCommand(string content) : Command(Type.SendDelete, content) {
    public override void Execute(string senderId) {
        new SendOrDelete(senderId).Show();
    }
}