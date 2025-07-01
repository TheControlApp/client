using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class WriteForMeCommand(string content) : Command(Type.WriteForMe, content) {
    public override void Execute(string senderId) {
        foreach (string element in bannedWords) {
            if (!content.Contains(element)) continue;
            new CustomMessage("Writing task contains blacklisted terms, skipping", string.Empty, 3, false).Show();
            return;
        }
        string[] splitCommand = content.Split("&&&");
        new WriteForMe(splitCommand[0], splitCommand[1], senderId).Show();
    }
}