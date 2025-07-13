namespace ControlApp.Subroutines;

partial class Popup
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Popup));
        axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).BeginInit();
        SuspendLayout();
        // 
        // axWindowsMediaPlayer
        // 
        axWindowsMediaPlayer.Dock = DockStyle.Fill;
        axWindowsMediaPlayer.Enabled = true;
        axWindowsMediaPlayer.Location = new Point(0, 0);
        axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
        axWindowsMediaPlayer.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer.OcxState");
        axWindowsMediaPlayer.TabIndex = 0;
        axWindowsMediaPlayer.PlayStateChange += axWMP_PlayStateChange;
        // 
        // Popup
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.Black;
        ClientSize = new Size(POPUP_WIDTH, POPUP_HEIGHT);
        ControlBox = false;
        Controls.Add(axWindowsMediaPlayer);
        FormBorderStyle = FormBorderStyle.None;
        Name = "Popup";
        ShowIcon = false;
        ShowInTaskbar = false;
        Text = "Popup";
        TopMost = true;
        Load += PopUp_Load;
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
}