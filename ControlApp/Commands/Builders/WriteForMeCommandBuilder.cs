using ControlApp.Services;
using FluentFTP.Helpers;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ControlApp.Commands.Builders;

public class WriteForMeCommandBuilder() : CommandBuilder("Write For Me Command") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        Label upperLabel = (Label) inputPanel.Controls["upperLabel"]!;
        upperLabel.Text = "Writing Task Text";
        upperLabel.Show();
        inputPanel.Controls["upperTextBox"]!.Show();
        Label lowerLabel = (Label) inputPanel.Controls["lowerLabel"]!;
        lowerLabel.Text = "Writing Task Count";
        lowerLabel.Show();
        inputPanel.Controls["upperTextBox"]!.Show();
        inputPanel.Controls["lowerSpinner"]!.Show();
    }

    public override CommandStructure BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"]!;
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Writing task text cannot be empty.");
            return null;
        }
        NumericUpDown lowerSpinner = (NumericUpDown) inputPanel.Controls["lowerSpinner"]!;
        string text = upperTextBox.Text;
        int count = (int)lowerSpinner.Value;
        upperTextBox.Clear();
        lowerSpinner.Value = 1;
        // Check against the banned words list from the service.
        string? foundBannedWord = ServerConfigService.BannedWords.FirstOrDefault(word => text.ToLower().Contains(word.ToLower()));
        if (foundBannedWord != null)
        {
            MessageBox.Show($"The message contains a banned word: '{foundBannedWord}'. Please remove it.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
        var content = new { text, count };
        return new CommandStructure
        {
            Type = CommandCodes.WriteForMe,
            Content = JsonSerializer.SerializeToElement(content)
        };
    }
}