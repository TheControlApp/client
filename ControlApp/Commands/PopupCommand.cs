using ControlApp.Exceptions;
using ControlApp.Exceptions.Commands;
using ControlApp.Services;
using ControlApp.Subroutines;
using ControlApp.Utils;
using System.Configuration;
using System.Text.Json;

namespace ControlApp.Commands;

public class PopupCommand : Command {

    public PopupCommand() : base(CommandCodes.PopupMedia) { }

    public override void Execute(string senderId, JsonElement content)
    {
        Utilities.LogInfo("Popup command content: " + content);

        string url = content.GetProperty("url").GetString() ?? throw new WrongCommandFormatException();

        // FTP shortcut logic remains for compatibility for now
        if (url.StartsWith("FTP"))
        {
            url = $"https://www.thecontrolapp.co.uk/storage/{url.Substring(3)}";
        }

        string filename = Utilities.GetLastItemFromUrl(url);
        if (!Utilities.IsAnimatedFile(filename) && !Utilities.IsImageFile(filename))
        {
            return;
        }
        string? filePath = ServerCommunicator.GetFile(url);
        if (filePath == null)
        {
            return;
        }
        if (ConfigurationService.CommandSettings.PopUps.PopupsDisplayBehavior == Models.PopupsDisplayBehavior.ALL)
        {
            new Popup(filePath).Show();
            return;
        }
        Popup? openPopup = (Popup?)Utilities.GetForm(typeof(Popup));
        if (openPopup != null)
        {
            openPopup.AddUrl(filePath);
        }
        else
        {
            new Popup(filePath).Show();
        }
    }
}