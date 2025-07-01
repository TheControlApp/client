using System.Runtime.InteropServices;
using FluentFTP.Helpers;

namespace ControlApp.Subroutines;

public partial class WatchForMe : Form {
	private readonly string senderId;

	private int timeWatched;

	private int timeCensored;

	private bool active;

	private int lostFocus;

	private bool censored;

	[DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool InternetSetOption(nint hInternet, int dwOption, string lpBuffer, int lpdwBufferLength);

	private static void SetUserAgent(string userAgent) {
		InternetSetOption(IntPtr.Zero, 41, userAgent, userAgent.Length);
	}

	private void RefreshTitle() {
		Text = $"Watched for={timeWatched}, Censored for={timeCensored}, Lost focus={lostFocus}";
	}

	public WatchForMe(string url, string senderId) {
		this.senderId = senderId;
		SetUserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/109.0");
		InitializeComponent();
		switch (Path.GetExtension(url)) {
			case ".mp4":
			case ".webm":
			case ".webp":
			case ".gif":
				axWindowsMediaPlayer.Ctlenabled = false;
				axWindowsMediaPlayer.uiMode = "None";
				axWindowsMediaPlayer.settings.autoStart = true;
				axWindowsMediaPlayer.settings.setMode("loop", varfMode: true);
				FileInfo fileInfo = new FileInfo(url);
				if (Utils.IsWebPage(url)) {
					axWindowsMediaPlayer.URL = ServerCommunicator.GetFile(url) != null ? fileInfo.ToString() : url;
				} else {
					axWindowsMediaPlayer.URL = fileInfo.ToString();
				}
				webView21.Visible = false;
				break;
			case ".png":
			case ".jpg":
			case ".jpeg": {
				webView21.Visible = false;
				string? file = ServerCommunicator.GetFile(url);
				if (file == null) {
					Close();
					break;
				}
				Image i = Image.FromFile(file);
				Screen my = Screen.AllScreens[0];
				Width = Math.Min(i.Width, my.Bounds.Width);
				Height = Math.Min(i.Height, my.Bounds.Height);
				axWindowsMediaPlayer.Visible = false;
				PictureBox pictureBox = new PictureBox {
					Dock = DockStyle.Fill,
					Image = i
				};
				Controls.Add(pictureBox);
				break;
			}
			default: {
				axWindowsMediaPlayer.Visible = false;
				if (url.StartsWithCI("HTTP")) {
					url = "https://" + url;
				}
				Uri uri = new Uri(url);
				webView21.Source = uri;
				break;
			}
		}
		AutoSize = true;
		timeWatched = 0;
		timeCensored = 0;
		lostFocus = 0;
		watchTimer.Interval = 1000;
		watchTimer.Tick += (_, _) => {
			if (!active) return;
			++timeWatched;
			RefreshTitle();
		};
		watchTimer.Start();
		censorTimer.Interval = 1000;
		censorTimer.Tick += (_, _) => {
			if (!active) return;
			++timeCensored;
			RefreshTitle();
		};
		active = false;
		Deactivate += (_, _) => active = false;
		Activated += Form1_Reactivate;
		FormClosing += Form1_FormClosing;
	}

	private void Form1_Reactivate(object? sender, EventArgs e) {
		if (active) return;
		lostFocus++;
		active = true;
		RefreshTitle();
	}

	private void Form1_FormClosing(object? sender, FormClosingEventArgs e) {
		if (e.CloseReason == CloseReason.UserClosing) {
			ServerCommunicator.SendCommand(senderId, Utils.Encrypt($"M=User :  {MainWindow.username} watched for : {timeWatched} seconds and censored for : {timeCensored} seconds. Form lost focus {lostFocus} times"), groupSend: false);
		}
	}

	private void WFM_KeyDown(object sender, KeyEventArgs e) {
		if (e.KeyCode != Keys.C) return;
		if (censored) {
			Opacity = 1.0;
			censored = false;
			censorTimer.Stop();
		} else {
			Opacity = 0.0;
			censored = true;
			censorTimer.Start();
		}
	}

	private void axWindowsMediaPlayer_KeyPress(object sender, KeyPressEventArgs e) {
		if (e.KeyChar != 'c') return;
		if (censored) {
			Opacity = 1.0;
			censored = false;
			censorTimer.Stop();
		} else {
			Opacity = 0.0;
			censored = true;
			censorTimer.Start();
		}
	}
}
