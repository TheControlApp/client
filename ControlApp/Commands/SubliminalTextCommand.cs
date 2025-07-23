using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using System.Text.Json;

namespace ControlApp.Commands;

public class SubliminalTextCommand : Command {
    public SubliminalTextCommand(): base(CommandCodes.SubliminalText) { }
    public override void Execute(string senderId, JsonElement content) {
        string text = content.GetProperty("text").ToString() ?? throw new WrongCommandFormatException();
        new Subliminal(text, true).Show();
    }
}