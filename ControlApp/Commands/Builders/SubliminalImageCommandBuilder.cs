using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class SubliminalImageCommandBuilder() : FileCommandBuilder("Subliminal Image Command", "Image/Video Source") {
    public override Command? BuildCommand(Panel inputPanel) {
        string content;
        if (((RadioButton) inputPanel.Controls["fileRadioButton"]).Checked) {
            TextBox fileNameTextBox = (TextBox) inputPanel.Controls["fileNameTextBox"];
            if (fileNameTextBox.Text == String.Empty) {
                MessageBox.Show("Please upload a file.");
                return null;
            } //TODO: Add filter to file selector
            content = "FTP" + fileNameTextBox.Text;
            fileNameTextBox.Clear(); 
        }
        else // implies URL input
        {
            TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"];
            content = upperTextBox.Text;
            if (Strings.IsNullOrWhiteSpace(content) || !Utils.IsWebPage(content)) {
                MessageBox.Show("Please enter a valid URL.");
                return null;
            } else if (!Utils.IsAnimatedFile(content) && !Utils.IsImageFile(content)) { // I know the "else" here is redundant, but it emphasizes that these two clauses are mutually exclusive
                MessageBox.Show("File format not supported.");
                return null;
            }
            upperTextBox.Clear();
        }
        return new SubliminalImageCommand(content);
    }
}