using ControlApp.Commands;
using ControlApp.Services;
using ControlApp.Utils;
using System.Configuration;

namespace ControlApp;

public partial class MainWindow : Form
{
    public static string? username = ConfigurationManager.AppSettings["UserName"];
    public static bool verified;


    // Update handler signature
    private void OnCommandReceived(CommandPayload command)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnCommandReceived(command)));
            return;
        }
        if (ConfigurationService.CommandSettings.General.MiscellaneousConfigs.RunAllOutstanding)
        {
            Utilities.LogInfo($"Executing command '{command.GetType().Name}' from '{command.SenderUsername}' via WebSocket.");
            remainingCommandLabel.Text = command.SenderUsername;
            command.Execute();
        }

    }

    private void OnWebSocketConnected()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(OnWebSocketConnected));
            return;
        }
        statusLabel.Text = "Connected";
        statusLabel.ForeColor = Color.Green;
        Utilities.LogInfo("UI Updated: WebSocket Connected");
    }

    private void OnWebSocketDisconnected()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(OnWebSocketDisconnected));
            return;
        }
        statusLabel.Text = "Disconnected";
        statusLabel.ForeColor = Color.Red;
        Utilities.LogWarning("UI Updated: WebSocket Disconnected");
    }

    public MainWindow()
    {
        InitializeComponent();
        verified = false;
    }

    public void RefreshCredentialCache()
    {
        usernameInput.Text = ConfigurationManager.AppSettings["UserName"];
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (e.CloseReason != CloseReason.UserClosing && e.CloseReason != CloseReason.None) return;
        e.Cancel = true;
        Hide();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        usernameInput.Text = username;
    }

    #region UI Event handlers
    private async void OnClickClearOutstandingCommands(object sender, EventArgs e)
    {
        if (MessageBox.Show("Are you sure?", "Clear Outstanding", MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            Utilities.LogInfo("Clearing command queue");
            await CommandManager.ClearQueueAndNotifyServerAsync();
        }
    }

    private async void OnClickRunNextCommand(object sender, EventArgs e)
    {
        Utilities.LogInfo("Running next command");
        await CommandManager.ExecuteNextPayloadAsync();
        thumbsUpButton.Enabled = true;
    }

    private async void OnClickRunLastCommand(object sender, EventArgs e)
    {
        Utilities.LogInfo("Running last command");
        await CommandManager.ExecuteLastPayloadAsync();
    }

    private async void OnClickRunAllCommands(object sender, EventArgs e)
    {
        Utilities.LogInfo("Running all commands");
        await CommandManager.ExecuteAllPayloadsAsync();
    }

    private async void OnClickBlockLastCommandSender(object sender, EventArgs e)
    {
        // Confirm with the user before taking a destructive action.
        if (MessageBox.Show("Are you sure you want to block the last command's sender? This action cannot be undone for now.",
                            "Confirm Block",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            // Disable the button to prevent multiple clicks while the operation is in progress.
            blockSenderButton.Enabled = false;

            // Call the CommandManager to handle the logic.
            var (success, message) = await CommandManager.BlockLastCommandSender();

            // Show feedback to the user based on the result.
            MessageBoxIcon icon = success ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            MessageBox.Show(message, "Block Status", MessageBoxButtons.OK, icon);

            // Re-enable the button after the operation is complete.
            blockSenderButton.Enabled = true;
        }
    }

    private async void OnClickReportLastCommandSender(object sender, EventArgs e)
    {
        // Confirm with the user before reporting.
        if (MessageBox.Show("Are you sure you want to report the last command's sender? This will send the command's content to the administrators for review.",
                            "Confirm Report",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            reportSenderButton.Enabled = false;

            // Call the CommandManager to handle all the logic.
            var (success, message) = await CommandManager.ReportLastCommandSenderAsync();

            // Show feedback to the user.
            MessageBoxIcon icon = success ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            MessageBox.Show(message, "Report Status", MessageBoxButtons.OK, icon);

            reportSenderButton.Enabled = true;
        }
    }

    private async void OnClickLikeLastCommand(object sender, EventArgs e)
    {
        Utilities.LogInfo("Like command");
        thumbsUpButton.Enabled = false;

        // Call the CommandManager to handle all the logic.
        var (success, message) = await CommandManager.LikeLastCommandSenderAsync();

        //TODO: replace that by some temporary non-intrusive alert ?
        MessageBoxIcon icon = success ? MessageBoxIcon.Information : MessageBoxIcon.Error;
        MessageBox.Show(message, "Thumbs Up", MessageBoxButtons.OK, icon);

        UpdateCommandUI();
    }
    
    private void OnClickLinkDiscordToYourAccount(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using AccountSettingsForm configForm = new();
        configForm.ShowDialog();
    }

    private void OnClickOpenAccountConfig(object sender, EventArgs e)
    {
        using (AccountSettingsForm configForm = new())
        {
            configForm.ShowDialog();
        }
        RefreshCredentialCache();
    }

    private void OnClickOpenCommandConfig(object sender, EventArgs e)
    {
        using CommandOptionsForm optionsForm = new();
        optionsForm.ShowDialog();
    }
    private void OnClickOpenBlockList(object sender, EventArgs e)
    {
        using (ContactsList blockList = new())
        {
            blockList.ShowDialog();
        }
        sendCommandTab.PopulateDestUserList();
    }
    #endregion

}
