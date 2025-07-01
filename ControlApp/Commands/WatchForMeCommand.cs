using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class WatchForMeCommand(string content) : Command(Type.WatchForMe, content) {
    public override void Execute(string senderId) {
        new WatchForMe(content, senderId).Show();
    }
}