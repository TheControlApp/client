using ControlApp.Services;
using ControlApp.Utils;
using FluentFTP.Helpers;
using System.Security.Policy;
using System.Text.Json;

namespace ControlApp.Commands.Builders;

public class WatchForMeCommandBuilder() : FileCommandBuilder("Watch For Me Command", "File") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        string url;
        if (((RadioButton) inputPanel.Controls["fileRadioButton"]!).Checked) {
            TextBox fileNameTextBox = (TextBox) inputPanel.Controls["fileNameTextBox"]!;
            if (fileNameTextBox.Text == string.Empty) {
                MessageBox.Show("Please upload a file.");
                return null;
            }
            url = "FTP" + fileNameTextBox.Text;
            fileNameTextBox.Clear(); 
        }
        else // implies URL input
        {
            TextBox upperTextBox = (TextBox) inputPanel.Controls["upperTextBox"]!;
            url = upperTextBox.Text;
            if (Strings.IsNullOrWhiteSpace(url) || !Utilities.IsWebPage(url)) {
                MessageBox.Show("Please enter a valid URL.");
                return null;
            } else if (!Utilities.IsAnimatedFile(url) && !Utilities.IsImageFile(url)) { // I know the "else" here is redundant, but it emphasizes that these two clauses are mutually exclusive
                MessageBox.Show("File format not supported.");
                return null;
            }
            upperTextBox.Clear();
        }
        // Check if the URL contains any of the banned domains/sites.
        string? foundBannedSite = ServerConfigService.BannedSites.FirstOrDefault(site => url.ToLower().Contains(site.ToLower()));
        if (foundBannedSite != null)
        {
            MessageBox.Show($"The URL '{foundBannedSite}' is not allowed.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
        var content = new { url };
        return new CommandStructure
        {
            Type = CommandCodes.WatchForMe,
            Content = JsonSerializer.SerializeToElement(content)
        };
    }

    public override void ConfigureInputPanel(Panel inputPanel) {
        base.ConfigureInputPanel(inputPanel);
        ((OpenFileDialog) inputPanel.Container!.Components["openFileDialog"]!).Filter = "Video files (*.mpg;*.mpeg;*.mov;*.mp4;*.avi;*.webm)|*.mpg;*.mpeg;*.mov;*.mp4;*.avi;*.webm";
    }
}