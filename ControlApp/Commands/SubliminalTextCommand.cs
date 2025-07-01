using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class SubliminalTextCommand(string content) : Command(Type.SubliminalText, content) {
    public override void Execute(string senderId) {
        new Subliminal(content, true).Show();
    }
}