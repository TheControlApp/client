namespace ControlApp
{
    partial class ContactsList
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
            saveAndCloseButton = new Button();
            label3 = new Label();
            blockListBox = new ListBox();
            knownListBox = new ListBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // saveAndCloseButton
            // 
            saveAndCloseButton.Enabled = false;
            saveAndCloseButton.Location = new Point(96, 228);
            saveAndCloseButton.Name = "saveAndCloseButton";
            saveAndCloseButton.Size = new Size(157, 23);
            saveAndCloseButton.TabIndex = 0;
            saveAndCloseButton.Text = "Save and Close";
            saveAndCloseButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(157, 21);
            label3.TabIndex = 5;
            label3.Text = "Blocked Users";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // blockListBox
            // 
            blockListBox.FormattingEnabled = true;
            blockListBox.ItemHeight = 15;
            blockListBox.Location = new Point(12, 33);
            blockListBox.Name = "blockListBox";
            blockListBox.Size = new Size(157, 184);
            blockListBox.TabIndex = 7;
            // 
            // knownListBox
            // 
            knownListBox.FormattingEnabled = true;
            knownListBox.ItemHeight = 15;
            knownListBox.Location = new Point(175, 33);
            knownListBox.Name = "knownListBox";
            knownListBox.Size = new Size(157, 184);
            knownListBox.TabIndex = 8;
            // 
            // label1
            // 
            label1.Location = new Point(175, 9);
            label1.Name = "label1";
            label1.Size = new Size(157, 21);
            label1.TabIndex = 9;
            label1.Text = "Known Users";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // BlockList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(344, 263);
            Controls.Add(label1);
            Controls.Add(knownListBox);
            Controls.Add(blockListBox);
            Controls.Add(label3);
            Controls.Add(saveAndCloseButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BlockList";
            Text = "Contacts";
            Load += Other_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button saveAndCloseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox commonUsersTextBox;
        private System.Windows.Forms.TextBox websiteBlacklistTextBox;
        private System.Windows.Forms.Label websiteBlacklistLabel;
        private System.Windows.Forms.Label label3;
        private ListBox blockListBox;
        private ListBox knownListBox;
    }
}