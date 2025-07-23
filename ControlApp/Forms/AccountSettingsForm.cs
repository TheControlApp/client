using System.Configuration;
using ControlApp.Utils;
using Microsoft.Win32;

namespace ControlApp;

public partial class AccountSettingsForm : Form
{
    private class DiscordStatus
    {
        public bool Connected { get; set; }
        public string DiscordUsername { get; set; }
    }

    public AccountSettingsForm()
    {
        InitializeComponent();
    }

    private void SaveSettings()
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        KeyValueConfigurationCollection apps = configuration.AppSettings.Settings;
        apps.Remove("UserName");
        apps.Add("UserName", textBox2.Text);
        configuration.Save(ConfigurationSaveMode.Full);
        ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
    }

    private void confirmButton_Click(object? sender, EventArgs e)
    {
        SaveSettings();
        Close();
    }
    private void ConfigSettingsForm_Load(object? sender, EventArgs e)
    {
        textBox2.Text = ConfigurationManager.AppSettings["UserName"];
        if (MainWindow.verified)
        {
            buttonDiscordOAuth.Text = "  Account Linked";
            buttonDiscordOAuth.Enabled = false;
            buttonDiscordOAuth.BackColor = Color.FromArgb(67, 181, 129);
        }
    }

    private void AddToStartup(object sender, EventArgs e)
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        if (key == null)
        {
            Utilities.LogError("Could not get registry key, program not added to startup!");
            return;
        }
        key.SetValue("ControlApp", "\"" + Application.ExecutablePath + "\"");
    }

    private void RemoveFromStartup(object sender, EventArgs e)
    {
        using RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        if (key == null)
        {
            Utilities.LogError("Could not get registry key, program not removed from startup!");
            return;
        }
        key.DeleteValue("ControlApp", false);
    }

    private void buttonDiscordOAuth_Click(object sender, EventArgs e)
    {
        // --- DUMMY IMPLEMENTATION ---
        // Ici, vous lanceriez normalement le flux OAuth2 de Discord dans un navigateur.
        // Pour la simulation, nous allons simplement afficher un message et suggérer un redémarrage.

        // 1. Simuler l'ouverture du navigateur pour l'autorisation Discord
        // Process.Start(new ProcessStartInfo { FileName = "https://your-backend.com/discord/auth", UseShellExecute = true });

        // 2. Simuler une réponse réussie du backend
        MessageBox.Show(
            "Your Discord account has been linked successfully!\n\nPlease restart the application to access all features.",
            "Success",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);

        // Dans une vraie application, le backend notifierait le client (via SignalR par exemple)
        // ou le client vérifierait périodiquement le statut. Un redémarrage est la simulation la plus simple.
        Close();
    }
}
