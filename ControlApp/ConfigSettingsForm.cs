using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using Microsoft.Win32;

namespace ControlApp;

public partial class ConfigSettingsForm : Form {
	private MainWindow openingWindow;

	public ConfigSettingsForm(MainWindow openingWindow) {
		this.openingWindow = openingWindow;
		InitializeComponent();
	}

	private void SaveSettings() {
		Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
		KeyValueConfigurationCollection apps = configuration.AppSettings.Settings;
		apps.Remove("LocalDrive");
		apps.Remove("AutoRun");
		apps.Remove("UserName");
		apps.Remove("Password");
		apps.Remove("Delay");
		apps.Remove("RunAll");
		apps.Add("LocalDrive", textBox1.Text);
		apps.Add("AutoRun", checkBox1.Checked ? "true" : "false");
		apps.Add("UserName", textBox2.Text);
		apps.Add("Password", Utils.Encrypt(textBox3.Text));
		apps.Add("RunAll", checkBox2.Checked ? "true" : "false");
		switch (delayCombo.SelectedIndex) {
			case 0:
				apps.Add("Delay", "30");
				break;
			case 1:
				apps.Add("Delay", "60");
				break;
			default:
				apps.Add("Delay", "120");
				break;
		}
		configuration.Save(ConfigurationSaveMode.Full);
		ConfigurationManager.RefreshSection(configuration.AppSettings.SectionInformation.Name);
		MainWindow.RefreshCredentialCache();
	}

	protected override void OnClosing(CancelEventArgs e) {
		openingWindow.UpdateTimerState();
	}

	private void confirmButton_Click(object? sender, EventArgs e) {
		SaveSettings();
		Close();
	}
	private void ConfigSettingsForm_Load(object? sender, EventArgs e) {
		textBox1.Text = ConfigurationManager.AppSettings["LocalDrive"];
		textBox2.Text = ConfigurationManager.AppSettings["UserName"];
		textBox3.Text = Utils.Decrypt(ConfigurationManager.AppSettings["Password"] ?? String.Empty);
		checkBox1.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["AutoRun"]);
		checkBox2.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["RunAll"]);
		delayCombo.SelectedIndex = ConfigurationManager.AppSettings["Delay"] switch {
			"30" => 0,
			"60" => 1,
			_ => 2,
		};
	}

	private void serverSettingsButton_Click(object sender, EventArgs e) {
		SaveSettings();
		string username = MainWindow.username != null ? MainWindow.username : "";
		string password = MainWindow.password != null ? MainWindow.password : "";
		Process.Start(
			new ProcessStartInfo {
				FileName = "https://www.thecontrolapp.co.uk/Pages/Sub/SubSettings.aspx?user=" + username + "&password=" + Utils.Decrypt(password), UseShellExecute = true
			}
		);
	}

	private void AddToStartup(object sender, EventArgs e) {
		using RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		if (key == null) {
			Utils.LogError("Could not get registry key, program not added to startup!");
			return;
		}
		key.SetValue("ControlApp", "\"" + Application.ExecutablePath + "\"");
	}

	private void RemoveFromStartup(object sender, EventArgs e) {
		using RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		if (key == null) {
			Utils.LogError("Could not get registry key, program not removed from startup!");
			return;
		}
		key.DeleteValue("ControlApp", false);
	}
}
