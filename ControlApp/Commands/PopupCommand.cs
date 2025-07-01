using System.Configuration;
using ControlApp.Subroutines;

namespace ControlApp.Commands;

public class PopupCommand(string content) : Command(Type.Popup, content) {
    public override void Execute(string senderId) {
        Utils.LogInfo("Popup command content: " + content);
        string filename = GetLastItemFromUrl(content);
        if (Utils.IsAnimatedFile(filename) || Utils.IsImageFile(filename)) {
            string? filePath = ServerCommunicator.GetFile(content);
            if (filePath != null) {
                if (ConfigurationManager.AppSettings["PopStyle"] == "Parallel") {
                    new Popup(filePath).Show();
                } else {
                    Popup? openPopup = (Popup?) Utils.GetForm(typeof(Popup));
                    if (openPopup != null) {
                        openPopup.AddUrl(filePath);
                    }
                    else {
                        new Popup(filePath).Show();
                    }
                }
            }
        }
    }
}