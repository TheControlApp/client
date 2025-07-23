using System.Text.Json.Serialization;

namespace ControlApp.Models;

/// <summary>
/// Represents the dynamic configuration data fetched from the server.
/// </summary>
public class ServerConfig
{
    [JsonPropertyName("bannedWords")]
    public List<string> BannedWords { get; set; }

    [JsonPropertyName("bannedSites")]
    public List<string> BannedSites { get; set; }
}