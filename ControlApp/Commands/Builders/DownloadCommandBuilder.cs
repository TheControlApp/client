using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class DownloadCommandBuilder() : FileCommandBuilder("Download Command", "Source File") {
    public override Command? BuildCommand(Panel inputPanel) {
        if (((RadioButton)inputPanel.Controls["fileRadioButton"]).Checked) {
            TextBox fileNameTextBox = (TextBox)inputPanel.Controls["fileNameTextBox"];
            if (fileNameTextBox.Text == String.Empty) {
                MessageBox.Show("Please upload a file.");
                return null;
            }
            string content = "FTP" + fileNameTextBox.Text;
            fileNameTextBox.Clear();
            return new DownloadCommand(content);
        }
        else // implies URL input
        {
            TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"];
            string content = upperTextBox.Text;
            if (Strings.IsNullOrWhiteSpace(content) || !Utils.IsWebPage(content)) {
                MessageBox.Show("Please enter a URL.");
                return null;
            }
            upperTextBox.Clear();
            return new AudioCommand(content);
        }
    }
}