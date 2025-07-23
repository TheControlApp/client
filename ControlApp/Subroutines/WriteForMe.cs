using ControlApp.Commands;
using ControlApp.Services;
using ControlApp.Utils;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp.Subroutines;

public partial class WriteForMe : Form
{
	private int seconds;

	private int mistakes;

	private int count;

	private string senderId;
	private Timer timer = new Timer();

	public WriteForMe(string message, int times, string senderId) {
		InitializeComponent();
		inputBox.Text = message;
		count = times;
		countLabel.Text = count.ToString();
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

    private async Task SendCompletionMessage(string message)
    {
        var content = new { body = message };
        var command = new CommandStructure
        {
            Type = CommandCodes.PopupText,
            Content = JsonSerializer.SerializeToElement(content)
        };

        await WebSocketsCommunicator.SendCommandAsync(senderId, new List<CommandStructure> { command }, false);
        Close();
    }

    private async void input_KeyDown(object sender, KeyEventArgs e) {
        if (e.KeyData != Keys.Return) return;
        if (inputBox.Text == writeLabel.Text)
        {
            count--;
            countLabel.Text = count.ToString();
        }
        else
        {
            mistakes++;
            mistakeLabel.Text = mistakes.ToString();
        }
        inputBox.Text = "";
        if (count != 0) return;

        string successMessage = $"{AccountService.CurrentUser.Username} completed your command in {seconds} seconds with {mistakes} mistakes. Please Reward";
        await SendCompletionMessage(successMessage);
    }

	private async void button1_Click(object sender, EventArgs e) {
        string failMessage = $"{AccountService.CurrentUser.Username} failed your command after {seconds} seconds with {mistakes} mistakes. Please Punish";
        await SendCompletionMessage(failMessage);
    }
}
