namespace ControlApp.Subroutines
{
    partial class CustomMessage
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
            messageLabel = new Label();
            button = new Button();
            SuspendLayout();
            // 
            // messageLabel
            // 
            messageLabel.AutoSize = true;
            messageLabel.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
            messageLabel.Location = new Point(26, 24);
            messageLabel.MaximumSize = new Size(800, 0);
            messageLabel.Name = "messageLabel";
            messageLabel.Size = new Size(117, 46);
            messageLabel.TabIndex = 0;
            messageLabel.Text = "messageLabel";
            // 
            // button
            // 
            button.Anchor = AnchorStyles.Bottom;
            button.AutoSize = true;
            button.Location = new Point(244, 81);
            button.Name = "button";
            button.Size = new Size(75, 25);
            button.TabIndex = 1;
            button.Text = "Close";
            button.UseVisualStyleBackColor = true;
            button.Click += button_Click;
            // 
            // CustomMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(563, 118);
            ControlBox = false;
            Controls.Add(button);
            Controls.Add(messageLabel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CustomMessage";
            Text = "CustomMessage";
            TopMost = true;
            Load += CustomMessage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label messageLabel;
        private Button button;
    }
}