using System.Speech.Synthesis;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class CustomMessage : Form {
	private static bool ttsDisabled;
	
	private readonly string messageText;

	private readonly string buttonText;

	private readonly int time;

	private readonly bool ttsCommand;

	private Timer timer;

	public static bool IsTtsDisabled() {
		return ttsDisabled;
	}

	public CustomMessage(string messageText, string buttonText, int time, bool ttsCommand) {
		if (ttsCommand && ttsDisabled) return;
		InitializeComponent();
		this.messageText = messageText;
		this.buttonText = buttonText;
		this.ttsCommand = ttsCommand;
		this.time = time;
	}

	private void CustomMessage_Load(object sender, EventArgs e) {
		if (ttsCommand) {
			Opacity = 0.0;
			SpeechSynthesizer speechSynthesizer;
			try {
				speechSynthesizer = new SpeechSynthesizer();
			} catch (PlatformNotSupportedException) {
				const string message = "TTS is not supported on this platform, so it will be turned off for this session";
				Utils.LogError(message);
				MessageBox.Show(message);
				ttsDisabled = true;
				return;
			}

			speechSynthesizer.SetOutputToDefaultAudioDevice();
			speechSynthesizer.Speak(messageText);
			speechSynthesizer.Dispose();
			Close();
			return;
		}
		messageLabel.Text = messageText;
		if (buttonText != "") {
			button.Text = buttonText;
		}
		if (time == 0) return;
		timer = new Timer();
		timer.Tick += delegate {
			Close();
		};
		timer.Interval = (int)TimeSpan.FromSeconds(time).TotalMilliseconds;
		timer.Start();
	}

	private void button_Click(object sender, EventArgs e) {
		Close();
	}

	public new void Show() {
		if (ttsCommand && ttsDisabled) return;
		base.Show();
	}
}
