using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using System.Text.Json;

namespace ControlApp.Commands;

public class WriteForMeCommand : Command {
    public WriteForMeCommand(): base(CommandCodes.WriteForMe) { }
    public override void Execute(string senderId, JsonElement content) {
        string text = content.GetProperty("text").ToString() ?? throw new WrongCommandFormatException();
        int count = content.GetProperty("count").GetInt32();

        foreach (string element in bannedWords) {
            if (!text.Contains(element)) continue;
            new CustomMessage("Writing task contains blacklisted terms, skipping", string.Empty, 3, false).Show();
            return;
        }
        new WriteForMe(text, count, senderId).Show();
    }
}