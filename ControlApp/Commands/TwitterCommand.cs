using System.Diagnostics;

namespace ControlApp.Commands;

public class TwitterCommand(string content) : Command(Type.Twitter, content) {
    public override void Execute(string senderId) {
        Process.Start(new ProcessStartInfo{
            FileName = "https://x.com/intent/tweet?text=" + content.Replace(" ", "%20") + " [Posted by :]&url=www.thecontrolapp.co.uk",
            UseShellExecute = true
        });
    }
}