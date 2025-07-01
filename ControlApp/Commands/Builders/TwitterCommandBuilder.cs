using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class TwitterCommandBuilder() : SingleInputCommandBuilder("Twitter Command", "Post Content") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        base.ConfigureInputPanel(inputPanel);
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"];
        upperTextBox.Multiline = true;
        upperTextBox.Size = new Size(454, 212);
    }
    
    public override Command? BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"];
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Post content cannot be empty.");
            return null;
        }
        string content = upperTextBox.Text;
        upperTextBox.Clear();
        return new SubliminalTextCommand(content);
    }
}