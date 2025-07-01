using System.ComponentModel;
using System.Configuration;

namespace ControlApp;

partial class CommandBuilderTab {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
        if (disposing && (components != null)) {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
        displayPanel = new System.Windows.Forms.Panel();
        respondButton = new System.Windows.Forms.Button();
        sendCommandButton = new System.Windows.Forms.Button();
        groupCombo = new System.Windows.Forms.ComboBox();
        destUsernameCombo = new System.Windows.Forms.ComboBox();
        groupLabel = new System.Windows.Forms.Label();
        destUsernameLabel = new System.Windows.Forms.Label();
        commandDisplay = new System.Windows.Forms.TextBox();
        commandDisplayLabel = new System.Windows.Forms.Label();
        builderPanel = new System.Windows.Forms.Panel();
        clearCommandsButton = new System.Windows.Forms.Button();
        inputPanel = new System.Windows.Forms.Panel();
        urlRadioButton = new System.Windows.Forms.RadioButton();
        fileRadioButton = new System.Windows.Forms.RadioButton();
        fileUploadButton = new System.Windows.Forms.Button();
        fileNameTextBox = new System.Windows.Forms.TextBox();
        lowerSpinner = new System.Windows.Forms.NumericUpDown();
        lowerTextBox = new System.Windows.Forms.TextBox();
        lowerLabel = new System.Windows.Forms.Label();
        upperTextBox = new System.Windows.Forms.TextBox();
        upperLabel = new System.Windows.Forms.Label();
        addCommandButton = new System.Windows.Forms.Button();
        commandCombo = new System.Windows.Forms.ComboBox();
        displayPanel.SuspendLayout();
        builderPanel.SuspendLayout();
        inputPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)lowerSpinner).BeginInit();
        SuspendLayout();
        // 
        // displayPanel
        // 
        displayPanel.Controls.Add(respondButton);
        displayPanel.Controls.Add(sendCommandButton);
        displayPanel.Controls.Add(groupCombo);
        displayPanel.Controls.Add(destUsernameCombo);
        displayPanel.Controls.Add(groupLabel);
        displayPanel.Controls.Add(destUsernameLabel);
        displayPanel.Controls.Add(commandDisplay);
        displayPanel.Controls.Add(commandDisplayLabel);
        displayPanel.Location = new System.Drawing.Point(499, 12);
        displayPanel.Name = "displayPanel";
        displayPanel.Size = new System.Drawing.Size(289, 426);
        displayPanel.TabIndex = 0;
        // 
        // respondButton
        // 
        respondButton.Location = new System.Drawing.Point(198, 389);
        respondButton.Name = "respondButton";
        respondButton.Size = new System.Drawing.Size(81, 22);
        respondButton.TabIndex = 7;
        respondButton.Text = "Respond";
        respondButton.UseVisualStyleBackColor = true;
        respondButton.Click += respondButton_Click;
        // 
        // sendCommandButton
        // 
        sendCommandButton.Location = new System.Drawing.Point(198, 353);
        sendCommandButton.Name = "sendCommandButton";
        sendCommandButton.Size = new System.Drawing.Size(81, 23);
        sendCommandButton.TabIndex = 6;
        sendCommandButton.Text = "Send";
        sendCommandButton.UseVisualStyleBackColor = true;
        sendCommandButton.Click += sendCommandButton_Click;
        // 
        // groupCombo
        // 
        groupCombo.FormattingEnabled = true;
        groupCombo.Location = new System.Drawing.Point(78, 389);
        groupCombo.Name = "groupCombo";
        groupCombo.Items.AddRange(new string[]{ "", "Hypno", "Goon", "Censored", "Girl Cock", "Male focus", "Female focus", "BDSM", "Extreme", "Trans", "Panic", "Blackmail/Exposure", "Latex/Rubber", "Sissy", "Chastity", "Furry", "Feet", "Praise/Wholesome", "Misc" });
        groupCombo.Size = new System.Drawing.Size(114, 23);
        groupCombo.TabIndex = 5;
        groupCombo.SelectedIndexChanged += groupCombo_SelectedIndexChanged;
        // 
        // destUsernameCombo
        // 
        destUsernameCombo.FormattingEnabled = true;
        destUsernameCombo.Location = new System.Drawing.Point(78, 353);
        destUsernameCombo.Name = "destUsernameCombo";
        destUsernameCombo.Size = new System.Drawing.Size(114, 23);
        destUsernameCombo.TabIndex = 4;
        destUsernameCombo.SelectedIndexChanged += destUsernameCombo_SelectedIndexChanged;
        // 
        // groupLabel
        // 
        groupLabel.Location = new System.Drawing.Point(10, 389);
        groupLabel.Name = "groupLabel";
        groupLabel.Size = new System.Drawing.Size(62, 23);
        groupLabel.TabIndex = 3;
        groupLabel.Text = "Group";
        groupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // destUsernameLabel
        // 
        destUsernameLabel.Location = new System.Drawing.Point(10, 353);
        destUsernameLabel.Name = "destUsernameLabel";
        destUsernameLabel.Size = new System.Drawing.Size(62, 23);
        destUsernameLabel.TabIndex = 2;
        destUsernameLabel.Text = "Username";
        destUsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // commandDisplay
        // 
        commandDisplay.Location = new System.Drawing.Point(3, 26);
        commandDisplay.Multiline = true;
        commandDisplay.Name = "commandDisplay";
        commandDisplay.ReadOnly = true;
        commandDisplay.Size = new System.Drawing.Size(286, 311);
        commandDisplay.TabIndex = 1;
        // 
        // commandDisplayLabel
        // 
        commandDisplayLabel.Location = new System.Drawing.Point(3, 0);
        commandDisplayLabel.Name = "commandDisplayLabel";
        commandDisplayLabel.Size = new System.Drawing.Size(254, 23);
        commandDisplayLabel.TabIndex = 0;
        commandDisplayLabel.Text = "Current Commands";
        commandDisplayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // builderPanel
        // 
        builderPanel.Controls.Add(clearCommandsButton);
        builderPanel.Controls.Add(inputPanel);
        builderPanel.Controls.Add(addCommandButton);
        builderPanel.Controls.Add(commandCombo);
        builderPanel.Location = new System.Drawing.Point(12, 12);
        builderPanel.Name = "builderPanel";
        builderPanel.Size = new System.Drawing.Size(481, 426);
        builderPanel.TabIndex = 1;
        // 
        // clearCommandsButton
        // 
        clearCommandsButton.Location = new System.Drawing.Point(297, 371);
        clearCommandsButton.Name = "clearCommandsButton";
        clearCommandsButton.Size = new System.Drawing.Size(127, 23);
        clearCommandsButton.TabIndex = 3;
        clearCommandsButton.Text = "Clear Commands";
        clearCommandsButton.UseVisualStyleBackColor = true;
        clearCommandsButton.Click += clearCommandsButton_Click;
        // 
        // inputPanel
        // 
        inputPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
        inputPanel.Controls.Add(urlRadioButton);
        inputPanel.Controls.Add(fileRadioButton);
        inputPanel.Controls.Add(fileUploadButton);
        inputPanel.Controls.Add(fileNameTextBox);
        inputPanel.Controls.Add(lowerSpinner);
        inputPanel.Controls.Add(lowerTextBox);
        inputPanel.Controls.Add(lowerLabel);
        inputPanel.Controls.Add(upperTextBox);
        inputPanel.Controls.Add(upperLabel);
        inputPanel.Location = new System.Drawing.Point(3, 26);
        inputPanel.Name = "inputPanel";
        inputPanel.Size = new System.Drawing.Size(478, 319);
        inputPanel.TabIndex = 2;
        // 
        // urlRadioButton
        // 
        urlRadioButton.Location = new System.Drawing.Point(317, 27);
        urlRadioButton.Name = "urlRadioButton";
        urlRadioButton.Size = new System.Drawing.Size(108, 23);
        urlRadioButton.TabIndex = 8;
        urlRadioButton.TabStop = true;
        urlRadioButton.Text = "From URL/Text";
        urlRadioButton.UseVisualStyleBackColor = true;
        urlRadioButton.Visible = false;
        urlRadioButton.CheckedChanged += urlRadioButton_CheckedChanged;
        // 
        // fileRadioButton
        // 
        fileRadioButton.Location = new System.Drawing.Point(53, 27);
        fileRadioButton.Name = "fileRadioButton";
        fileRadioButton.Size = new System.Drawing.Size(83, 23);
        fileRadioButton.TabIndex = 7;
        fileRadioButton.TabStop = true;
        fileRadioButton.Text = "From File";
        fileRadioButton.UseVisualStyleBackColor = true;
        fileRadioButton.Visible = false;
        fileRadioButton.CheckedChanged += fileRadioButton_CheckedChanged;
        // 
        // fileUploadButton
        // 
        fileUploadButton.Location = new System.Drawing.Point(384, 99);
        fileUploadButton.Name = "fileUploadButton";
        fileUploadButton.Size = new System.Drawing.Size(84, 23);
        fileUploadButton.TabIndex = 6;
        fileUploadButton.Text = "Browse";
        fileUploadButton.UseVisualStyleBackColor = true;
        fileUploadButton.Visible = false;
        fileUploadButton.Click += fileUploadButton_Click;
        // 
        // fileNameTextBox
        // 
        fileNameTextBox.Location = new System.Drawing.Point(10, 99);
        fileNameTextBox.Name = "fileNameTextBox";
        fileNameTextBox.ReadOnly = true;
        fileNameTextBox.Size = new System.Drawing.Size(368, 23);
        fileNameTextBox.TabIndex = 5;
        fileNameTextBox.Visible = false;
        // 
        // lowerSpinner
        // 
        lowerSpinner.Location = new System.Drawing.Point(10, 183);
        lowerSpinner.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        lowerSpinner.Name = "lowerSpinner";
        lowerSpinner.Size = new System.Drawing.Size(96, 23);
        lowerSpinner.TabIndex = 4;
        lowerSpinner.Value = new decimal(new int[] { 1, 0, 0, 0 });
        lowerSpinner.Visible = false;
        // 
        // lowerTextBox
        // 
        lowerTextBox.Location = new System.Drawing.Point(10, 183);
        lowerTextBox.Name = "lowerTextBox";
        lowerTextBox.Size = new System.Drawing.Size(368, 23);
        lowerTextBox.TabIndex = 3;
        lowerTextBox.Visible = false;
        // 
        // lowerLabel
        // 
        lowerLabel.Location = new System.Drawing.Point(10, 157);
        lowerLabel.Name = "lowerLabel";
        lowerLabel.Size = new System.Drawing.Size(458, 23);
        lowerLabel.TabIndex = 2;
        lowerLabel.Text = "lowerLabel";
        lowerLabel.Visible = false;
        lowerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // upperTextBox
        // 
        upperTextBox.Location = new System.Drawing.Point(10, 99);
        upperTextBox.MaxLength = 2000;
        upperTextBox.Name = "upperTextBox";
        upperTextBox.Size = new System.Drawing.Size(458, 23);
        upperTextBox.TabIndex = 1;
        upperTextBox.Visible = false;
        // 
        // upperLabel
        // 
        upperLabel.Location = new System.Drawing.Point(10, 77);
        upperLabel.Name = "upperLabel";
        upperLabel.Size = new System.Drawing.Size(458, 19);
        upperLabel.TabIndex = 0;
        upperLabel.Visible = false;
        upperLabel.Text = "upperLabel";
        // 
        // addCommandButton
        // 
        addCommandButton.Location = new System.Drawing.Point(56, 371);
        addCommandButton.Name = "addCommandButton";
        addCommandButton.Size = new System.Drawing.Size(127, 23);
        addCommandButton.TabIndex = 1;
        addCommandButton.Text = "Add Command";
        addCommandButton.UseVisualStyleBackColor = true;
        addCommandButton.Visible = false;
        addCommandButton.Click += addCommandButton_Click;
        // 
        // commandCombo
        // 
        commandCombo.DataSource = buildersList;
        commandCombo.DisplayMember = "DisplayName";
        commandCombo.FormattingEnabled = true;
        commandCombo.Location = new System.Drawing.Point(3, 3);
        commandCombo.Name = "commandCombo";
        commandCombo.Size = new System.Drawing.Size(475, 23);
        commandCombo.TabIndex = 0;
        commandCombo.SelectedIndexChanged += commandCombo_SelectedIndexChanged;
        // 
        // CommandBuilderTab
        // 
        Controls.Add(builderPanel);
        Controls.Add(displayPanel);
        
        // non-designer generated code
        PopulateUserList();
        ToolTip toolTip = new ToolTip();
        toolTip.SetToolTip(groupCombo, "Sends the command to all in this group.");
        toolTip.SetToolTip(destUsernameCombo, "Sends the command to this user.");
        
        displayPanel.ResumeLayout(false);
        displayPanel.PerformLayout();
        builderPanel.ResumeLayout(false);
        inputPanel.ResumeLayout(false);
        inputPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)lowerSpinner).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.Button clearCommandsButton;

    private System.Windows.Forms.RadioButton fileRadioButton;
    private System.Windows.Forms.RadioButton urlRadioButton;

    private System.Windows.Forms.TextBox fileNameTextBox;
    private System.Windows.Forms.Button fileUploadButton;
    private OpenFileDialog openFileDialog;

    private System.Windows.Forms.NumericUpDown lowerSpinner;

    private System.Windows.Forms.Label upperLabel;
    private System.Windows.Forms.TextBox upperTextBox;
    private System.Windows.Forms.Label lowerLabel;
    private System.Windows.Forms.TextBox lowerTextBox;

    private System.Windows.Forms.Button addCommandButton;
    private System.Windows.Forms.Panel inputPanel;

    private System.Windows.Forms.Panel builderPanel;
    private System.Windows.Forms.ComboBox commandCombo;

    private System.Windows.Forms.ComboBox destUsernameCombo;
    private System.Windows.Forms.ComboBox groupCombo;
    private System.Windows.Forms.Button sendCommandButton;
    private System.Windows.Forms.Button respondButton;

    private System.Windows.Forms.Label destUsernameLabel;
    private System.Windows.Forms.Label groupLabel;

    private System.Windows.Forms.TextBox commandDisplay;

    private System.Windows.Forms.Panel displayPanel;
    private System.Windows.Forms.Label commandDisplayLabel;

    #endregion

    public void PopulateUserList() {
        string userlist = ConfigurationManager.AppSettings["CommonUsers"];
        if (userlist == null) return;
        destUsernameCombo.Items.Clear();
        string[] array = Utils.SeparateString(userlist);
        foreach (string user in array) {
            destUsernameCombo.Items.Add(user);
        }
    }
}