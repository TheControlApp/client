using System.Configuration;

namespace ControlApp
{
    public partial class Other : Form {
        public Other() {
            InitializeComponent();
        }

        private void SaveAndCloseButton_Click(object sender, EventArgs e) {
            Configuration myconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection apps = myconfig.AppSettings.Settings;
            apps.Remove("CommonUsers");
            string commonUserList = "";
            foreach (string line in textBox1.Lines) {
                commonUserList += $"[{line}],";
            }
            if (commonUserList.Length > 0) commonUserList = commonUserList.Remove(commonUserList.Length - 1); // remove last char
            apps.Add("CommonUsers", commonUserList);

            apps.Remove("BlackList");
            string blacklist = "";
            foreach (string line in textBox2.Lines) {
                blacklist += $"[{line}],";
            }
            if (blacklist.Length > 0) blacklist = blacklist.Remove(blacklist.Length - 1); // remove last char
            apps.Add("BlackList", blacklist);

            apps.Remove("UserBList");
            string userBlacklist = "";
            foreach (string line in textBox2.Lines) {
                userBlacklist += $"[{line}],";
            }
            if (userBlacklist.Length > 0) userBlacklist = userBlacklist.Remove(userBlacklist.Length - 1); // remove last char
            apps.Add("UserBList", userBlacklist);
            myconfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }

        private void Other_Load(object sender, EventArgs e) {
            if (!Utils.CheckEnabled("DarkMode")) return;
            BackColor = Color.Black;
            ForeColor = Color.White;
            foreach (Control control in Controls) {
                if (control is Panel) {
                    control.BackColor = Color.Black;
                    control.ForeColor = Color.White;
                }
                if (control is Button) {
                    control.BackColor = Color.DarkGray;
                    control.ForeColor = Color.White;
                }
            }
            string? commonUserList = ConfigurationManager.AppSettings["CommonUsers"];
            string? blacklist = ConfigurationManager.AppSettings["BlackList"];
            string? userBlacklist = ConfigurationManager.AppSettings["UserBList"];
            if (commonUserList != null) {
                string[] users = Utils.SeparateString(commonUserList);
                textBox1.Lines = users;
            }
            if (blacklist != null) {
                string[] blists = Utils.SeparateString(blacklist);
                textBox2.Lines = blists;
            }
            if (userBlacklist != null) {
                string[] blists = Utils.SeparateString(userBlacklist);
                textBox3.Lines = blists;
            }
        }
    }
}
