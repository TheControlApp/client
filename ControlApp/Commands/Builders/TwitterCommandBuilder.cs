using ControlApp.Services;
using FluentFTP.Helpers;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ControlApp.Commands.Builders;

public class TwitterCommandBuilder() : SingleInputCommandBuilder("Twitter Command", "Post Content") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        base.ConfigureInputPanel(inputPanel);
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"]!;
        upperTextBox.Multiline = true;
        upperTextBox.Size = new Size(454, 212);
    }
    
    public override CommandStructure BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"]!;
        if (Strings.IsNullOrWhiteSpace(upperTextBox.Text)) {
            MessageBox.Show("Post content cannot be empty.");
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
            Type = CommandCodes.Twitter,
            Content = JsonSerializer.SerializeToElement(content)
        };
    }
}