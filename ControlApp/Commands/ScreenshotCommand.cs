using System.Configuration;
using System.Drawing.Imaging;

namespace ControlApp.Commands;

public class ScreenshotCommand(string content) : Command(Type.Screenshot, content) {
    public override void Execute(string senderId) {
        if (Screen.PrimaryScreen == null)
            throw new InvalidOperationException("Screenshots are not supported in a headless environment");
        string screenshotName = "scr" + MainWindow.username + senderId + DateTime.Now.ToString("yyyy-MM-dd") + ".jpg";
        string filePath = Path.Join(ConfigurationManager.AppSettings["LocalDrive"], screenshotName);
        using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)) {
            using (Graphics g = Graphics.FromImage(bmpScreenCapture)) {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, bmpScreenCapture.Size, CopyPixelOperation.SourceCopy);
            }
            new Bitmap(bmpScreenCapture, new Size(bmpScreenCapture.Width / 2, bmpScreenCapture.Height / 2)).Save(filePath, ImageFormat.Jpeg);
        }

        if (!ServerCommunicator.SendFtpFile(filePath)) return;
        ServerCommunicator.SendCommand(senderId, Utils.Encrypt("U=FTP" + screenshotName), false);
        MessageBox.Show("Screen shot taken :D");
    }
}