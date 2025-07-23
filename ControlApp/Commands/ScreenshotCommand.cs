using ControlApp.Services;
using ControlApp.Utils;
using System.Configuration;
using System.Drawing.Imaging;
using System.Text.Json;

namespace ControlApp.Commands;

public class ScreenshotCommand : Command {
    public ScreenshotCommand() : base(CommandCodes.Screenshot) { }
    public override async void Execute(string senderId, JsonElement content) {
        if (Screen.PrimaryScreen == null)
            throw new InvalidOperationException("Screenshots are not supported in a headless environment");
        string screenshotName = "scr" + MainWindow.username + senderId + DateTime.Now.ToString("yyyy-MM-dd") + ".jpg";
        string filePath = Path.Join(ConfigurationService.CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath, screenshotName);
        using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)) {
            using (Graphics g = Graphics.FromImage(bmpScreenCapture)) {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
            }
            new Bitmap(bmpScreenCapture, new Size(bmpScreenCapture.Width / 2, bmpScreenCapture.Height / 2)).Save(filePath, ImageFormat.Jpeg);
        }

        if (!ServerCommunicator.SendFtpFile(filePath)) return;
        // Crée une commande "popup-media" pour afficher la capture d'écran chez l'expéditeur.
        var popupContent = new { url = $"https://www.thecontrolapp.co.uk/storage/{screenshotName}" };
        var popupCommand = new CommandStructure
        {
            Type = CommandCodes.PopupMedia,
            Content = JsonSerializer.SerializeToElement(popupContent)
        };

        // Envoie la nouvelle commande via le WebSocket.
        await WebSocketsCommunicator.SendCommandAsync(senderId, new List<CommandStructure> { popupCommand }, false);
        MessageBox.Show("Screen shot taken :D");
    }
}