using System.IO;
using AxWMPLib;

namespace ControlApp.Subroutines;

partial class WatchForMe
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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WatchForMe));
        watchTimer = new System.Windows.Forms.Timer(components);
        censorTimer = new System.Windows.Forms.Timer(components);
        webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
        axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
        ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).BeginInit();
        SuspendLayout();
        // 
        // webView21
        // 
        webView21.AllowExternalDrop = true;
        webView21.CreationProperties = null;
        webView21.DefaultBackgroundColor = System.Drawing.Color.White;
        webView21.Dock = System.Windows.Forms.DockStyle.Fill;
        webView21.Location = new System.Drawing.Point(0, 0);
        webView21.Name = "webView21";
        webView21.Size = new System.Drawing.Size(1044, 647);
        webView21.TabIndex = 1;
        webView21.ZoomFactor = 1D;
        // 
        // axWindowsMediaPlayer
        // 
        axWindowsMediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
        axWindowsMediaPlayer.Location = new System.Drawing.Point(0, 0);
        axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
        axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)resources.GetObject("axWindowsMediaPlayer.OcxState"));
        axWindowsMediaPlayer.Size = new System.Drawing.Size(1044, 647);
        axWindowsMediaPlayer.TabIndex = 3;
        axWindowsMediaPlayer.KeyPress += axWindowsMediaPlayer_KeyPress;
        // 
        // WatchForMe
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1044, 647);
        Controls.Add(axWindowsMediaPlayer);
        Controls.Add(webView21);
        Text = "WatchForMe";
        KeyDown += WFM_KeyDown;
        ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
        ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }


    #endregion
    private System.Windows.Forms.Timer watchTimer;
    private System.Windows.Forms.Timer censorTimer;
    private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
    private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
}