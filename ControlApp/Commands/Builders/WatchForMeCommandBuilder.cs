using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class WatchForMeCommandBuilder() : FileCommandBuilder("Watch For Me Command", "File") {
    public override Command? BuildCommand(Panel inputPanel) {
        string content;
        if (((RadioButton) inputPanel.Controls["fileRadioButton"]).Checked) {
            TextBox fileNameTextBox = (TextBox) inputPanel.Controls["fileNameTextBox"];
            if (fileNameTextBox.Text == string.Empty) {
                MessageBox.Show("Please upload a file.");
                return null;
            }
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
        return new WatchForMeCommand(content);
    }

    public override void ConfigureInputPanel(Panel inputPanel) {
        base.ConfigureInputPanel(inputPanel);
        ((OpenFileDialog) inputPanel.Container.Components["openFileDialog"]).Filter = "Video files (*.mpg;*.mpeg;*.mov;*.mp4;*.avi;*.webm)|*.mpg;*.mpeg;*.mov;*.mp4;*.avi;*.webm";
    }
}