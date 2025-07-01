using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class SubliminalLoopCommandBuilder() : FileCommandBuilder("Subliminal Loop Command", "Loop Item") {
    public override Command? BuildCommand(Panel inputPanel) {
        string content;
        if (((RadioButton)inputPanel.Controls["fileRadioButton"]).Checked) {
            TextBox fileNameTextBox = (TextBox)inputPanel.Controls["fileNameTextBox"];
            if (fileNameTextBox.Text == String.Empty) {
                MessageBox.Show("Please upload a file.");
                return null;
            } //TODO: Add filter to file selector
            content = "FTP" + fileNameTextBox.Text;
            fileNameTextBox.Clear();
        }
        else // implies URL input
        {
            TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"];
            content = upperTextBox.Text;
            if (Strings.IsNullOrWhiteSpace(content)) {
                MessageBox.Show("Please fill the text box.");
                return null;
            }
            upperTextBox.Clear();
        }
        return new SubliminalLoopCommand(content);
    }
}