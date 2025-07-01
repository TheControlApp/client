using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class Blank : Form {
	private int lockIntervals;

	private Timer timer = new Timer();

	private LockMouse mouseLock = new LockMouse();

	private LockKeyboard keyboardLock = new LockKeyboard();

	private bool lockMouse;
	private bool lockKeyboard;

	private void RefreshLockStates() {
		if (lockMouse && !mouseLock.IsLocked()) {
			mouseLock.Lock();
		}
		if (lockKeyboard && !keyboardLock.IsLocked()) {
			keyboardLock.Lock();
		}
	}

	private void ReleaseLocks() {
		if (mouseLock.IsLocked()) mouseLock.Unlock();
		if (keyboardLock.IsLocked()) keyboardLock.Unlock();
	}

	public Blank(bool lockMouse, bool lockKeyboard) {
		timer.Interval = (int)TimeSpan.FromSeconds(10.0).TotalMilliseconds;
		timer.Tick += closeWindow;
		this.lockMouse = lockMouse;
		this.lockKeyboard = lockKeyboard;
		InitializeComponent();
		lockIntervals = 1;
	}

	private void closeWindow(object? sender, EventArgs e) {
		lockIntervals--;
		if (lockIntervals == 0) {
			ReleaseLocks();
			Close();
		} else {
			RefreshLockStates();
		}
	}

	public void AddTime(bool lockMouseArg, bool lockKeyboardArg) {
		lockMouse |= lockMouseArg;
		lockKeyboard |= lockKeyboardArg;
		lockIntervals++;
	}

	private void Blank_Load(object sender, EventArgs e) {
		Opacity = 0.001;
		StartPosition = FormStartPosition.CenterScreen;
		Screen[] my = Screen.AllScreens;
		Size = my[0].Bounds.Size;
		FormBorderStyle = FormBorderStyle.None;
		TopMost = true;
		Visible = true;
		if (lockMouse) {
			mouseLock.Lock();
		}
		if (lockKeyboard) {
			keyboardLock.Lock();
		}
		timer.Start();
	}

	private void Blank_FormClosing(object sender, FormClosingEventArgs e) {
		timer.Stop();
		ReleaseLocks();
	}
}
