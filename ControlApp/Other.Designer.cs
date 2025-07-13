namespace ControlApp
{
    partial class Other
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
            saveAndCloseButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            commonUsersTextBox = new System.Windows.Forms.TextBox();
            websiteBlacklistTextBox = new System.Windows.Forms.TextBox();
            websiteBlacklistLabel = new System.Windows.Forms.Label();
            userBlacklistTextBox = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // saveAndCloseButton
            // 
            saveAndCloseButton.Location = new System.Drawing.Point(146, 228);
            saveAndCloseButton.Name = "saveAndCloseButton";
            saveAndCloseButton.Size = new System.Drawing.Size(123, 23);
            saveAndCloseButton.TabIndex = 0;
            saveAndCloseButton.Text = "Save and Close";
            saveAndCloseButton.UseVisualStyleBackColor = true;
            saveAndCloseButton.Click += SaveAndCloseButton_Click;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(123, 15);
            label1.TabIndex = 1;
            label1.Text = "Common Users";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // commonUsersTextBox
            // 
            commonUsersTextBox.Location = new System.Drawing.Point(12, 27);
            commonUsersTextBox.Multiline = true;
            commonUsersTextBox.Name = "commonUsersTextBox";
            commonUsersTextBox.Size = new System.Drawing.Size(123, 189);
            commonUsersTextBox.TabIndex = 2;
            // 
            // websiteBlacklistTextBox
            // 
            websiteBlacklistTextBox.Location = new System.Drawing.Point(147, 27);
            websiteBlacklistTextBox.Multiline = true;
            websiteBlacklistTextBox.Name = "websiteBlacklistTextBox";
            websiteBlacklistTextBox.Size = new System.Drawing.Size(123, 189);
            websiteBlacklistTextBox.TabIndex = 4;
            // 
            // websiteBlacklistLabel
            // 
            websiteBlacklistLabel.Location = new System.Drawing.Point(147, 9);
            websiteBlacklistLabel.Name = "websiteBlacklistLabel";
            websiteBlacklistLabel.Size = new System.Drawing.Size(123, 15);
            websiteBlacklistLabel.TabIndex = 3;
            websiteBlacklistLabel.Text = "Website Blacklist";
            websiteBlacklistLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // userBlacklistTextBox
            // 
            userBlacklistTextBox.Location = new System.Drawing.Point(282, 27);
            userBlacklistTextBox.Multiline = true;
            userBlacklistTextBox.Name = "userBlacklistTextBox";
            userBlacklistTextBox.Size = new System.Drawing.Size(123, 189);
            userBlacklistTextBox.TabIndex = 6;
            // 
            // label3
            // 
            label3.Location = new System.Drawing.Point(282, 9);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(123, 15);
            label3.TabIndex = 5;
            label3.Text = "User Blacklist";
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Other
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(417, 263);
            Controls.Add(userBlacklistTextBox);
            Controls.Add(label3);
            Controls.Add(websiteBlacklistTextBox);
            Controls.Add(websiteBlacklistLabel);
            Controls.Add(commonUsersTextBox);
            Controls.Add(label1);
            Controls.Add(saveAndCloseButton);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Text = "Other";
            Load += Other_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button saveAndCloseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox commonUsersTextBox;
        private System.Windows.Forms.TextBox websiteBlacklistTextBox;
        private System.Windows.Forms.Label websiteBlacklistLabel;
        private System.Windows.Forms.TextBox userBlacklistTextBox;
        private System.Windows.Forms.Label label3;
    }
}