using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using System.Text.Json;

namespace ControlApp.Commands;

public class SubliminalLoopCommand : Command {
    public SubliminalLoopCommand(): base(CommandCodes.SubliminalLoop) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        SubLoop.AddItem(url);
    }
}