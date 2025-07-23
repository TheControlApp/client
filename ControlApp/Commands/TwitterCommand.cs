using ControlApp.Exceptions.Commands;
using System.Configuration;
using System.Diagnostics;
using System.Text.Json;

namespace ControlApp.Commands;

//TODO
public class TwitterCommand : Command {
    public TwitterCommand(): base(CommandCodes.Twitter) { }
    public override void Execute(string senderId, JsonElement content) {
        string text = content.GetProperty("text").ToString() ?? throw new WrongCommandFormatException();
        text = text.Replace(" ", "%20");
        Process.Start(new ProcessStartInfo{
            FileName = "https://x.com/intent/tweet?text=" + text + " [Posted by :]&url=" + ConfigurationManager.AppSettings["SiteUrl"],
            UseShellExecute = true
        });
    }
}