using ControlApp.Services;
using FluentFTP.Helpers;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ControlApp.Commands.Builders;

public class MessageBoxCommandBuilder() : CommandBuilder("Message Box Command") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        Label upperLabel = (Label)inputPanel.Controls["upperLabel"]!;
        upperLabel.Text = "Message Box Text";
        upperLabel.Show();
        inputPanel.Controls["upperTextBox"]!.Show();
        Label lowerLabel = (Label)inputPanel.Controls["lowerLabel"]!;
        lowerLabel.Text = "Close Button Text";
        lowerLabel.Show();
        inputPanel.Controls["upperTextBox"]!.Show();
        inputPanel.Controls["lowerTextBox"]!.Show();
    }

    public override CommandStructure BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"]!;
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Message box cannot be empty.");
            return null;
        }
        string text = upperTextBox.Text;
        upperTextBox.Clear();
        // Check against the banned words list from the service.
        string? foundBannedWord = ServerConfigService.BannedWords.FirstOrDefault(word => text.ToLower().Contains(word.ToLower()));
        if (foundBannedWord != null)
        {
            MessageBox.Show($"The message contains a banned word: '{foundBannedWord}'. Please remove it.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
        var content = new { text };
        return new CommandStructure
        {
            Type = CommandCodes.PopupText,
            Content = JsonSerializer.SerializeToElement(content)
        };
    }
}