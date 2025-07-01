using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class TTSCommand(string content) : Command(Type.TTS, content) {
  public override void Execute(string senderId) {
      if (CustomMessage.IsTtsDisabled()) return;
	  new CustomMessage(content, "", 3, true).Show();
  }
}