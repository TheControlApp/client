using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class SpinnerCommand(string content) : Command(Type.Spinner, content) {
    public override void Execute(string senderId) {
        new Spinner(content).Show();
    }
}