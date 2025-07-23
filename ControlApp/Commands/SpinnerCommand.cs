using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using System.Net.Http.Json;
using System.Text.Json;

namespace ControlApp.Commands;

public class SpinnerCommand : Command {
    public SpinnerCommand()  : base(CommandCodes.Spinner) { }
    public override void Execute(string senderId, JsonElement content) {
        string[] spinArgs = JsonSerializer.Deserialize<string[]>(content.GetProperty("options")) ?? throw new WrongCommandFormatException();
        new Spinner(spinArgs).Show();
    }
}