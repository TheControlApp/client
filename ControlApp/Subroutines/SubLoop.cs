using System.ComponentModel;
using System.Runtime.InteropServices;
using WMPLib;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class SubLoop : Form {
	private const int CHECK_INTERVAL = 250;
	
	public class LoopItem(LoopItem.Type type, string content) {
		public enum Type {
			Media,
			Text
		}
	
		public readonly Type type = type;
		public readonly string content = content;

		public static LoopItem? parseLoopItem(string inputString) {
			string[] splitInput = Utils.SeparateString(inputString);
			if (splitInput.Length == 2)
				return new LoopItem(splitInput[0] switch {
					"m" => Type.Media,
					_ => Type.Text
				}, splitInput[1]);
			Utils.LogError("Invalid subliminal loop element detected, skipping...");
			return null;
		}

        public override string ToString() {
            return $"[{(type == Type.Media ? 'm' : 't')}],[{content}]";
        }
    }

	private const int GWL_STYLE = -20;

	private static List<LoopItem> loopList = new List<LoopItem>();

	private static bool running;

	private static string subliminalListPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "ConstantSubList.txt");

	private int position;

	private static Timer timer = new Timer();

	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowLong(nint hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(nint hWnd, int nIndex, uint dwNewLong);

	public SubLoop() {
		InitializeComponent();
		label.Text = "";
		if (File.Exists(subliminalListPath)) {
			string[] lines = File.ReadAllLines(subliminalListPath);
			foreach (string line in lines) {
				LoopItem? item = LoopItem.parseLoopItem(line);
				if (item != null) loopList.Add(item);
			}
		}
		Console.WriteLine($"Loop list contains {loopList.Count} items");
		timer.Tick += LoopThrough;
		timer.Interval = CHECK_INTERVAL;
		uint initialStyle = GetWindowLong(Handle, -20);
		SetWindowLong(Handle, GWL_STYLE, initialStyle | 0x80000 | 0x20);
		BackColor = Color.Magenta;
		TransparencyKey = Color.Magenta;
		if (loopList.Count > 0) timer.Start();
		running = true;
	}

	private void LoopThrough(object? sender, EventArgs e) {
		if (!running || (axWindowsMediaPlayer.playState != WMPPlayState.wmppsStopped &&
		                 axWindowsMediaPlayer.playState != 0)) return;
		DoItem(loopList[position]);
		position = (position + 1) % loopList.Count;
	}

	public static void AddItem(string item) {
		LoopItem loopItem;
		if (!Utils.IsWebPage(item)) {
			loopItem = new LoopItem(LoopItem.Type.Text, item);
		} else {
			string? filename = ServerCommunicator.GetFile(item);
			if (filename != null) {
				loopItem = new LoopItem(LoopItem.Type.Media, filename);
			} else {
				return;
			}
		}
		if (running) {
			loopList.Add(loopItem);
		} else {
            using StreamWriter writer = File.AppendText(subliminalListPath); writer.WriteLine(loopItem.ToString());
            if (loopList.Count != 1) return;
            running = true;
			timer.Start();
		}
	}

	private void DoItem(LoopItem item) {
		if (item.type == LoopItem.Type.Text) {
			label.Visible = true;
			label.Text = item.content;
			axWindowsMediaPlayer.Visible = false;
		} else {
			label.Visible = false;
			axWindowsMediaPlayer.Visible = true;
			axWindowsMediaPlayer.URL = item.content;
			axWindowsMediaPlayer.Ctlenabled = false;
			axWindowsMediaPlayer.stretchToFit = true;
			axWindowsMediaPlayer.uiMode = "None";
			axWindowsMediaPlayer.settings.autoStart = true;
			axWindowsMediaPlayer.settings.volume = 25;
		}
	}

	protected override void OnClosing(CancelEventArgs e) {
		running = false;
		File.Delete(subliminalListPath);
		using (StreamWriter writer = File.AppendText(subliminalListPath)) {
			foreach (LoopItem item in loopList) {
				writer.WriteLine(item.ToString());
			}
		}
		timer.Stop();
	}
}
