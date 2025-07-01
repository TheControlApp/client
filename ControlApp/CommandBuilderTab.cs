using ControlApp.Commands;
using ControlApp.Commands.Builders;
using FluentFTP.Helpers;

namespace ControlApp;

public partial class CommandBuilderTab : TabPage {
    private readonly string COMMAND_SEPARATOR = "|||";
    
    private readonly List<CommandBuilder> buildersList = new CommandBuilder[] {
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
    }.ToList();

    private List<Command> commandList = new List<Command>();
    
    public CommandBuilderTab() {
        InitializeComponent();
    }

    private void addCommandButton_Click(object sender, EventArgs e) {
        if (commandCombo.SelectedItem == null) throw new InvalidOperationException("Add command button clicked even though it should not be accessible at this time");
        Command? builtCommand = ((CommandBuilder) commandCombo.SelectedItem).BuildCommand(inputPanel);
        if (builtCommand == null) return;
        switch (builtCommand.type) {
            case Command.Type.InputDisable: {
                groupCombo.SelectedIndex = 8; // Extreme
                groupCombo.Enabled = false;
                break;
            }
            case Command.Type.Screenshot: {
                foreach (Command command in commandList) { // yes, screenshot is the only restricted command, but
                    if (command.type == builtCommand.type) { // using variable instead of hardcoding to make it easy to add more restricted commands
                        MessageBox.Show($"One command cannot have more than one {command.type} commands");
                        return;
                    }
                }
                break;
            }
        }
        commandList.Add(builtCommand);
        commandDisplay.Text += (commandDisplay.Text.Length == 0 ? builtCommand.GetType().Name : '\n' + builtCommand.GetType().Name);
    }

    private void clearCommandsButton_Click(object sender, EventArgs e) {
        if (MessageBox.Show("Are you sure you want to delete ALL built commands?", "Deletion Dialog",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        commandList.Clear();
        commandDisplay.Clear();
    }

    private void destUsernameCombo_SelectedIndexChanged(object sender, EventArgs e) {
        groupCombo.Text = String.Empty;
        groupCombo.SelectedIndex = 0;
    }

    private void groupCombo_SelectedIndexChanged(object sender, EventArgs e) {
        destUsernameCombo.Text = String.Empty;
    }

    private int GetGroup() {
        return (groupCombo.SelectedIndex + 1) * -1;
    }

    private void sendCommandButton_Click(object sender, EventArgs e) {
        bool groupSelected = groupCombo.SelectedIndex != 0 && groupCombo.SelectedIndex != -1;
        string destination = groupSelected ? GetGroup().ToString() : destUsernameCombo.Text;
        if (ServerCommunicator.SendCommand(destination, BuildCommandString(commandList, true), groupSelected)) {
            MessageBox.Show("Command successfully sent!");
        } else {
            MessageBox.Show("Command could not be sent!");
        }
        destUsernameCombo.Text = "";
        groupCombo.SelectedIndex = 0;
        groupCombo.Enabled = true;
    }

    private void respondButton_Click(object sender, EventArgs e) {
        string? lastSenderId = MainWindow.GetLastSenderId();
        if (Strings.IsNullOrWhiteSpace(lastSenderId)) {
            MessageBox.Show("You cannot respond to any user because you have not received a command yet.");
            return;
        }
        string command = BuildCommandString(commandList, true);
        if (groupCombo.SelectedIndex == 0) {
            if (lastSenderId != "-1") { // respond to specific user
                ServerCommunicator.SendCommand(lastSenderId, command, false);
            }
        } else if (Convert.ToInt16(lastSenderId) >= -1) { // TODO: How are IDs assigned? Can they ever be lower than -1?
            string group = GetGroup().ToString();
            ServerCommunicator.SendCommand(group, command, true);
        }
    }

    private string BuildCommandString(List<Command> argCommandList, bool clear) {
        string output = String.Empty;
        foreach (Command command in argCommandList) {
            output += $"{command.GetCode()}={command.content}" + COMMAND_SEPARATOR;
        }
        if (output.Length != 0) output.Remove(output.Length - COMMAND_SEPARATOR.Length);
        if (!clear) return output;
        argCommandList.Clear();
        commandDisplay.Clear();
        return output;
    }

    private void fileUploadButton_Click(object sender, EventArgs e) {
        if (MainWindow.verified) {
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string fullPath = openFileDialog.FileName;
                fileNameTextBox.Text = Path.GetFileName(fullPath);
                new Webform("https://www.thecontrolapp.co.uk/upload.aspx?file=" + fullPath).Show();
            } else {
                fileNameTextBox.Text = "";
            }
        } else {
            MessageBox.Show("Only allowed for verified users");
        }
    }

    private void commandCombo_SelectedIndexChanged(object sender, EventArgs e) {
        foreach (Control control in inputPanel.Controls) {
            control.Hide();
        }
        if (commandCombo.SelectedIndex == 0) {
            addCommandButton.Hide();
            return;
        }
        upperTextBox.Multiline = false;
        upperTextBox.Size = new Size(454, 23);
        if (commandCombo.SelectedItem == null) throw new InvalidOperationException("Command combo selection removed before end of method call"); // code should never reach here
        ((CommandBuilder) commandCombo.SelectedItem).ConfigureInputPanel(inputPanel);
        foreach (Control control in inputPanel.Controls) {
            if (!control.Visible && control is TextBox textBox) textBox.Clear();
        }
        addCommandButton.Show();
    }

    private void fileRadioButton_CheckedChanged(object sender, EventArgs e) {
        upperLabel.Show();
        bool radioButtonState = fileRadioButton.Checked;
        urlRadioButton.Checked = !radioButtonState;
        fileNameTextBox.Visible = radioButtonState;
        fileUploadButton.Visible = radioButtonState;
        upperTextBox.Visible = !radioButtonState;
    }

    private void urlRadioButton_CheckedChanged(object sender, EventArgs e) {
        upperLabel.Show();
        bool radioButtonState = urlRadioButton.Checked;
        fileRadioButton.Checked = !radioButtonState;
        fileNameTextBox.Visible = !radioButtonState;
        fileUploadButton.Visible = !radioButtonState;
        upperTextBox.Visible = radioButtonState;
    }
}