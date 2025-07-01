namespace ControlApp.Subroutines
{
    partial class Blank
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
            SuspendLayout();
            // 
            // Blank
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            ControlBox = false;
            Cursor = System.Windows.Forms.Cursors.No;
            Opacity = 0.001D;
            ShowInTaskbar = false;
            Text = "Blank";
            TopMost = true;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormClosing += Blank_FormClosing;
            Load += Blank_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}