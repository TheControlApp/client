using ControlApp.Models;
using ControlApp.Services;
using ControlApp.Utils;
using System.Configuration;

namespace ControlApp
{
    public partial class ContactsList : Form {
        public ContactsList() {
            InitializeComponent();
        }

        private void Other_Load(object sender, EventArgs e)
        {
            UserAccount? u = AccountService.CurrentUser;
            if (u != null)
            {
                foreach (string s in u.BlackList)
                {
                    blockListBox.Items.Add(s);
                }

                foreach (string s in u.KnownList)
                {
                    knownListBox.Items.Add(s);
                }
            } else
            {
                Close();
            }
        }
    }
}
