using ControlApp.Commands;
using ControlApp.Commands.Builders;
using ControlApp.Services;
using ControlApp.Utils;
using FluentFTP.Helpers;
using System.Configuration;
using System.Text.Json;

namespace ControlApp;

public partial class CommandBuilderTab : TabPage {
    // The list of available command builders for the UI ComboBox.
    private readonly List<CommandBuilder> buildersList =
    [
        new DummyCommandBuilder("Select Command"),
        new AudioCommandBuilder(),
        new DownloadCommandBuilder(),
        new InputDisableCommandBuilder(),
        new MessageBoxCommandBuilder(),
        new MouseDisableCommandBuilder(),
        new PopupCommandBuilder(),
        new RunnableCommandBuilder(),
        new ScreenshotCommandBuilder(),
        new SendDeleteCommandBuilder(),
        new SpinnerCommandBuilder(),
        new SubliminalImageCommandBuilder(),
        new SubliminalLoopCommandBuilder(),
        new SubliminalTextCommandBuilder(),
        new TTSCommandBuilder(),
        new TwitterCommandBuilder(),
        new WallpaperCommandBuilder(),
        new WatchForMeCommandBuilder(),
        new WebcamCommandBuilder(),
        new WebsiteCommandBuilder(),
        new WriteForMeCommandBuilder()
    ];

    private List<CommandStructure> commandList = [];
    
    public CommandBuilderTab() {
        InitializeComponent();
    }

