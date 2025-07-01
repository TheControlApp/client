using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class WriteForMe : Form
{
	private int seconds;

	private int mistakes;

	private int count;

	private string senderId;
	private Timer timer = new Timer();

	public WriteForMe(string message, string times, string senderId) {
		InitializeComponent();
		inputBox.Text = message;
		count = Convert.ToInt16(times);
		countLabel.Text = times;
		this.senderId = senderId;
	}

	private void WriteForMe_Load(object sender, EventArgs e)
	{
		seconds = 0;
		mistakes = 0;
		mistakeLabel.Text = mistakes.ToString();
		timer.Interval = 1000;
		timer.Tick += MyTimer_Tick;
		timer.Start();
	}

	private void MyTimer_Tick(object? sender, EventArgs e)
	{
		seconds++;
		timeLabel.Text = seconds.ToString();
	}

	private void input_KeyDown(object sender, KeyEventArgs e) {
		if (e.KeyData != Keys.Return) return;
		if (inputBox.Text == writeLabel.Text) {
			count--;
			countLabel.Text = count.ToString();
		} else {
			mistakes++;
			mistakeLabel.Text = mistakes.ToString();
		}
		inputBox.Text = "";
		if (count != 0) return;
		ServerCommunicator.SendCommand(senderId, Utils.Encrypt($"M={MainWindow.username} completed your command in {seconds} seconds with {mistakes} mistakes.&&&Please Reward"), groupSend: false);
		Close();
	}

	private void button1_Click(object sender, EventArgs e) {
		ServerCommunicator.SendCommand(senderId, Utils.Encrypt($"M={MainWindow.username} failed your command after {seconds} seconds with {mistakes} mistakes.&&&Please Punish"), groupSend: false);
		Close();
	}
}
