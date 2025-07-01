using System.Diagnostics;

namespace ControlApp.Commands;

public class WebsiteCommand(string content) : Command(Type.Website, content) {
    public override void Execute(string senderId) {
        string url = Utils.IsWebPage(content) ? content : "https://" + content;
        Process.Start(new ProcessStartInfo{
            FileName = url,
            UseShellExecute = true
        });
    }
}