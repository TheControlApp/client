using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class MessageBoxCommand(string content) : Command(Type.MessageBox, content) {
    public override void Execute(string senderId) {
        foreach (string element in bannedWords) {
            if (!content.Contains(element)) continue;
            new CustomMessage("Message contains blacklisted terms, skipping...", String.Empty, 4, false).Show();
            return;
        }
        string[] splitCommand = content.Split("&&&");
        new CustomMessage(splitCommand[0], (splitCommand.Length > 1) ? splitCommand[1] : "", 0 , false).Show();
    }
}