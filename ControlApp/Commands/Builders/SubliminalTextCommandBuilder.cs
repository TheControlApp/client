using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class SubliminalTextCommandBuilder() : SingleInputCommandBuilder("Subliminal Text Command", "Subliminal Text") {
    public override Command? BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"];
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Subliminal text cannot be empty.");
            return null;
        }
        string content = upperTextBox.Text;
        upperTextBox.Clear();
        return new SubliminalTextCommand(content);
    }
}