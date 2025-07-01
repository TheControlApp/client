using System.Configuration;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class Popup : Form {
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
			axWindowsMediaPlayer1.ClickEvent += (_, _) => Close();
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
			axWindowsMediaPlayer1.Visible = true;
			axWindowsMediaPlayer1.URL = runningUrl;
			axWindowsMediaPlayer1.Ctlenabled = false;
			axWindowsMediaPlayer1.uiMode = "None";
			axWindowsMediaPlayer1.stretchToFit = true;
			axWindowsMediaPlayer1.settings.autoStart = true;
			axWindowsMediaPlayer1.settings.setMode("loop", varfMode: true);
		} else {
			Close();
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
		axWindowsMediaPlayer1.URL = runningUrl;
		axWindowsMediaPlayer1.Ctlenabled = false;
		axWindowsMediaPlayer1.uiMode = "None";
		axWindowsMediaPlayer1.stretchToFit = true;
		axWindowsMediaPlayer1.settings.autoStart = true;
		axWindowsMediaPlayer1.settings.setMode("loop", varfMode: true);
		timer.Start();
	}
}
