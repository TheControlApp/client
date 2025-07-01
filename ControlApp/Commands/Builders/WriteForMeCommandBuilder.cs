using FluentFTP.Helpers;

namespace ControlApp.Commands.Builders;

public class WriteForMeCommandBuilder() : CommandBuilder("Write For Me Command") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        Label upperLabel = (Label) inputPanel.Controls["upperLabel"];
        upperLabel.Text = "Writing Task Text";
        upperLabel.Show();
        inputPanel.Controls["upperTextBox"].Show();
        Label lowerLabel = (Label) inputPanel.Controls["lowerLabel"];
        lowerLabel.Text = "Writing Task Count";
        lowerLabel.Show();
        inputPanel.Controls["upperTextBox"].Show();
        inputPanel.Controls["lowerSpinner"].Show();
    }

    public override Command? BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"];
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Writing task text cannot be empty.");
            return null;
        }
        NumericUpDown lowerSpinner = (NumericUpDown) inputPanel.Controls["lowerSpinner"];
        string content = $"{upperTextBox.Text}&&&{lowerSpinner.Value}";
        upperTextBox.Clear();
        lowerSpinner.Value = 1;
        return new SubliminalTextCommand(content);
    }
}