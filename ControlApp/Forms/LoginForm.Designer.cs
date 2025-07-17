namespace ControlApp.Forms
{
    partial class LoginForm
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
            tabControlAuth = new TabControl();
            tabPageLogin = new TabPage();
            panel1 = new Panel();
            label1 = new Label();
            label2 = new Label();
            textBoxPasswordLogin = new TextBox();
            textBoxUsernameLogin = new TextBox();
            buttonLogin = new Button();
            tabPageRegister = new TabPage();
            panel2 = new Panel();
            label7 = new Label();
            textBoxDisplayNameRegister = new TextBox();
            checkBoxOptinRandomRegister = new CheckBox();
            label6 = new Label();
            label5 = new Label();
            textBoxPasswordRegister = new TextBox();
            label3 = new Label();
            label4 = new Label();
            textBoxEmailRegister = new TextBox();
            textBoxUsernameRegister = new TextBox();
            buttonRegister = new Button();
            tabControlAuth.SuspendLayout();
            tabPageLogin.SuspendLayout();
            panel1.SuspendLayout();
            tabPageRegister.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlAuth
            // 
            tabControlAuth.Controls.Add(tabPageLogin);
            tabControlAuth.Controls.Add(tabPageRegister);
            tabControlAuth.Location = new Point(-4, -1);
            tabControlAuth.Name = "tabControlAuth";
            tabControlAuth.SelectedIndex = 0;
            tabControlAuth.Size = new Size(805, 452);
            tabControlAuth.TabIndex = 0;
            // 
            // tabPageLogin
            // 
            tabPageLogin.Controls.Add(panel1);
            tabPageLogin.Controls.Add(buttonLogin);
            tabPageLogin.Location = new Point(4, 24);
            tabPageLogin.Name = "tabPageLogin";
            tabPageLogin.Padding = new Padding(3);
            tabPageLogin.Size = new Size(797, 424);
            tabPageLogin.TabIndex = 0;
            tabPageLogin.Text = "Login";
            tabPageLogin.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(textBoxPasswordLogin);
            panel1.Controls.Add(textBoxUsernameLogin);
            panel1.Location = new Point(256, 135);
            panel1.Name = "panel1";
            panel1.Size = new Size(280, 100);
            panel1.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 20);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 8;
            label1.Text = "Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(0, 56);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 9;
            label2.Text = "Password";
            // 
            // textBoxPasswordLogin
            // 
            textBoxPasswordLogin.Location = new Point(66, 53);
            textBoxPasswordLogin.Name = "textBoxPasswordLogin";
            textBoxPasswordLogin.Size = new Size(209, 23);
            textBoxPasswordLogin.TabIndex = 6;
            // 
            // textBoxUsernameLogin
            // 
            textBoxUsernameLogin.Location = new Point(66, 17);
            textBoxUsernameLogin.Name = "textBoxUsernameLogin";
            textBoxUsernameLogin.Size = new Size(209, 23);
            textBoxUsernameLogin.TabIndex = 7;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(351, 241);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(75, 23);
            buttonLogin.TabIndex = 5;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = true;
            // 
            // tabPageRegister
            // 
            tabPageRegister.Controls.Add(panel2);
            tabPageRegister.Controls.Add(buttonRegister);
            tabPageRegister.Location = new Point(4, 24);
            tabPageRegister.Name = "tabPageRegister";
            tabPageRegister.Padding = new Padding(3);
            tabPageRegister.Size = new Size(797, 424);
            tabPageRegister.TabIndex = 1;
            tabPageRegister.Text = "Register";
            tabPageRegister.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(label7);
            panel2.Controls.Add(textBoxDisplayNameRegister);
            panel2.Controls.Add(checkBoxOptinRandomRegister);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(textBoxPasswordRegister);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(textBoxEmailRegister);
            panel2.Controls.Add(textBoxUsernameRegister);
            panel2.Location = new Point(230, 67);
            panel2.Name = "panel2";
            panel2.Size = new Size(345, 203);
            panel2.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(0, 94);
            label7.Name = "label7";
            label7.Size = new Size(78, 15);
            label7.TabIndex = 16;
            label7.Text = "Display name";
            // 
            // textBoxDisplayNameRegister
            // 
            textBoxDisplayNameRegister.Location = new Point(126, 91);
            textBoxDisplayNameRegister.Name = "textBoxDisplayNameRegister";
            textBoxDisplayNameRegister.Size = new Size(209, 23);
            textBoxDisplayNameRegister.TabIndex = 15;
            // 
            // checkBoxOptinRandomRegister
            // 
            checkBoxOptinRandomRegister.AutoSize = true;
            checkBoxOptinRandomRegister.CheckAlign = ContentAlignment.MiddleRight;
            checkBoxOptinRandomRegister.Location = new Point(0, 176);
            checkBoxOptinRandomRegister.Name = "checkBoxOptinRandomRegister";
            checkBoxOptinRandomRegister.Size = new Size(140, 19);
            checkBoxOptinRandomRegister.TabIndex = 14;
            checkBoxOptinRandomRegister.Text = "Opt-in random mode";
            checkBoxOptinRandomRegister.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(0, 136);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 136);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 11;
            label5.Text = "Password";
            // 
            // textBoxPasswordRegister
            // 
            textBoxPasswordRegister.Location = new Point(126, 133);
            textBoxPasswordRegister.Name = "textBoxPasswordRegister";
            textBoxPasswordRegister.Size = new Size(209, 23);
            textBoxPasswordRegister.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(0, 20);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 8;
            label3.Text = "Username";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(0, 56);
            label4.Name = "label4";
            label4.Size = new Size(36, 15);
            label4.TabIndex = 9;
            label4.Text = "Email";
            // 
            // textBoxEmailRegister
            // 
            textBoxEmailRegister.Location = new Point(126, 53);
            textBoxEmailRegister.Name = "textBoxEmailRegister";
            textBoxEmailRegister.Size = new Size(209, 23);
            textBoxEmailRegister.TabIndex = 6;
            // 
            // textBoxUsernameRegister
            // 
            textBoxUsernameRegister.Location = new Point(126, 17);
            textBoxUsernameRegister.Name = "textBoxUsernameRegister";
            textBoxUsernameRegister.Size = new Size(209, 23);
            textBoxUsernameRegister.TabIndex = 7;
            // 
            // buttonRegister
            // 
            buttonRegister.Location = new Point(230, 276);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(345, 52);
            buttonRegister.TabIndex = 11;
            buttonRegister.Text = "Register";
            buttonRegister.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControlAuth);
            Name = "LoginForm";
            Text = "LoginForm";
            tabControlAuth.ResumeLayout(false);
            tabPageLogin.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabPageRegister.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControlAuth;
        private TabPage tabPageLogin;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private TextBox textBoxPasswordLogin;
        private TextBox textBoxUsernameLogin;
        private Button buttonLogin;
        private TabPage tabPageRegister;
        private Panel panel2;
        private Label label3;
        private Label label4;
        private TextBox textBoxEmailRegister;
        private TextBox textBoxUsernameRegister;
        private Button buttonRegister;
        private Label label5;
        private TextBox textBoxPasswordRegister;
        private Label label6;
        private CheckBox checkBoxOptinRandomRegister;
        private Label label7;
        private TextBox textBoxDisplayNameRegister;
    }
}