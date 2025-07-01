using System.Configuration;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ControlApp.Commands;

public class WallpaperCommand(string content) : Command(Type.Wallpaper, content) {
    
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDWININICHANGE = 0x02;

    public override void Execute(string senderId) {
        string? filename = ServerCommunicator.GetFile(content);
        if (filename != null) ChangeWallpaper(filename);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    private void ChangeWallpaper(string filename) {
        string? style = ConfigurationManager.AppSettings["PaperStyle"];
        RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        if (key == null) {
            Utils.LogError("Could not get registry key, wallpaper not changed!");
            return;
        }
        key.SetValue(@"TileWallpaper", 0.ToString());
        key.SetValue(@"WallpaperStyle", style == "Stretch" ? '2' : '1');

        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
    }
}