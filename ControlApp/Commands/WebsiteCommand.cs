using ControlApp.Exceptions.Commands;
using ControlApp.Utils;
using System.Diagnostics;
using System.Text.Json;

namespace ControlApp.Commands;

public class WebsiteCommand : Command {
    public WebsiteCommand(): base(CommandCodes.Website) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        if(!Utilities.IsWebPage(url)) 
            url = "https://" + url;
        Process.Start(new ProcessStartInfo{
            FileName = url,
            UseShellExecute = true
        });
    }
}