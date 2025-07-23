using ControlApp.Exceptions.Commands;
using ControlApp.Subroutines;
using System.Text.Json;

namespace ControlApp.Commands;

public class TTSCommand : Command {
    public TTSCommand(): base(CommandCodes.TTS) { }
  public override void Execute(string senderId, JsonElement content) {
      if (CustomMessage.IsTtsDisabled()) return;
        string text = content.GetProperty("text").ToString() ?? throw new WrongCommandFormatException();
        new CustomMessage(text, "", 3, true).Show();
  }
}