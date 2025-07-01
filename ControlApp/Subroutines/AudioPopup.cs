using WMPLib;

namespace ControlApp.Subroutines;

public partial class AudioPopup : Form {
    private string localFileUrl;

    public AudioPopup(string fileUrl) {
        localFileUrl = fileUrl;
        InitializeComponent();
    }

    private void AudioPopup_Load(object sender, EventArgs e) {
        IWMPPlayer4 windowsMediaPlayerClass = new WindowsMediaPlayerClass();
        windowsMediaPlayerClass.URL = localFileUrl;
        windowsMediaPlayerClass.controls.play();
        Close();
    }
}