using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
using ControlApp.Services;
using ControlApp.Subroutines;
using ControlApp.Utils;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp;

partial class MainWindow {
	private readonly Size PANEL_SIZE = new Size(817, 444);
	
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>

    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        settingToolStripMenuItem = new ToolStripMenuItem();
        accountConfigToolStripMenuItem = new ToolStripMenuItem();
        commandConfigToolStripMenuItem = new ToolStripMenuItem();
        otherToolStripMenuItem = new ToolStripMenuItem();
        historyToolStripMenuItem = new ToolStripMenuItem();
        listToolStripMenuItem = new ToolStripMenuItem();
        scoreLabel = new Label();
        scoreInput = new TextBox();
        tabControl = new TabControl();
        mainTab = new TabPage();
        runAllButton = new Button();
        label2 = new Label();
        lastUserCommandLabel = new Label();
        remainingCommandLabel = new Label();
        thumbsUpButton = new Button();
        runLastButton = new Button();
        clearOutstandingButton = new Button();
        nextUserTextLabel = new Label();
        nextUserCommandLabel = new Label();
        reportSenderButton = new Button();
        blockSenderButton = new Button();
        runNextButton = new Button();
        commandCountLabel = new Label();
        sendCommandTab = new CommandBuilderTab();
        linkLabelVerification = new LinkLabel();
        usernameTextLabel = new Label();
        statusLabel = new Label();
        usernameInput = new TextBox();
        menuStrip1.SuspendLayout();
        tabControl.SuspendLayout();
        mainTab.SuspendLayout();
        sendCommandTab.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { settingToolStripMenuItem, historyToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(845, 24);
        menuStrip1.TabIndex = 8;
        menuStrip1.Text = "menuStrip1";
        // 
        // settingToolStripMenuItem
        // 
        settingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { accountConfigToolStripMenuItem, commandConfigToolStripMenuItem, otherToolStripMenuItem });
        settingToolStripMenuItem.Name = "settingToolStripMenuItem";
        settingToolStripMenuItem.Size = new Size(61, 20);
        settingToolStripMenuItem.Text = "Settings";
        // 
        // accountConfigToolStripMenuItem
        // 
        accountConfigToolStripMenuItem.Name = "accountConfigToolStripMenuItem";
        accountConfigToolStripMenuItem.Size = new Size(131, 22);
        accountConfigToolStripMenuItem.Text = "Account";
        accountConfigToolStripMenuItem.Click += OnClickOpenAccountConfig;
        // 
        // commandConfigToolStripMenuItem
        // 
        commandConfigToolStripMenuItem.Name = "commandConfigToolStripMenuItem";
        commandConfigToolStripMenuItem.Size = new Size(131, 22);
        commandConfigToolStripMenuItem.Text = "Command";
        commandConfigToolStripMenuItem.Click += OnClickOpenCommandConfig;
        // 
        // otherToolStripMenuItem
        // 
        otherToolStripMenuItem.Name = "otherToolStripMenuItem";
        otherToolStripMenuItem.Size = new Size(131, 22);
        otherToolStripMenuItem.Text = "Blocks";
        otherToolStripMenuItem.Click += OnClickOpenBlockList;
        // 
        // historyToolStripMenuItem
        // 
        historyToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { listToolStripMenuItem });
        historyToolStripMenuItem.Enabled = false;
        historyToolStripMenuItem.Name = "historyToolStripMenuItem";
        historyToolStripMenuItem.Size = new Size(57, 20);
        historyToolStripMenuItem.Text = "History";
        // 
        // listToolStripMenuItem
        // 
        listToolStripMenuItem.Enabled = false;
        listToolStripMenuItem.Name = "listToolStripMenuItem";
        listToolStripMenuItem.Size = new Size(92, 22);
        listToolStripMenuItem.Text = "List";
        // 
        // scoreLabel
        // 
        scoreLabel.AutoSize = true;
        scoreLabel.Location = new Point(652, 40);
        scoreLabel.Name = "scoreLabel";
        scoreLabel.Size = new Size(63, 15);
        scoreLabel.TabIndex = 34;
        scoreLabel.Text = "Your Score";
        // 
        // scoreInput
        // 
        scoreInput.Location = new Point(714, 37);
        scoreInput.Name = "scoreInput";
        scoreInput.ReadOnly = true;
        scoreInput.Size = new Size(100, 23);
        scoreInput.TabIndex = 35;
        // 
        // tabControl
        // 
        tabControl.Controls.Add(mainTab);
        tabControl.Controls.Add(sendCommandTab);
        tabControl.Location = new Point(10, 66);
        tabControl.Multiline = true;
        tabControl.Name = "tabControl";
        tabControl.SelectedIndex = 0;
        tabControl.Size = new Size(825, 472);
        tabControl.TabIndex = 32;
        // 
        // mainTab
        // 
        mainTab.Controls.Add(runAllButton);
        mainTab.Controls.Add(label2);
        mainTab.Controls.Add(lastUserCommandLabel);
        mainTab.Controls.Add(remainingCommandLabel);
        mainTab.Controls.Add(thumbsUpButton);
        mainTab.Controls.Add(runLastButton);
        mainTab.Controls.Add(clearOutstandingButton);
        mainTab.Controls.Add(nextUserTextLabel);
        mainTab.Controls.Add(nextUserCommandLabel);
        mainTab.Controls.Add(reportSenderButton);
        mainTab.Controls.Add(blockSenderButton);
        mainTab.Controls.Add(runNextButton);
        mainTab.Controls.Add(commandCountLabel);
        mainTab.Location = new Point(4, 24);
        mainTab.Name = "mainTab";
        mainTab.Padding = new Padding(3);
        mainTab.Size = new Size(817, 444);
        mainTab.TabIndex = 0;
        mainTab.Text = "Received Commands";
        mainTab.UseVisualStyleBackColor = true;
        // 
        // runAllButton
        // 
        runAllButton.Location = new Point(239, 111);
        runAllButton.Name = "runAllButton";
        runAllButton.Size = new Size(113, 23);
        runAllButton.TabIndex = 17;
        runAllButton.Text = "Run All";
        runAllButton.UseVisualStyleBackColor = true;
        runAllButton.Click += OnClickRunAllCommands;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(51, 86);
        label2.Name = "label2";
        label2.Size = new Size(80, 15);
        label2.TabIndex = 16;
        label2.Text = "Next is from : ";
        // 
        // lastUserCommandLabel
        // 
        lastUserCommandLabel.AutoSize = true;
        lastUserCommandLabel.Location = new Point(134, 86);
        lastUserCommandLabel.Name = "lastUserCommandLabel";
        lastUserCommandLabel.Size = new Size(50, 15);
        lastUserCommandLabel.TabIndex = 15;
        lastUserCommandLabel.Text = "No Data";
        // 
        // remainingCommandLabel
        // 
        remainingCommandLabel.AutoSize = true;
        remainingCommandLabel.Location = new Point(134, 28);
        remainingCommandLabel.Name = "remainingCommandLabel";
        remainingCommandLabel.Size = new Size(50, 15);
        remainingCommandLabel.TabIndex = 14;
        remainingCommandLabel.Text = "No Data";
        // 
        // thumbsUpButton
        // 
        thumbsUpButton.Location = new Point(383, 82);
        thumbsUpButton.Name = "thumbsUpButton";
        thumbsUpButton.Size = new Size(113, 23);
        thumbsUpButton.TabIndex = 13;
        thumbsUpButton.Text = "Like Sender";
        thumbsUpButton.UseVisualStyleBackColor = true;
        thumbsUpButton.Click += OnClickLikeLastCommand;
        // 
        // runLastButton
        // 
        runLastButton.Location = new Point(239, 82);
        runLastButton.Name = "runLastButton";
        runLastButton.Size = new Size(113, 23);
        runLastButton.TabIndex = 12;
        runLastButton.Text = "Run Last";
        runLastButton.UseVisualStyleBackColor = true;
        runLastButton.Click += OnClickRunLastCommand;
        // 
        // clearOutstandingButton
        // 
        clearOutstandingButton.Location = new Point(239, 24);
        clearOutstandingButton.Name = "clearOutstandingButton";
        clearOutstandingButton.Size = new Size(113, 23);
        clearOutstandingButton.TabIndex = 11;
        clearOutstandingButton.Text = "Clear Outstanding";
        clearOutstandingButton.UseVisualStyleBackColor = true;
        clearOutstandingButton.Click += OnClickClearOutstandingCommands;
        // 
        // nextUserTextLabel
        // 
        nextUserTextLabel.AutoSize = true;
        nextUserTextLabel.Location = new Point(51, 58);
        nextUserTextLabel.Name = "nextUserTextLabel";
        nextUserTextLabel.Size = new Size(80, 15);
        nextUserTextLabel.TabIndex = 10;
        nextUserTextLabel.Text = "Next is from : ";
        // 
        // nextUserCommandLabel
        // 
        nextUserCommandLabel.AutoSize = true;
        nextUserCommandLabel.Location = new Point(134, 58);
        nextUserCommandLabel.Name = "nextUserCommandLabel";
        nextUserCommandLabel.Size = new Size(50, 15);
        nextUserCommandLabel.TabIndex = 9;
        nextUserCommandLabel.Text = "No Data";
        // 
        // reportSenderButton
        // 
        reportSenderButton.Location = new Point(383, 24);
        reportSenderButton.Name = "reportSenderButton";
        reportSenderButton.Size = new Size(113, 23);
        reportSenderButton.TabIndex = 7;
        reportSenderButton.Text = "Report Sender";
        reportSenderButton.UseVisualStyleBackColor = true;
        reportSenderButton.Click += OnClickReportLastCommandSender;
        // 
        // blockSenderButton
        // 
        blockSenderButton.Location = new Point(383, 53);
        blockSenderButton.Name = "blockSenderButton";
        blockSenderButton.Size = new Size(113, 23);
        blockSenderButton.TabIndex = 6;
        blockSenderButton.Text = "Block Sender";
        blockSenderButton.UseVisualStyleBackColor = true;
        blockSenderButton.Click += OnClickBlockLastCommandSender;
        // 
        // runNextButton
        // 
        runNextButton.Location = new Point(239, 53);
        runNextButton.Name = "runNextButton";
        runNextButton.Size = new Size(113, 23);
        runNextButton.TabIndex = 2;
        runNextButton.Text = "Run Next";
        runNextButton.UseVisualStyleBackColor = true;
        runNextButton.Click += OnClickRunNextCommand;
        // 
        // commandCountLabel
        // 
        commandCountLabel.AutoSize = true;
        commandCountLabel.Location = new Point(20, 28);
        commandCountLabel.Name = "commandCountLabel";
        commandCountLabel.Size = new Size(108, 15);
        commandCountLabel.TabIndex = 1;
        commandCountLabel.Text = "No of Commands :";
        // 
        // sendCommandTab
        // 
        sendCommandTab.Controls.Add(linkLabelVerification);
        sendCommandTab.Location = new Point(4, 24);
        sendCommandTab.Name = "sendCommandTab";
        sendCommandTab.Padding = new Padding(3);
        sendCommandTab.Size = new Size(817, 444);
        sendCommandTab.TabIndex = 1;
        sendCommandTab.Text = "Send Command";
        sendCommandTab.UseVisualStyleBackColor = true;
        // 
        // linkLabelVerification
        // 
        linkLabelVerification.AutoSize = true;
        linkLabelVerification.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        linkLabelVerification.LinkColor = Color.Firebrick;
        linkLabelVerification.Location = new Point(188, 192);
        linkLabelVerification.Name = "linkLabelVerification";
        linkLabelVerification.Size = new Size(419, 15);
        linkLabelVerification.TabIndex = 2;
        linkLabelVerification.TabStop = true;
        linkLabelVerification.Text = "Please link your Discord account in the settings to enable sending commands.";
        linkLabelVerification.Visible = false;
        linkLabelVerification.LinkClicked += OnClickLinkDiscordToYourAccount;
        // 
        // usernameTextLabel
        // 
        usernameTextLabel.AutoSize = true;
        usernameTextLabel.Location = new Point(21, 37);
        usernameTextLabel.Name = "usernameTextLabel";
        usernameTextLabel.Size = new Size(65, 15);
        usernameTextLabel.TabIndex = 16;
        usernameTextLabel.Text = "User Name";
        // 
        // statusLabel
        // 
        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(241, 37);
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(79, 15);
        statusLabel.TabIndex = 36;
        statusLabel.Text = "Disconnected";
        // 
        // usernameInput
        // 
        usernameInput.Enabled = false;
        usernameInput.Location = new Point(92, 34);
        usernameInput.Name = "usernameInput";
        usernameInput.ReadOnly = true;
        usernameInput.Size = new Size(143, 23);
        usernameInput.TabIndex = 15;
        // 
        // MainWindow
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(845, 548);
        Controls.Add(statusLabel);
        Controls.Add(scoreInput);
        Controls.Add(scoreLabel);
        Controls.Add(tabControl);
        Controls.Add(usernameTextLabel);
        Controls.Add(usernameInput);
        Controls.Add(menuStrip1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MainMenuStrip = menuStrip1;
        Name = "MainWindow";
        Text = "The Control App v0.1.2";
        Load += Form1_Load;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        tabControl.ResumeLayout(false);
        mainTab.ResumeLayout(false);
        mainTab.PerformLayout();
        sendCommandTab.ResumeLayout(false);
        sendCommandTab.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip menuStrip1;
	private ToolStripMenuItem settingToolStripMenuItem;
	private ToolStripMenuItem accountConfigToolStripMenuItem;
	private ToolStripMenuItem commandConfigToolStripMenuItem;
	private Label usernameTextLabel;
    private TabControl tabControl;
	private TabPage mainTab;
	private CommandBuilderTab sendCommandTab;
	private ToolStripMenuItem historyToolStripMenuItem;
	private ToolStripMenuItem listToolStripMenuItem;
	private ToolStripMenuItem otherToolStripMenuItem;
	private Button runNextButton;
	private Label commandCountLabel;
	private Button blockSenderButton;
	private Button reportSenderButton;
	private Label nextUserTextLabel;
	private Label nextUserCommandLabel;
	private Button clearOutstandingButton;
	private Button runLastButton;
	private Button thumbsUpButton;
	private Label scoreLabel;
	private TextBox scoreInput;
    private Forms.LoginForm loginForm;
    private async void Form1_Load(object sender, EventArgs e)
	{
        // Subscribe to WebSocket events
        WebSocketsCommunicator.CommandReceived += OnCommandReceived;
        WebSocketsCommunicator.Connected += OnWebSocketConnected;
        WebSocketsCommunicator.Disconnected += OnWebSocketDisconnected;
        CommandManager.OnQueueChanged += UpdateCommandUI;
        // set the initial UI state
        UpdateUIFromAccount();
        UpdateCommandUI();
        // Start the connection
        await WebSocketsCommunicator.ConnectAsync();
    }

    // A new method to refresh the UI based on the logged-in user
    private void UpdateUIFromAccount()
    {
        if (AccountService.IsLoggedIn)
        {
            var currentUser = AccountService.CurrentUser;

            usernameInput.Text = currentUser.Username;

            // Logic to restrict the tab
            if (!currentUser.IsVerified)
            {
                sendCommandTab.Enabled = false;
                linkLabelVerification.Visible = true;
                linkLabelVerification.BringToFront();
            }
            else
            {
                sendCommandTab.Enabled = true;
                linkLabelVerification.Visible = false;
            }
        }
        else
        {
            // This case should ideally not happen if the startup logic is correct,
            // but it's good practice to handle it.
            MessageBox.Show("Error: No logged-in user found. Exiting.");
            Application.Exit();
        }
    }

    /// <summary>
    /// Updates all UI elements related to the command queue's state.
    /// This method is thread-safe and can be called from any thread.
    /// It should be subscribed to the CommandManager.OnQueueChanged event.
    /// </summary>
    private void UpdateCommandUI()
    {
        // This is a critical pattern for thread safety in WinForms.
        // The OnQueueChanged event might be fired from a background thread (like the WebSocket listener).
        // This check ensures the UI is only ever updated from the main UI thread.
        if (InvokeRequired)
        {
            // If we're not on the UI thread, reinvoke this same method on the UI thread and exit.
            Invoke(new Action(UpdateCommandUI));
            return;
        }

        // --- 1. Get the current state from the CommandManager ---
        // We fetch the state once at the beginning to ensure consistency throughout the update.
        int pendingCount = CommandManager.PendingPayloadCount;
        var nextPayload = CommandManager.PeekNextPayload();
        var lastPayload = CommandManager.LastExecutedPayload;

        // --- 2. Update Labels and Text Boxes ---
        remainingCommandLabel.Text = pendingCount.ToString();
        nextUserCommandLabel.Text = nextPayload?.Payload.SenderUsername ?? "None";
        lastUserCommandLabel.Text = lastPayload?.Payload.SenderUsername ?? "None";

        // --- 3. Determine the Enabled/Disabled state for all related buttons ---
        bool hasPendingPayloads = pendingCount > 0;
        bool hasLastPayload = lastPayload != null;

        // The "Thumbs Up" button has a special condition: it requires a last payload AND the sender must not be anonymous.
        bool canInteractWithLastSender = hasLastPayload &&
                                         lastPayload.Payload.SenderUsername != "-1" &&
                                         !string.IsNullOrEmpty(lastPayload.Payload.SenderUsername);

        // --- 4. Apply the new state to all buttons ---

        // Buttons related to the command queue
        runNextButton.Enabled = hasPendingPayloads;
        runAllButton.Enabled = hasPendingPayloads;
        clearOutstandingButton.Enabled = hasPendingPayloads;

        // Buttons related to the last executed command
        runLastButton.Enabled = hasLastPayload;
        blockSenderButton.Enabled = hasLastPayload;
        reportSenderButton.Enabled = hasLastPayload;

        // Button with special conditions
        thumbsUpButton.Enabled = canInteractWithLastSender;
    }

    private LinkLabel linkLabelVerification;
    private Label statusLabel;
    private Label label2;
    private Label lastUserCommandLabel;
    private Label remainingCommandLabel;
    private TextBox usernameInput;
    private Button runAllButton;
}