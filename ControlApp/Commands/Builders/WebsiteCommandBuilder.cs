using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class WebsiteCommandBuilder() : SingleInputCommandBuilder("Website Command", "Website URL") {
    public override Command? BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"];
        string content = upperTextBox.Text;
        if (Strings.IsNullOrWhiteSpace(content) || !Utils.IsWebPage(content)) {
            MessageBox.Show("Please enter a valid URL.");
            return null;
        }
        upperTextBox.Clear();
        return new WebsiteCommand(content);
    }
}