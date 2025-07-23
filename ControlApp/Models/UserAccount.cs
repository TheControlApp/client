using System.Text.Json.Serialization;

namespace ControlApp.Models;

public class UserAccount
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("isVerified")]
    public bool IsVerified { get; set; }
    [JsonPropertyName("blackList")]
    public List<string> BlackList { get; set; }
    [JsonPropertyName("knownList")]
    public List<string> KnownList { get; set; }
}
