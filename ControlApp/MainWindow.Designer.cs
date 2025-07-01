using System.ComponentModel;
using System.Configuration;
using ControlApp.Subroutines;
using Timer = System.Windows.Forms.Timer;

namespace ControlApp;

partial class MainWindow {
	private readonly Size PANEL_SIZE = new Size(817, 438);
	
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

	private void InitializeComponent() {
		components = new System.ComponentModel.Container();
		
		timer = new System.Windows.Forms.Timer(components);
		
		menuStrip1 = new MenuStrip();
		settingToolStripMenuItem = new ToolStripMenuItem();
		configToolStripMenuItem = new ToolStripMenuItem();
		optionsToolStripMenuItem = new ToolStripMenuItem();
		otherToolStripMenuItem = new ToolStripMenuItem();
		
		scoreLabel = new Label();
		scoreInput = new TextBox();
		
		tabControl = new TabControl();
		mainTab = new TabPage();
		usernameTextLabel = new Label();
		usernameInput = new TextBox();
		commandCountLabel = new Label();
		commandCountTextBox = new TextBox();
		nextUserTextLabel = new Label();
		nextUserLabel = new Label();
		checkNextButton = new Button();
		runNextButton = new Button();
		runLastButton = new Button();
		clearOutstandingButton = new Button();
		blockSenderButton = new Button();
		reportSenderButton = new Button();
		thumbsUpButton = new Button();
		
		sendCommandTab = new CommandBuilderTab();
		historyToolStripMenuItem = new ToolStripMenuItem();
		listToolStripMenuItem = new ToolStripMenuItem();
		menuStrip1.SuspendLayout();
		tabControl.SuspendLayout();
		mainTab.SuspendLayout();
		SuspendLayout();
		// 
		// menuStrip1
		// 
		menuStrip1.Items.AddRange(new ToolStripItem[] { settingToolStripMenuItem, historyToolStripMenuItem });
		menuStrip1.Location = new Point(0, 0);
		menuStrip1.Name = "menuStrip1";
		menuStrip1.Size = new Size(839, 24);
		menuStrip1.TabIndex = 8;
		menuStrip1.Text = "menuStrip1";
		// 
		// settingToolStripMenuItem
		// 
		settingToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { configToolStripMenuItem, optionsToolStripMenuItem, otherToolStripMenuItem });
		settingToolStripMenuItem.Name = "settingToolStripMenuItem";
		settingToolStripMenuItem.Size = new Size(56, 22);
		settingToolStripMenuItem.Text = "Setting";
		// 
		// configToolStripMenuItem
		// 
		configToolStripMenuItem.Name = "configToolStripMenuItem";
		configToolStripMenuItem.Size = new Size(116, 22);
		configToolStripMenuItem.Text = "Config";
		configToolStripMenuItem.Click += configToolStripMenuItem_Click;
		// 
		// optionsToolStripMenuItem
		// 
		optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
		optionsToolStripMenuItem.Size = new Size(116, 22);
		optionsToolStripMenuItem.Text = "Options";
		optionsToolStripMenuItem.Click += optionsToolStripMenuItem_Click;
		// 
		// otherToolStripMenuItem
		// 
		otherToolStripMenuItem.Name = "otherToolStripMenuItem";
		otherToolStripMenuItem.Size = new Size(116, 22);
		otherToolStripMenuItem.Text = "Other";
		otherToolStripMenuItem.Click += otherToolStripMenuItem_Click;
		// 
		// usernameInput
		// 
		usernameInput.Enabled = false;
		usernameInput.Location = new Point(92, 34);
		usernameInput.Name = "usernameInput";
		usernameInput.Size = new Size(143, 23);
		usernameInput.TabIndex = 15;
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
		// timer
		// 
		timer.Interval = 1500;
		timer.Tick += timer1_Tick;
		// 
		// tabControl
		// 
		tabControl.Controls.Add(mainTab);
		tabControl.Controls.Add(sendCommandTab);
		tabControl.Location = new Point(10, 66);
		tabControl.Name = "tabControl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(825, 466);
		tabControl.TabIndex = 32;
		// 
		// mainTab
		// 
		mainTab.Controls.Add(thumbsUpButton);
		mainTab.Controls.Add(runLastButton);
		mainTab.Controls.Add(clearOutstandingButton);
		mainTab.Controls.Add(nextUserTextLabel);
		mainTab.Controls.Add(nextUserLabel);
		mainTab.Controls.Add(checkNextButton);
		mainTab.Controls.Add(reportSenderButton);
		mainTab.Controls.Add(blockSenderButton);
		mainTab.Controls.Add(runNextButton);
		mainTab.Controls.Add(commandCountLabel);
		mainTab.Controls.Add(commandCountTextBox);
		mainTab.Location = new Point(4, 24);
		mainTab.Name = "mainTab";
		mainTab.Padding = new Padding(3);
		mainTab.Size = PANEL_SIZE;
		mainTab.TabIndex = 0;
		mainTab.Text = "Received Commands";
		mainTab.UseVisualStyleBackColor = true;
		// 
		// thumbsUpButton
		// 
		thumbsUpButton.Location = new Point(342, 46);
		thumbsUpButton.Name = "thumbsUpButton";
		thumbsUpButton.Size = new Size(76, 23);
		thumbsUpButton.TabIndex = 13;
		thumbsUpButton.Text = "Thumbs up";
		thumbsUpButton.UseVisualStyleBackColor = true;
		thumbsUpButton.Click += thumbsUpButton_Click;
		// 
		// runLastButton
		// 
		runLastButton.Location = new Point(236, 75);
		runLastButton.Name = "runLastButton";
		runLastButton.Size = new Size(100, 23);
		runLastButton.TabIndex = 12;
		runLastButton.Text = "Run Last";
		runLastButton.UseVisualStyleBackColor = true;
		runLastButton.Click += RunLastButtonClick;
		// 
		// clearOutstandingButton
		// 
		clearOutstandingButton.Location = new Point(22, 81);
		clearOutstandingButton.Name = "clearOutstandingButton";
		clearOutstandingButton.Size = new Size(113, 23);
		clearOutstandingButton.TabIndex = 11;
		clearOutstandingButton.Text = "Clear Outstanding";
		clearOutstandingButton.UseVisualStyleBackColor = true;
		clearOutstandingButton.Click += clearOutstandingButton_Click;
		// 
		// nextUserTextLabel
		// 
		nextUserTextLabel.AutoSize = true;
		nextUserTextLabel.Location = new Point(22, 50);
		nextUserTextLabel.Name = "nextUserTextLabel";
		nextUserTextLabel.Size = new Size(78, 15);
		nextUserTextLabel.TabIndex = 10;
		nextUserTextLabel.Text = "Next is from: ";
		// 
		// nextUserLabel
		// 
		nextUserLabel.AutoSize = true;
		nextUserLabel.Location = new Point(106, 50);
		nextUserLabel.Name = "nextUserLabel";
		nextUserLabel.Size = new Size(50, 15);
		nextUserLabel.TabIndex = 9;
		nextUserLabel.Text = "No Data";
		// 
		// checkNextButton
		// 
		checkNextButton.Location = new Point(236, 17);
		checkNextButton.Name = "checkNextButton";
		checkNextButton.Size = new Size(100, 23);
		checkNextButton.TabIndex = 8;
		checkNextButton.Text = "Check";
		checkNextButton.UseVisualStyleBackColor = true;
		checkNextButton.Click += checkNextButton_Click;
		// 
		// reportSenderButton
		// 
		reportSenderButton.Location = new Point(700, 46);
		reportSenderButton.Name = "reportSenderButton";
		reportSenderButton.Size = new Size(100, 23);
		reportSenderButton.TabIndex = 7;
		reportSenderButton.Text = "Report Sender";
		reportSenderButton.UseVisualStyleBackColor = true;
		reportSenderButton.Click += reportSenderButton_Click;
		// 
		// blockSenderButton
		// 
		blockSenderButton.Location = new Point(700, 17);
		blockSenderButton.Name = "blockSenderButton";
		blockSenderButton.Size = new Size(100, 23);
		blockSenderButton.TabIndex = 6;
		blockSenderButton.Text = "Block Sender";
		blockSenderButton.UseVisualStyleBackColor = true;
		blockSenderButton.Click += blockSenderButton_Click;
		// 
		// runNextButton
		// 
		runNextButton.Location = new Point(236, 46);
		runNextButton.Name = "runNextButton";
		runNextButton.Size = new Size(100, 23);
		runNextButton.TabIndex = 2;
		runNextButton.Text = "Run Next";
		runNextButton.UseVisualStyleBackColor = true;
		runNextButton.Click += runNextButton_Click;
		// 
		// commandCountLabel
		// 
		commandCountLabel.AutoSize = true;
		commandCountLabel.Location = new Point(128, 20);
		commandCountLabel.Name = "commandCountLabel";
		commandCountLabel.Size = new Size(102, 15);
		commandCountLabel.TabIndex = 1;
		commandCountLabel.Text = "No of Commands";
		// 
		// commandCountTextBox
		// 
		commandCountTextBox.Location = new Point(22, 17);
		commandCountTextBox.Name = "commandCountTextBox";
		commandCountTextBox.Size = new Size(100, 23);
		commandCountTextBox.TabIndex = 0;
		// 
		// sendCommandTab
		// 
		sendCommandTab.Location = new Point(4, 24);
		sendCommandTab.Name = "sendCommandTab";
		sendCommandTab.Padding = new Padding(3);
		sendCommandTab.Size = PANEL_SIZE;
		sendCommandTab.TabIndex = 1;
		sendCommandTab.Text = "Send Command";
		sendCommandTab.UseVisualStyleBackColor = true;
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
		scoreInput.Size = new Size(100, 23);
		scoreInput.TabIndex = 35;
		scoreInput.ReadOnly = true;
		// 
		// historyToolStripMenuItem
		// 
		historyToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { listToolStripMenuItem });
		historyToolStripMenuItem.Name = "historyToolStripMenuItem";
		historyToolStripMenuItem.Size = new Size(57, 20);
		historyToolStripMenuItem.Text = "History";
		// 
		// listToolStripMenuItem
		// 
		listToolStripMenuItem.Name = "listToolStripMenuItem";
		listToolStripMenuItem.Size = new Size(180, 22);
		listToolStripMenuItem.Text = "List";
		// 
		// ControlApp
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(839, 517);
		Controls.Add(scoreInput);
		Controls.Add(scoreLabel);
		Controls.Add(tabControl);
		Controls.Add(usernameTextLabel);
		Controls.Add(usernameInput);
		Controls.Add(menuStrip1);
		MainMenuStrip = menuStrip1;
		Name = "ControlApp";
		Text = "The Control App v0.1.2";
		Load += Form1_Load;
		menuStrip1.ResumeLayout(false);
		menuStrip1.PerformLayout();
		tabControl.ResumeLayout(false);
		mainTab.ResumeLayout(false);
		mainTab.PerformLayout();
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion
	
	private MenuStrip menuStrip1;
	private ToolStripMenuItem settingToolStripMenuItem;
	private ToolStripMenuItem configToolStripMenuItem;
	private ToolStripMenuItem optionsToolStripMenuItem;
	private TextBox usernameInput;
	private Label usernameTextLabel;
	private System.Windows.Forms.Timer timer;
	private TabControl tabControl;
	private TabPage mainTab;
	private CommandBuilderTab sendCommandTab;
	private ToolStripMenuItem historyToolStripMenuItem;
	private ToolStripMenuItem listToolStripMenuItem;
	private ToolStripMenuItem otherToolStripMenuItem;
	private Button runNextButton;
	private Label commandCountLabel;
	private TextBox commandCountTextBox;
	private Button blockSenderButton;
	private Button reportSenderButton;
	private Button checkNextButton;
	private Label nextUserTextLabel;
	private Label nextUserLabel;
	private Button clearOutstandingButton;
	private Button runLastButton;
	private Button thumbsUpButton;
	private Label scoreLabel;
	private TextBox scoreInput;

	private void Form1_Load(object sender, EventArgs e) {
		CheckNext();
		int delay = (ConfigurationManager.AppSettings["Delay"] != null) ? Convert.ToInt32(ConfigurationManager.AppSettings["Delay"]) : 60;
		timer.Enabled = true;
		timer.Interval = delay * 1000;
	}

	private void configToolStripMenuItem_Click(object sender, EventArgs e) {
		new ConfigSettingsForm(this).ShowDialog();
		usernameInput.Text = username;
	}

	private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
		new Options().ShowDialog();
	}

	private void clearOutstandingButton_Click(object sender, EventArgs e) {
		if (MessageBox.Show("Are you sure?", "Clear Outstanding", MessageBoxButtons.YesNo) == DialogResult.Yes){
			ServerCommunicator.DeleteOutstanding();
			CheckNext();
		}
	}
	private void runNextButton_Click(object sender, EventArgs e) {
		RunNextCommand();
		CheckNext();
		thumbsUpButton.Enabled = true;
	}

	private void blockSenderButton_Click(object sender, EventArgs e) {
		if (lastSender[0] != "-1") { // TODO: Can it ever be -1?
			if (MessageBox.Show("Are you sure", "Sure?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
				ServerCommunicator.SendBlockReport(lastSender[0], "", "0");
			}
		} else {
			MessageBox.Show("Last message was from an anonymous sender");
		}
	}

	private void reportSenderButton_Click(object sender, EventArgs e) {
		if (MessageBox.Show("Are you sure", "Sure?", MessageBoxButtons.YesNo) == DialogResult.Yes) {
			ServerCommunicator.SendBlockReport(lastSender[0], lastSender[1], "1");
		}
	}

	private void checkNextButton_Click(object sender, EventArgs e) {
		timer.Stop();
		timer.Start();
		CheckNext();
	}
}