using ControlApp.Services;
using ControlApp.Subroutines;
using ControlApp.Utils;
using Emgu.CV;
using System.Configuration;
using System.Text.Json;

namespace ControlApp.Commands;

public class WebcamCommand : Command {
    public WebcamCommand(): base(CommandCodes.Webcam) { }
    public override async void Execute(string senderId, JsonElement content) {   
        if (ConfigurationService.CommandSettings.General.MiscellaneousConfigs.WebcamCountdown) {
            new CustomMessage("Taking webcam picture in 5 seconds...", "", 5, false).Show();
        }
        string filename = "web" + MainWindow.username + senderId + DateTime.Now.ToString("yyyy-MM-dd") + ".jpg";
        string filePath = Path.Join(ConfigurationService.CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath, filename);
        try {
            using VideoCapture capture = new VideoCapture();
            using Bitmap image = capture.QueryFrame().ToBitmap();
            new Bitmap(image, new Size(Convert.ToUInt16(image.Width / 1.5), Convert.ToUInt16(image.Height / 1.5))).Save(filePath);
        } catch (Exception ex) {
            Utilities.LogError("Error getting webcam image: " + ex.Message);
            return;
        }

        if (!ServerCommunicator.SendFtpFile(filePath))
        {
            Utilities.LogError("Webcam image taken, but not sent due to connectivity error");
            return;
        }
        // Crée une commande "popup-media" pour afficher l'image de la webcam.
        var popupContent = new { url = $"https://www.thecontrolapp.co.uk/storage/{filename}" };
        var popupCommand = new CommandStructure
        {
            Type = CommandCodes.PopupMedia,
            Content = JsonSerializer.SerializeToElement(popupContent)
        };

        // Envoie la nouvelle commande via le WebSocket.
        await WebSocketsCommunicator.SendCommandAsync(senderId, new List<CommandStructure> { popupCommand }, false);

    }
}