using System.Runtime.InteropServices;
using System.Text.Json;
using ControlApp.Exceptions.Commands;
using ControlApp.Services;
using ControlApp.Utils;
using Microsoft.Win32;

namespace ControlApp.Commands;

public class WallpaperCommand : Command {
    
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_UPDATEINIFILE = 0x01;
    private const int SPIF_SENDWININICHANGE = 0x02;

    public WallpaperCommand(): base(CommandCodes.Wallpaper) { }
    public override void Execute(string senderId, JsonElement content) {
        string url = content.GetProperty("url").ToString() ?? throw new WrongCommandFormatException();
        string? filename = ServerCommunicator.GetFile(url);
        if (filename != null) ChangeWallpaper(filename);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    private void ChangeWallpaper(string filename) {
        char style = ConfigurationService.CommandSettings.WallPaper.Fitting == Models.WallpaperFitting.STRETCH ? '2' : '1';
        RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        if (key == null) {
            Utilities.LogError("Could not get registry key, wallpaper not changed!");
            return;
        }
        key.SetValue(@"TileWallpaper", 0.ToString());
        key.SetValue(@"WallpaperStyle", style);

        SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, filename, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
    }
}