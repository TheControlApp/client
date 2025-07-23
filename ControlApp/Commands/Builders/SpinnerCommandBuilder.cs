using ControlApp.Services;
using FluentFTP.Helpers;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ControlApp.Commands.Builders;

public class SpinnerCommandBuilder() : SingleInputCommandBuilder("Spinner Command", "Spinner Options") {
    public override void ConfigureInputPanel(Panel inputPanel) {
        base.ConfigureInputPanel(inputPanel);
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"]!;
        upperTextBox.Multiline = true;
        upperTextBox.Size = new Size(454, 212);
    }

    public override CommandStructure BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"]!;
        if (upperTextBox.Lines.Length == 0 || upperTextBox.Lines.Length == 1) {
            MessageBox.Show("Please enter some options into the spinner box.");
            return null;
        }
        List<string> optionList = new List<string>();
        foreach (string line in upperTextBox.Lines) {
            if (Strings.IsNullOrWhiteSpace(line)) continue;
            optionList.Add(line);
        }
        if (upperTextBox.Lines.Length > 10) {
            MessageBox.Show("Too many options for spinner");
            return null;
        }
        upperTextBox.Clear();
        // Check against the banned words list from the service.
        bool foundBannedWord = ServerConfigService.BannedWords.Any(word => optionList.Any(option => option.ToLower().Contains(word.ToLower())));
        if (foundBannedWord)
        {
            MessageBox.Show($"The message contains a banned word: '{foundBannedWord}'. Please remove it.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
        var content = new { options = optionList };
        return new CommandStructure
        {
            Type = CommandCodes.Spinner,
            Content = JsonSerializer.SerializeToElement(content)
        };
    }
}