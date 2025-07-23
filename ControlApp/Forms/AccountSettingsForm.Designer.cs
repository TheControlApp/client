namespace ControlApp
{
    partial class AccountSettingsForm
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
            confirmButton = new Button();
            textBox2 = new TextBox();
            label2 = new Label();
            button2 = new Button();
            buttonDiscordOAuth = new Button();
            panel1 = new Panel();
            button4 = new Button();
            label4 = new Label();
            button3 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // confirmButton
            // 
            confirmButton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            confirmButton.Location = new Point(6, 239);
            confirmButton.Name = "confirmButton";
            confirmButton.Size = new Size(183, 23);
            confirmButton.TabIndex = 3;
            confirmButton.Text = "Ok";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += confirmButton_Click;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new Point(6, 25);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(103, 23);
            textBox2.TabIndex = 4;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(115, 28);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 5;
            label2.Text = "User Name";
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button2.Location = new Point(6, 210);
            button2.Name = "button2";
            button2.Size = new Size(183, 23);
            button2.TabIndex = 13;
            button2.Text = "Change Server Settings";
            button2.UseVisualStyleBackColor = true;
            // 
            // buttonDiscordOAuth
            // 
            buttonDiscordOAuth.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonDiscordOAuth.BackColor = Color.FromArgb(88, 101, 242);
            buttonDiscordOAuth.FlatAppearance.BorderSize = 0;
            buttonDiscordOAuth.FlatStyle = FlatStyle.Flat;
            buttonDiscordOAuth.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonDiscordOAuth.ForeColor = Color.White;
            buttonDiscordOAuth.Image = Properties.Resources.discord_logo;
            buttonDiscordOAuth.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDiscordOAuth.Location = new Point(6, 54);
            buttonDiscordOAuth.Name = "buttonDiscordOAuth";
            buttonDiscordOAuth.Padding = new Padding(5, 0, 0, 0);
            buttonDiscordOAuth.Size = new Size(183, 30);
            buttonDiscordOAuth.TabIndex = 21;
            buttonDiscordOAuth.Text = "  Link Discord Account";
            buttonDiscordOAuth.TextAlign = ContentAlignment.MiddleLeft;
            buttonDiscordOAuth.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDiscordOAuth.UseVisualStyleBackColor = false;
            buttonDiscordOAuth.Click += buttonDiscordOAuth_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(button4);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(button3);
            panel1.Location = new Point(6, 115);
            panel1.Name = "panel1";
            panel1.Size = new Size(183, 63);
            panel1.TabIndex = 18;
            // 
            // button4
            // 
            button4.Location = new Point(101, 27);
            button4.Name = "button4";
            button4.Size = new Size(65, 23);
            button4.TabIndex = 18;
            button4.Text = "Remove";
            button4.UseVisualStyleBackColor = true;
            button4.Click += RemoveFromStartup;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 0);
            label4.Name = "label4";
            label4.Size = new Size(97, 15);
            label4.TabIndex = 0;
            label4.Text = "Windows Startup";
            // 
            // button3
            // 
            button3.Location = new Point(14, 27);
            button3.Name = "button3";
            button3.Size = new Size(65, 23);
            button3.TabIndex = 17;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = true;
            button3.Click += AddToStartup;
            // 
            // AccountSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(196, 378);
            Controls.Add(buttonDiscordOAuth);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(confirmButton);
            MaximizeBox = false;
            MaximumSize = new Size(99999, 417);
            MinimizeBox = false;
            MinimumSize = new Size(212, 417);
            Name = "AccountSettingsForm";
            Text = "Config";
            Load += ConfigSettingsForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private Button buttonDiscordOAuth;
        private Panel panel1;
        private Button button4;
        private Label label4;
        private Button button3;
    }
}