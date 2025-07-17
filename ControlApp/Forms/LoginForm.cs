using ControlApp.Exceptions;
using ControlApp.Exceptions.LoginExceptions;
using ControlApp.Exceptions.RegisterExceptions;
using ControlApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp.Forms
{
    public partial class LoginForm : Form
    {
        [GeneratedRegex("^[a-zA-Z0-9]{5,}$")]
        private static partial Regex UsernameRegex();
        [GeneratedRegex("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,}$")]
        private static partial Regex PasswordRegex();
        //https://i.sstatic.net/YI6KR.png - state machine of this regex
        [GeneratedRegex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])")]
        private static partial Regex EmailRegex();

        public LoginForm()
        {
            InitializeComponent();
            buttonLogin.Click += async (s, e) => await LoginUser(s, e);
            buttonRegister.Click += async (s, e) => await RegisterUser(s, e);
        }

        private async Task LoginUser(object sender, EventArgs e)
        {
            buttonLogin.Enabled = false;
            Login loginModel = new()
            {
                Username = textBoxUsernameLogin.Text,
                Password = textBoxPasswordLogin.Text
            };

            if (!UsernameRegex().IsMatch(loginModel.Username))
            {
                MessageBox.Show("Invalid username.\nUsername must be at least 5 characters long and contain only letters and numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonLogin.Enabled = true;
                return;
            }

            if (!PasswordRegex().IsMatch(loginModel.Password))
            {
                MessageBox.Show("Invalid password.\nPassword must be at least 10 characters long and include at least one letter, one number, and one special character.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonLogin.Enabled = true;
                return;
            }

            string receivedToken = null;
            try
            {
                receivedToken = await ServerCommunicator.LoginAndGetTokenAsync(loginModel);
            }
            catch (WrongLoginOrPaswordException)
            {
                MessageBox.Show("The username or password you entered is incorrect.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UpgradeAppVerionException)
            {
                MessageBox.Show("Your application version is outdated. Please upgrade to the latest version to continue.", "Upgrade Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable the button if login fails for any reason
                if (string.IsNullOrEmpty(receivedToken))
                {
                    buttonLogin.Enabled = true;
                }
            }

            if (!string.IsNullOrEmpty(receivedToken))
            {
                OnSuccessfulAuthentication(receivedToken);
            }
        }
        private async Task RegisterUser(object sender, EventArgs e)
        {
            buttonRegister.Enabled = false;
            Register registerModel = new()
            {
                UserName = textBoxUsernameRegister.Text,
                Password = textBoxPasswordRegister.Text,
                Email = textBoxEmailRegister.Text,
                RandomOptIn = checkBoxOptinRandomRegister.Checked,
                ScreenName = textBoxDisplayNameRegister.Text
            };

            if (!UsernameRegex().IsMatch(registerModel.UserName))
            {
                MessageBox.Show("Invalid username.\nUsername must be at least 5 characters long and contain only letters and numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonRegister.Enabled = true;
                return;
            }
            if (!UsernameRegex().IsMatch(registerModel.ScreenName))
            {
                MessageBox.Show("Invalid display name.\nDisplay name must be at least 5 characters long and contain only letters and numbers.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonRegister.Enabled = true;
                return;
            }
            if (!EmailRegex().IsMatch(registerModel.Email))
            {
                MessageBox.Show("Invalid email.\nEmail must be valid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonRegister.Enabled = true;
                return;
            }
            if (!PasswordRegex().IsMatch(registerModel.Password))
            {
                MessageBox.Show("Invalid password.\nPassword must be at least 10 characters long and include at least one letter, one number, and one special character.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonRegister.Enabled = true;
                return;
            }

            bool registrationSuccess = false;
            try
            {
                registrationSuccess = await ServerCommunicator.RegisterAsync(registerModel);
            }
            catch (UserNameOrEmailAlreadyInUseException)
            {
                MessageBox.Show("This username or email is already in use. Please choose another.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BadEmailException)
            {
                MessageBox.Show("The email address you entered is not valid.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (BadPasswordException)
            {
                MessageBox.Show("The password does not meet the security requirements.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedUserNameException)
            {
                MessageBox.Show("The username you have chosen is not allowed.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UpgradeAppVerionException)
            {
                MessageBox.Show("Your application version is outdated. Please upgrade to the latest version to continue.", "Upgrade Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred during registration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable the button if registration fails
                if (!registrationSuccess)
                {
                    buttonRegister.Enabled = true;
                }
            }

            if (!registrationSuccess)
            {
                OnSuccessfulRegistration(registerModel.UserName);
            }
        }

        private void OnSuccessfulAuthentication(string token)
        {
            // Store the token securely:
            SecureTokenStorage.SaveToken(token);

            // Indicate a successful operation and close the form:
            DialogResult = DialogResult.OK;
            Close();
        }
        private void OnSuccessfulRegistration(string registeredUsername)
        {
            // 1. Show a success message
            MessageBox.Show("Registration successful! Please log in to continue.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 2. Remove the register tab page from the tab control
            // (Assuming a TabControl named tabControlAuth with a TabPage named tabPageRegister)
            if (Controls.ContainsKey("tabControlAuth") && tabControlAuth.TabPages.ContainsKey("tabPageRegister"))
            {
                tabControlAuth.TabPages.RemoveByKey("tabPageRegister");
            }

            // 3. Automatically switch to the login tab and pre-fill the username
            // (Assuming the login tab is now at index 0)
            tabControlAuth.SelectedIndex = 0;
            textBoxUsernameLogin.Text = registeredUsername;
            textBoxPasswordLogin.Focus(); // Set focus to the password field for convenience
        }
    }
}
