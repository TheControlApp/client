using ControlApp.Subroutines;
using ControlApp.Utils;
using System.Text.Json;

namespace ControlApp.Commands;

public class MouseDisableCommand : Command {
    public MouseDisableCommand(): base(CommandCodes.MouseDisable) { }
    public override void Execute(string senderId, JsonElement content) {
        Blank? openBlank = (Blank?) Utilities.GetForm(typeof(Blank));
        if (openBlank == null) {
            new Blank(true, false).Show();
            Cursor.Show();
        } else {
            openBlank.AddTime(true, false);
        }
    }
}