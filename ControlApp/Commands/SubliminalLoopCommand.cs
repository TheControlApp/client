using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class SubliminalLoopCommand(string content) : Command(Type.SubliminalLoop, content) {
    public override void Execute(string senderId) {
        SubLoop.AddItem(content);
    }
}