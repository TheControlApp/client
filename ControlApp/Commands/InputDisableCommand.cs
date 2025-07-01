using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class InputDisableCommand(string content) : Command(Type.InputDisable, content) {
    public override void Execute(string senderId) {
        Blank? openBlank = (Blank?) Utils.GetForm(typeof(Blank));
        if (openBlank == null) {
            new Blank(true, true).Show();
            Cursor.Show();
        } else {
            openBlank.AddTime(true, true);
        }
    }
}