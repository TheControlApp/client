using System.Configuration;
using System.Runtime.InteropServices;
using AxWMPLib;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class Popup : Form {
	private const int POPUP_WIDTH = 800;
	private const int POPUP_HEIGHT = 450;
	private static readonly Random randGen = Random.Shared;
	
	private string runningUrl;

	private readonly Timer timer = new Timer();

	private const int GWL_STYLE = -20;

	private readonly Queue<string> urls = new Queue<string>();

	private readonly char[] popupPropertyArray;

	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowLong(nint hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(nint hWnd, int nIndex, uint dwNewLong);

	public Popup(string url) {
		InitializeComponent();
		string popupPropertyString = ConfigurationManager.AppSettings["PopType"] ?? "nnnn";
		Utils.LogInfo("Popup type: " + popupPropertyString);
		popupPropertyArray = popupPropertyString.ToCharArray();
		if (popupPropertyArray[0] == 's') {
			Opacity = 0.5;
		}
		if (popupPropertyArray[1] == 't') {
			uint initialStyle = GetWindowLong(Handle, GWL_STYLE);
			SetWindowLong(Handle, GWL_STYLE, initialStyle | 0x80000 | 0x20);
		} else if (popupPropertyArray[1] == 'c') {
			Click += (_, _) => Close();
			axWindowsMediaPlayer.ClickEvent += (_, _) => Close();
		}
		if (popupPropertyArray[2] == 'm' && popupPropertyArray[3] != 'n') {
			Timer moveTimer = new Timer();
			moveTimer.Interval = (int)TimeSpan.FromSeconds(2.0).TotalMilliseconds;
			moveTimer.Tick += ChangePos;
			moveTimer.Start();
		}
		runningUrl = url;
		timer.Tick += ContentFinished;
		if (ConfigurationManager.AppSettings["PopSet"] == "Long") {
			int timeUntilClose = randGen.Next(9) + 1;
			timer.Interval = (int)TimeSpan.FromMinutes(timeUntilClose).TotalMilliseconds;
			Utils.LogInfo($"Popup will close in {timeUntilClose} minutes");
		} else {
			int timeUntilClose = randGen.Next(30) + 30;
			timer.Interval = (int)TimeSpan.FromSeconds(timeUntilClose).TotalMilliseconds;
			Utils.LogInfo($"Popup will close in {timeUntilClose} seconds");
		}
	}

	public void AddUrl(string url) {
		urls.Enqueue(url);
	}

	private void ChangePos(object? sender, EventArgs e) {
		Random random = new Random();
		if (Screen.PrimaryScreen == null) throw new InvalidOperationException("Popups cannot be triggered in a headless environment");
		int screenWidth = Screen.PrimaryScreen.Bounds.Width;
		int screenHeight = Screen.PrimaryScreen.Bounds.Height;
		int randomX = random.Next(0, screenWidth - Width);
		int randomY = random.Next(0, screenHeight - Height);
		Location = new Point(randomX, randomY);
	}

	private void ContentFinished(object? sender, EventArgs e) {
		if (urls.Count > 0) {
			runningUrl = urls.Dequeue();
			axWindowsMediaPlayer.URL = runningUrl;
		} else {
			Close();
		}
	}

	private void axWMP_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e) {
		if (e.newState == 3) { // signals "Playing", there's no enum for this, docs also do it with integers: https://learn.microsoft.com/en-us/previous-versions/windows/desktop/wmp/axwmplib-axwindowsmediaplayer-playstatechange
			ResizePopup();
		}
	}

	private void PopUp_Load(object sender, EventArgs e) {
		if (popupPropertyArray[3] == 'f') {
			WindowState = FormWindowState.Maximized;
		} else {
			Random random = new Random();
			if (Screen.PrimaryScreen == null) throw new InvalidOperationException("Popups cannot be triggered in a headless environment");
			int screenWidth = Screen.PrimaryScreen.Bounds.Width;
			int screenHeight = Screen.PrimaryScreen.Bounds.Height;
			int randomX = random.Next(0, screenWidth - Width);
			int randomY = random.Next(0, screenHeight - Height);
			StartPosition = FormStartPosition.Manual;
			Location = new Point(randomX, randomY);
		}
		axWindowsMediaPlayer.URL = runningUrl;
		axWindowsMediaPlayer.Ctlenabled = false;
		axWindowsMediaPlayer.uiMode = "None";
		axWindowsMediaPlayer.stretchToFit = true;
		axWindowsMediaPlayer.settings.autoStart = true;
		axWindowsMediaPlayer.settings.setMode("loop", varfMode: true);
		timer.Start();
	}

	private void ResizePopup() {
		int sourceWidth = axWindowsMediaPlayer.currentMedia.imageSourceWidth;
		int sourceHeight = axWindowsMediaPlayer.currentMedia.imageSourceHeight;
		double widthRatio = (double) POPUP_WIDTH / sourceWidth;
		double heightRatio = (double) POPUP_HEIGHT / sourceHeight;
		ClientSize = widthRatio < heightRatio 
			? new Size(POPUP_WIDTH, (int) (sourceHeight * widthRatio)) 
			: new Size((int)(sourceWidth * heightRatio), POPUP_HEIGHT);
	}
}
