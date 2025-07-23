using ControlApp.Subroutines;
using ControlApp.Utils;
using System.Text.Json;

namespace ControlApp.Commands;

public class InputDisableCommand : Command {
    public InputDisableCommand(): base(CommandCodes.InputDisable) { }
    public override void Execute(string senderId, JsonElement content) {
        Blank? openBlank = (Blank?) Utilities.GetForm(typeof(Blank));
        if (openBlank == null) {
            new Blank(true, true).Show();
            Cursor.Show();
        } else {
            openBlank.AddTime(true, true);
        }
    }
}