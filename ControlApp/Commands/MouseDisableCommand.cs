using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class MouseDisableCommand(string content) : Command(Type.MouseDisable, content) {
    public override void Execute(string senderId) {
        Blank? openBlank = (Blank?) Utils.GetForm(typeof(Blank));
        if (openBlank == null) {
            new Blank(true, false).Show();
            Cursor.Show();
        } else {
            openBlank.AddTime(true, false);
        }
    }
}