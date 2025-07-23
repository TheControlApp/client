using AxWMPLib;
using ControlApp.Models;
using ControlApp.Services;
using ControlApp.Utils;
using System.Configuration;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class Popup : Form {
    // --- Constantes pour les appels P/Invoke ---
    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x80000;
    private const uint WS_EX_TRANSPARENT = 0x20;


    // --- Champs privés ---
    private static readonly Random randGen = Random.Shared;
    private readonly Timer timer = new();
    private readonly Queue<string> urls = new();
    private string runningUrl;
    private const int POPUP_WIDTH = 800;
    private const int POPUP_HEIGHT = 450;

    [DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowLong(nint hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(nint hWnd, int nIndex, uint dwNewLong);

    /// <summary>
    /// Initialise une nouvelle instance de la fenêtre Popup avec une configuration moderne.
    /// </summary>
    /// <param name="fileUrl">Le chemin local du fichier multimédia à afficher.</param>
    public Popup(string fileUrl)
    {
        InitializeComponent();

        // --- Chargement de la configuration ---
        var popupSettings = ConfigurationService.CommandSettings.PopUps;
        var panelConfig = popupSettings.PanelConfig;

        Utilities.LogInfo("Configuring new Popup from modern ConfigurationService settings.");

        // 1. Configurer l'opacité
        if (panelConfig.PopupsOpacity == PanelPopupsOpacity.SEETHROUGH)
        {
            Opacity = 0.5;
        }

        // 2. Configurer la cliquabilité
        switch (panelConfig.PopupsClickability)
        {
            case PanelPopupsClickability.CLICKTHROUGH:
                // Rend la fenêtre non-interactive (les clics passent "à travers")
                uint initialStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
                SetWindowLong(this.Handle, GWL_EXSTYLE, initialStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
                break;
            case PanelPopupsClickability.CLICKABLE:
                // Permet de fermer la fenêtre en cliquant dessus
                this.Click += (_, _) => this.Close();
                axWindowsMediaPlayer.ClickEvent += (_, _) => this.Close();
                break;
                // Le cas STANDARD ne nécessite aucune action.
        }

        // 3. Configurer le mouvement
        if (panelConfig.PopupsMovement == PanelPopupsMovement.MOVING)
        {
            Timer moveTimer = new() { Interval = 2000 }; // Se déplace toutes les 2 secondes
            moveTimer.Tick += ChangePos;
            moveTimer.Start();
        }

        // 4. Configurer la durée du popup
        runningUrl = fileUrl;
        timer.Tick += ContentFinished;
        int timeUntilClose;

        switch (popupSettings.PopupsDisplayLength)
        {
            case PopupsDisplayLength.LONG:
                timeUntilClose = randGen.Next(60, 601); // 2 à 10 minutes
                Utilities.LogInfo($"Popup (Long) will close in {timeUntilClose} seconds.");
                break;
            case PopupsDisplayLength.MEDIUM:
                timeUntilClose = randGen.Next(60, 301); // 60 secondes à 5 minutes
                Utilities.LogInfo($"Popup (Medium) will close in {timeUntilClose} seconds.");
                break;
            case PopupsDisplayLength.SHORT:
            default:
                timeUntilClose = randGen.Next(10, 61);  // 10 à 60 secondes
                Utilities.LogInfo($"Popup (Short) will close in {timeUntilClose} seconds.");
                break;
        }
        timer.Interval = (int)TimeSpan.FromSeconds(timeUntilClose).TotalMilliseconds;
    }

    /// <summary>
    /// Ajoute une URL à la file d'attente pour être lue après le média actuel.
    /// Utilisé lorsque le comportement d'affichage est "One at a time" (un par un).
    /// </summary>
    public void AddUrl(string url)
    {
        urls.Enqueue(url);
    }

    private void ChangePos(object? sender, EventArgs e)
    {
        if (Screen.PrimaryScreen == null) return;
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        int randomX = randGen.Next(0, screenWidth - Width);
        int randomY = randGen.Next(0, screenHeight - Height);
        this.Location = new Point(randomX, randomY);
    }

    private void ContentFinished(object? sender, EventArgs e)
    {
        if (urls.TryDequeue(out string? nextUrl))
        {
            runningUrl = nextUrl;
            axWindowsMediaPlayer.URL = runningUrl;
        }
        else
        {
            this.Close();
        }
    }

    private void axWMP_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
    {
        // 3 = WMPPlayState.wmppsPlaying. L'énumération n'est pas toujours disponible.
        if (e.newState == 3)
        {
            ResizePopup();
        }
    }

    private void PopUp_Load(object sender, EventArgs e)
    {
        var panelConfig = ConfigurationService.CommandSettings.PopUps.PanelConfig;

        // Gère le mode plein écran ou la position aléatoire
        if (panelConfig.PopupsFullScreen)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        else
        {
            if (Screen.PrimaryScreen == null) throw new InvalidOperationException("Popups cannot be triggered in a headless environment");
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int randomX = randGen.Next(0, screenWidth - Width);
            int randomY = randGen.Next(0, screenHeight - Height);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(randomX, randomY);
        }

        // Configure le lecteur Windows Media Player
        axWindowsMediaPlayer.URL = runningUrl;
        axWindowsMediaPlayer.Ctlenabled = false;
        axWindowsMediaPlayer.uiMode = "None";
        axWindowsMediaPlayer.stretchToFit = true;
        axWindowsMediaPlayer.settings.autoStart = true;
        axWindowsMediaPlayer.settings.setMode("loop", true);

        timer.Start();
    }

    private void ResizePopup()
    {
        if (axWindowsMediaPlayer.currentMedia == null || axWindowsMediaPlayer.currentMedia.imageSourceWidth == 0)
        {
            return; // Pas d'information sur la taille, on ne redimensionne pas
        }

        int sourceWidth = axWindowsMediaPlayer.currentMedia.imageSourceWidth;
        int sourceHeight = axWindowsMediaPlayer.currentMedia.imageSourceHeight;

        double widthRatio = (double)POPUP_WIDTH / sourceWidth;
        double heightRatio = (double)POPUP_HEIGHT / sourceHeight;

        // Applique le ratio le plus petit pour que l'image tienne entièrement
        double ratio = Math.Min(widthRatio, heightRatio);

        ClientSize = new Size((int)(sourceWidth * ratio), (int)(sourceHeight * ratio));
    }
}
