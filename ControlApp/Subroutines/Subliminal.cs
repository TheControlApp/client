using AxWMPLib;
using ControlApp.Models;
using ControlApp.Services;
using ControlApp.Utils;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class Subliminal : Form // Maybe we can reformat this to extend Popup? They are more or less the same thing, with a few adjustments
{
	private static readonly Random randGen = Random.Shared;
	private Timer timer;

	private const int GWL_STYLE = -20;

	[DllImport("user32.dll", SetLastError = true)]
	private static extern uint GetWindowLong(nint hWnd, int nIndex);

	[DllImport("user32.dll")]
	private static extern int SetWindowLong(nint hWnd, int nIndex, uint dwNewLong);

	public Subliminal(string content, bool message) {
		InitializeComponent(content, message);
		StartPosition = FormStartPosition.CenterScreen;
		Screen[] my = Screen.AllScreens;
		Size = my[0].Bounds.Size;
		FormBorderStyle = FormBorderStyle.None;
		TopMost = true;
		Visible = true;
		uint initialStyle = GetWindowLong(Handle, GWL_STYLE);
		SetWindowLong(Handle, GWL_STYLE, initialStyle | 0x80000 | 0x20);
		timer = new Timer();
		timer.Tick += delegate {
			Close();
		};
		int timeUntilClose = 30;
        switch (ConfigurationService.CommandSettings.PopUps.PopupsDisplayLength)
        {
            case PopupsDisplayLength.LONG:
                timeUntilClose = randGen.Next(60, 601); // 2 à 10 minutes
                Utilities.LogInfo($"Subliminal (Long) will close in {timeUntilClose} seconds.");
                break;
            case PopupsDisplayLength.MEDIUM:
                timeUntilClose = randGen.Next(60, 301); // 60 secondes à 5 minutes
                Utilities.LogInfo($"Subliminal (Medium) will close in {timeUntilClose} seconds.");
                break;
            case PopupsDisplayLength.SHORT:
            default:
                timeUntilClose = randGen.Next(10, 61);  // 10 à 60 secondes
                Utilities.LogInfo($"Subliminal (Short) will close in {timeUntilClose} seconds.");
                break;
        }
        timer.Interval = (int)TimeSpan.FromSeconds(timeUntilClose).TotalMilliseconds;
        timer.Start();
	}

	private void InitializeComponent(string content, bool message) {
		SuspendLayout();
		if (message) {
			Label label1 = new Label();
			label1.Dock = DockStyle.Fill;
			label1.Font = new Font("Showcard Gothic", 50f);
			label1.Location = new Point(0, 0);
			label1.Name = "label1";
			label1.Size = new Size(800, 450);
			label1.TabIndex = 0;
			label1.Text = content;
			label1.TextAlign = ContentAlignment.MiddleCenter;
			Controls.Add(label1);
		} else {
			ComponentResourceManager resources = new ComponentResourceManager(typeof(Popup));
			AxWindowsMediaPlayer axWindowsMediaPlayer = new AxWindowsMediaPlayer();
			((ISupportInitialize)axWindowsMediaPlayer).BeginInit();
			axWindowsMediaPlayer.Dock = DockStyle.Fill;
			axWindowsMediaPlayer.Enabled = true;
			axWindowsMediaPlayer.Location = new Point(0, 0);
			axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
			axWindowsMediaPlayer.OcxState = (AxHost.State) resources.GetObject("axWindowsMediaPlayer1.OcxState")!;
			axWindowsMediaPlayer.Size = new Size(800, 450);
			axWindowsMediaPlayer.TabIndex = 0;
			axWindowsMediaPlayer.URL = content;
			axWindowsMediaPlayer.Ctlenabled = false;
			axWindowsMediaPlayer.uiMode = "None";
			axWindowsMediaPlayer.settings.autoStart = true;
			axWindowsMediaPlayer.settings.setMode("loop", varfMode: true);
			Controls.Add(axWindowsMediaPlayer);
		}
		AutoScaleDimensions = new SizeF(7f, 15f);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(800, 450);
		Name = "Subliminal";
		ShowIcon = false;
		ShowInTaskbar = false;
		Opacity = 0.05;
		Text = "Subliminal";
		ResumeLayout(false);
	}
}
