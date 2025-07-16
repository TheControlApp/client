using ControlApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            button1.Click += async (s, e) => await LoginUser(s, e);
        }

        private async Task LoginUser(object sender, EventArgs e)
        {
            button1.Enabled = false;
            //TODO: fetch data from form
            Login loginModel = new()
            {
                Username = textBox2.Text,
                Password = textBox1.Text
            };
            //TODO: validate data (password more than 10 characters, letters, numbers and special characters at least one of each)(username more than 5 characters only numbers and letters)

            string receivedToken = await ServerCommunicator.LoginAndGetTokenAsync(loginModel);

            if (!string.IsNullOrEmpty(receivedToken))
            {
                OnSuccessfulAuthentication(receivedToken);
            }
            else
            {
                MessageBox.Show("Invalid credentials", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Re-enable the button if login fails
                button1.Enabled = true;
            }
        }
        private void RegisterUser(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        // Placeholder for successful authentication. Replace with actual server interaction!
        private void OnSuccessfulAuthentication(string token)
        {
            // Store the token securely:
            SecureTokenStorage.SaveToken(token);

            // Indicate a successful operation and close the form:
            DialogResult = DialogResult.OK;
            Close();
        }
        
    }
}
