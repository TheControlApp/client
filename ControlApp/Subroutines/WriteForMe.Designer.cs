namespace ControlApp.Subroutines;

partial class WriteForMe
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        failButton = new Button();
        titleLabel = new Label();
        writeLabel = new Label();
        inputBox = new TextBox();
        timeLabel = new Label();
        timeTextLabel = new Label();
        mistakeTextLabel = new Label();
        mistakeLabel = new Label();
        countTextLabel = new Label();
        countLabel = new Label();
        timer1 = new System.Windows.Forms.Timer(components);
        SuspendLayout();
        // 
        // failButton
        // 
        failButton.Location = new Point(398, 136);
        failButton.Name = "failButton";
        failButton.Size = new Size(75, 23);
        failButton.TabIndex = 0;
        failButton.Text = "Fail";
        failButton.UseVisualStyleBackColor = true;
        failButton.Click += button1_Click;
        // 
        // titleLabel
        // 
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        titleLabel.Location = new Point(21, 19);
        titleLabel.Name = "titleLabel";
        titleLabel.Size = new Size(125, 25);
        titleLabel.TabIndex = 1;
        titleLabel.Text = "Write for me";
        // 
        // writeLabel
        // 
        writeLabel.AutoSize = true;
        writeLabel.Location = new Point(21, 59);
        writeLabel.Name = "writeLabel";
        writeLabel.Size = new Size(38, 15);
        writeLabel.TabIndex = 2;
        writeLabel.Text = "label2";
        // 
        // inputBox
        // 
        inputBox.Location = new Point(21, 95);
        inputBox.Name = "inputBox";
        inputBox.Size = new Size(452, 23);
        inputBox.TabIndex = 3;
        inputBox.KeyDown += input_KeyDown;
        // 
        // timeLabel
        // 
        timeLabel.AutoSize = true;
        timeLabel.Location = new Point(420, 19);
        timeLabel.Name = "timeLabel";
        timeLabel.Size = new Size(38, 15);
        timeLabel.TabIndex = 4;
        timeLabel.Text = "label2";
        // 
        // timeTextLabel
        // 
        timeTextLabel.AutoSize = true;
        timeTextLabel.Location = new Point(366, 19);
        timeTextLabel.Name = "timeTextLabel";
        timeTextLabel.Size = new Size(33, 15);
        timeTextLabel.TabIndex = 5;
        timeTextLabel.Text = "Time";
        // 
        // mistakeTextLabel
        // 
        mistakeTextLabel.AutoSize = true;
        mistakeTextLabel.Location = new Point(366, 34);
        mistakeTextLabel.Name = "mistakeTextLabel";
        mistakeTextLabel.Size = new Size(53, 15);
        mistakeTextLabel.TabIndex = 6;
        mistakeTextLabel.Text = "Mistakes";
        // 
        // mistakeLabel
        // 
        mistakeLabel.AutoSize = true;
        mistakeLabel.Location = new Point(420, 34);
        mistakeLabel.Name = "mistakeLabel";
        mistakeLabel.Size = new Size(38, 15);
        mistakeLabel.TabIndex = 7;
        mistakeLabel.Text = "label5";
        // 
        // countTextLabel
        // 
        countTextLabel.AutoSize = true;
        countTextLabel.Location = new Point(366, 49);
        countTextLabel.Name = "countTextLabel";
        countTextLabel.Size = new Size(27, 15);
        countTextLabel.TabIndex = 8;
        countTextLabel.Text = "Left";
        // 
        // countLabel
        // 
        countLabel.AutoSize = true;
        countLabel.Location = new Point(420, 49);
        countLabel.Name = "countLabel";
        countLabel.Size = new Size(38, 15);
        countLabel.TabIndex = 9;
        countLabel.Text = "label7";
        // 
        // WriteForMe
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(492, 173);
        ControlBox = false;
        Controls.Add(countLabel);
        Controls.Add(countTextLabel);
        Controls.Add(mistakeLabel);
        Controls.Add(mistakeTextLabel);
        Controls.Add(timeTextLabel);
        Controls.Add(timeLabel);
        Controls.Add(inputBox);
        Controls.Add(writeLabel);
        Controls.Add(titleLabel);
        Controls.Add(failButton);
        FormBorderStyle = FormBorderStyle.None;
        Name = "WriteForMe";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "WriteForMe";
        TopMost = true;
        Load += WriteForMe_Load;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button failButton;
    private Label titleLabel;
    private Label writeLabel;
    private TextBox inputBox;
    private Label timeLabel;
    private Label timeTextLabel;
    private Label mistakeTextLabel;
    private Label mistakeLabel;
    private Label countTextLabel;
    private Label countLabel;
    private System.Windows.Forms.Timer timer1;
}