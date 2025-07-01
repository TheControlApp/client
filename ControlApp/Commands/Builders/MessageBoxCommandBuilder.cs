using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class MessageBoxCommandBuilder() : CommandBuilder("Message Box Command") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        Label upperLabel = (Label)inputPanel.Controls["upperLabel"];
        upperLabel.Text = "Message Box Text";
        upperLabel.Show();
        inputPanel.Controls["upperTextBox"].Show();
        Label lowerLabel = (Label)inputPanel.Controls["lowerLabel"];
        lowerLabel.Text = "Close Button Text";
        lowerLabel.Show();
        inputPanel.Controls["upperTextBox"].Show();
        inputPanel.Controls["lowerTextBox"].Show();
    }

    public override Command? BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"];
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Message box cannot be empty.");
            return null;
        }
        TextBox lowerTextBox = (TextBox) inputPanel.Controls["lowerTextBox"];
        string content = $"{upperTextBox.Text}&&&{lowerTextBox.Text}";
        upperTextBox.Clear();
        lowerTextBox.Clear();
        return new MessageBoxCommand(content);
    }
}