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
            string commonUserList = Utils.FormArrayString(commonUsersTextBox.Lines);
            apps.Add("CommonUsers", commonUserList);

            apps.Remove("BlackList");
            string blacklist = Utils.FormArrayString(websiteBlacklistTextBox.Lines);
            apps.Add("BlackList", blacklist);

            apps.Remove("UserBList");
            string userBlacklist = Utils.FormArrayString(userBlacklistTextBox.Lines);
            apps.Add("UserBList", userBlacklist);
            myconfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            Close();
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
            if (commonUserList != null) {
                string[] users = Utils.SeparateArrayString(commonUserList);
                commonUsersTextBox.Lines = users;
            }
            string? blacklist = ConfigurationManager.AppSettings["BlackList"];
            if (blacklist != null) {
                string[] blacklistedSites = Utils.SeparateArrayString(blacklist);
                websiteBlacklistTextBox.Lines = blacklistedSites;
            }
            string? userBlacklist = ConfigurationManager.AppSettings["UserBList"];
            if (userBlacklist != null) {
                string[] blacklistedUsers = Utils.SeparateArrayString(userBlacklist);
                userBlacklistTextBox.Lines = blacklistedUsers;
            }
        }
    }
}