    /// <summary>
    /// Builds a command from the selected builder and adds it to the list.
    /// The UI is updated to show the JSON of the added command.
    /// </summary>
    private void addCommandButton_Click(object sender, EventArgs e)
    {
        if (commandCombo.SelectedItem is not CommandBuilder selectedBuilder) return;

        CommandStructure? builtCommand = selectedBuilder.BuildCommand(inputPanel);
        if (builtCommand == null) return; // Build failed, likely due to invalid user input.

        // Handle commands with special restrictions.
        switch (builtCommand.Type)
        {
            case CommandCodes.InputDisable:
                groupCombo.SelectedIndex = 8; // Auto-select "Extreme" group
                groupCombo.Enabled = false;
                break;
            case CommandCodes.Screenshot:
                if (commandList.Any(cmd => cmd.Type == CommandCodes.Screenshot))
                {
                    MessageBox.Show("A payload cannot contain more than one screenshot command.", "Restriction", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                break;
        }

        commandList.Add(builtCommand);

        // Update the display with a pretty-printed JSON of the command.
        string commandJson = JsonSerializer.Serialize(builtCommand, new JsonSerializerOptions { WriteIndented = true });
        commandDisplay.AppendText(commandJson + Environment.NewLine + "----------" + Environment.NewLine);
    }

    /// <summary>
    /// Clears the list of commands and resets the UI.
    /// </summary>
    private void clearCommandsButton_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Are you sure you want to delete ALL built commands?", "Confirm Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        commandList.Clear();
        commandDisplay.Clear();
        groupCombo.Enabled = true; // Re-enable group combo if it was disabled.
    }

    /// <summary>
    /// Sends the queued commands to the specified user or group.
    /// Uses the modern WebSocket communicator.
    /// </summary>
    private async void sendCommandButton_Click(object sender, EventArgs e)
    {
        if (!commandList.Any())
        {
            MessageBox.Show("There are no commands to send.", "Empty Payload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        bool groupSelected = groupCombo.SelectedIndex > 0;
        string destination = groupSelected ? (-(groupCombo.SelectedIndex + 1)).ToString() : destUsernameCombo.Text;

        if (string.IsNullOrWhiteSpace(destination))
        {
            MessageBox.Show("Please select a user or group to send the command to.", "No Destination", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Send the command list via the WebSocket. The communicator handles payload creation and serialization.
        await WebSocketsCommunicator.SendCommandAsync(destination, [.. commandList], groupSelected);
        MessageBox.Show("Command payload sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // Clear the list and UI for the next payload.
        commandList.Clear();
        commandDisplay.Clear();
        destUsernameCombo.Text = "";
        groupCombo.SelectedIndex = 0;
        groupCombo.Enabled = true;
    }

    /// <summary>
    /// Responds to the last user who sent a command, or sends to a group if one is selected.
    /// </summary>
    private async void respondButton_Click(object sender, EventArgs e)
    {
        if (!commandList.Any())
        {
            MessageBox.Show("There are no commands to respond with.", "Empty Payload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // The "Respond" button's behavior changes based on whether a group is selected.
        // This replicates the original application's logic.
        bool groupSelected = groupCombo.SelectedIndex > 0;

        if (groupSelected)
        {
            // If a group is selected, send to the group.
            string group = (-(groupCombo.SelectedIndex + 1)).ToString();
            await WebSocketsCommunicator.SendCommandAsync(group, new List<CommandStructure>(commandList), true);
            MessageBox.Show($"Command payload sent to group '{groupCombo.Text}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            // If no group is selected, respond to the last sender.
            var lastSender = CommandManager.LastExecutedPayload?.Payload.SenderUsername;
            if (string.IsNullOrEmpty(lastSender) || lastSender == "-1")
            {
                MessageBox.Show("No previous command sender to respond to.", "No Recipient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            await WebSocketsCommunicator.SendCommandAsync(lastSender, new List<CommandStructure>(commandList), false);
            MessageBox.Show($"Response sent successfully to '{lastSender}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Clear the list and UI for the next payload.
        commandList.Clear();
        commandDisplay.Clear();
        groupCombo.Enabled = true;
    }

    #region UI Configuration and Event Handlers (Unchanged)

    private void destUsernameCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        groupCombo.Text = string.Empty;
        groupCombo.SelectedIndex = 0;
    }

    private void groupCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        destUsernameCombo.Text = string.Empty;
    }

    private void fileUploadButton_Click(object sender, EventArgs e)
    {
        if (AccountService.CurrentUser?.IsVerified ?? false)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;
                fileNameTextBox.Text = Path.GetFileName(fullPath);
                // The Webform for upload is legacy but kept for compatibility.
                new Webform(ConfigurationManager.AppSettings["SiteUrl"] + "upload.aspx?file=" + fullPath).Show();
            }
            else
            {
                fileNameTextBox.Text = "";
            }
        }
        else
        {
            MessageBox.Show("File upload is only available for verified users.", "Verification Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void commandCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (Control control in inputPanel.Controls)
        {
            control.Hide();
        }

        if (commandCombo.SelectedItem is not CommandBuilder selectedBuilder || commandCombo.SelectedIndex == 0)
        {
            addCommandButton.Hide();
            clearCommandsButton.Location = clearCommandsButton.Location with { X = (inputPanel.Width - clearCommandsButton.Width) / 2 };
            return;
        }

        upperTextBox.Multiline = false;
        upperTextBox.Size = new Size(454, 23);
        selectedBuilder.ConfigureInputPanel(inputPanel);

        foreach (Control control in inputPanel.Controls)
        {
            if (!control.Visible && control is TextBox textBox) textBox.Clear();
        }
        addCommandButton.Show();
        clearCommandsButton.Location = clearCommandsButton.Location with { X = 297 };
    }

    private void fileRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        upperLabel.Show();
        bool radioButtonState = fileRadioButton.Checked;
        urlRadioButton.Checked = !radioButtonState;
        fileNameTextBox.Visible = radioButtonState;
        fileUploadButton.Visible = radioButtonState;
        upperTextBox.Visible = !radioButtonState;
    }

    private void urlRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        upperLabel.Show();
        bool radioButtonState = urlRadioButton.Checked;
        fileRadioButton.Checked = !radioButtonState;
        fileNameTextBox.Visible = !radioButtonState;
        fileUploadButton.Visible = !radioButtonState;
        upperTextBox.Visible = radioButtonState;
    }

    #endregion
}