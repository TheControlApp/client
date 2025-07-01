namespace ControlApp.Subroutines;

partial class SubLoop
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubLoop));
        axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
        label = new Label();
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).BeginInit();
        SuspendLayout();
        // 
        // axWindowsMediaPlayer1
        // 
        axWindowsMediaPlayer.Dock = DockStyle.Fill;
        axWindowsMediaPlayer.Enabled = true;
        axWindowsMediaPlayer.Location = new Point(0, 0);
        axWindowsMediaPlayer.Name = "axWindowsMediaPlayer1";
        axWindowsMediaPlayer.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");          
        axWindowsMediaPlayer.TabIndex = 0;
        // 
        // label
        // 
        label.BackColor = Color.Transparent;
        label.Dock = DockStyle.Fill;
        label.Font = new Font("Arial", 60F);
        label.Location = new Point(0, 0);
        label.Name = "label1";
        label.Size = new Size(800, 450);
        label.TabIndex = 1;
        label.Text = "label";
        label.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // SubLoop
        // 
        WindowState = FormWindowState.Maximized;
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders
        Location = new Point(0, 0);
        TopMost = true;        // make the form always on top                     
        Visible = true;        // Important! if this isn't set, then the form is not shown at all
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(label);
        Controls.Add(axWindowsMediaPlayer);
        Name = "SubLoop";
        Opacity = 0.05D;
        Text = "SubLoop";
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
    private Label label;
}