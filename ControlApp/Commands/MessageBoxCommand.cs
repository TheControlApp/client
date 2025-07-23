using System.Text.Json;
using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class MessageBoxCommand : Command
{
    public MessageBoxCommand() : base(CommandCodes.PopupText) { }

    public override void Execute(string senderId, JsonElement content)
    {
        string body = content.GetProperty("body").GetString() ?? throw new WrongCommandFormatException();
        string buttonText = "Close"; // Default value

        // buttonText is optional
        if (content.TryGetProperty("buttonText", out JsonElement btnElement))
        {
            buttonText = btnElement.GetString() ?? throw new WrongCommandFormatException();
        }

        if (bannedWords.Exists(word => body.Contains(word)))
        {
            new CustomMessage("Message contains blacklisted terms, skipping...", string.Empty, 4, false).Show();
            return;
        }

        new CustomMessage(body, buttonText, 0, false).Show();
    }
}