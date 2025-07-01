namespace ControlApp.Subroutines;

partial class SendOrDelete
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
    private void InitializeComponent() {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendOrDelete));
        axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
        sendButton = new System.Windows.Forms.Button();
        deleteButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
        SuspendLayout();
        axWindowsMediaPlayer1.Dock = System.Windows.Forms.DockStyle.Top;
        axWindowsMediaPlayer1.Enabled = true;
        axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
        axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
        axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState"));
        axWindowsMediaPlayer1.Size = new System.Drawing.Size(800, 450);
        axWindowsMediaPlayer1.TabIndex = 1;
        sendButton.Location = new System.Drawing.Point(279, 468);
        sendButton.Name = "sendButton";
        sendButton.Size = new System.Drawing.Size(75, 23);
        sendButton.TabIndex = 2;
        sendButton.Text = "Send";
        sendButton.UseVisualStyleBackColor = true;
        sendButton.Click += sendButton_CLick;
        deleteButton.Location = new System.Drawing.Point(384, 468);
        deleteButton.Name = "deleteButton";
        deleteButton.Size = new System.Drawing.Size(75, 23);
        deleteButton.TabIndex = 3;
        deleteButton.Text = "Delete";
        deleteButton.UseVisualStyleBackColor = true;
        deleteButton.Click += deleteButton_Click;
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 503);
        ControlBox = false;
        Controls.Add(deleteButton);
        Controls.Add(sendButton);
        Controls.Add(axWindowsMediaPlayer1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Name = "SendOrDelete";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "SendOrDelete";
        TopMost = true;
        Load += SendOrDelete_Load;
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    private Button sendButton;
    private Button deleteButton;
}