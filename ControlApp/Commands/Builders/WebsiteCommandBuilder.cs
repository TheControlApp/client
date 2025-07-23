using ControlApp.Services;
using ControlApp.Utils;
using FluentFTP.Helpers;
using System.Security.Policy;
using System.Text.Json;

namespace ControlApp.Commands.Builders;

public class WebsiteCommandBuilder() : SingleInputCommandBuilder("Website Command", "Website URL") {
    public override CommandStructure BuildCommand(Panel inputPanel) {
        TextBox upperTextBox = (TextBox)inputPanel.Controls["upperTextBox"]!;
        string url = upperTextBox.Text;
        if (Strings.IsNullOrWhiteSpace(url) || !Utilities.IsWebPage(url)) {
            MessageBox.Show("Please enter a valid URL.");
            return null;
        }
        upperTextBox.Clear();
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
            Type = CommandCodes.Website,
            Content = JsonSerializer.SerializeToElement(content)
        };
    }
}