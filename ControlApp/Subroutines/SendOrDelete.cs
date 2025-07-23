using ControlApp.Commands;
using ControlApp.Services;
using ControlApp.Utils;
using System.Configuration;
using System.Text.Json;

namespace ControlApp.Subroutines;

public partial class SendOrDelete : Form {
	private string candidateFile;

	private string senderId;

	public SendOrDelete(string senderId) {
		string? location = ConfigurationService.CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath;
		if (location == null) return;
		List<string> candidateList = new List<string>();
		foreach (string file in Directory.GetFiles(location)) {
			FileInfo info = new FileInfo(file);
			if ((!Utilities.IsAnimatedFile(file) && !Utilities.IsImageFile(file)) || info.Length >= 1000000) continue;
			candidateList.Add(file);
		}
		if (candidateList.Count <= 0) return;
		Random rnd = new Random();
		candidateFile = candidateList[rnd.Next(candidateList.Count)];
		this.senderId = senderId;
		
		InitializeComponent();
	}

	private async void deleteButton_Click(object sender, EventArgs e) {
        File.Delete(candidateFile);

        // Crée une commande simple de type "message box".
        var messageContent = new { body = $"{AccountService.CurrentUser.Username} chose to delete." };
        var messageCommand = new CommandStructure
        {
            Type = CommandCodes.PopupText,
            Content = JsonSerializer.SerializeToElement(messageContent)
        };

        await WebSocketsCommunicator.SendCommandAsync(senderId, new List<CommandStructure> { messageCommand }, false);
        Close();
    }

	private void SendOrDelete_Load(object sender, EventArgs e) {
		axWindowsMediaPlayer1.URL = candidateFile;
		axWindowsMediaPlayer1.Ctlenabled = false;
		axWindowsMediaPlayer1.uiMode = "None";
		axWindowsMediaPlayer1.settings.autoStart = true;
		axWindowsMediaPlayer1.settings.setMode("loop", varfMode: true);
	}

	private async void sendButton_CLick(object sender, EventArgs e) {
        if (!ServerCommunicator.SendFtpFile(candidateFile))
        {
            return;
        }
        // Crée une liste pour contenir nos commandes de réponse.
        var responseCommands = new List<CommandStructure>();
        // 1. Crée la commande "message box".
        var messageContent = new { body = $"{AccountService.CurrentUser.Username} chose to send." };
        responseCommands.Add(new CommandStructure
        {
            Type = CommandCodes.PopupText,
            Content = JsonSerializer.SerializeToElement(messageContent)
        });

        // 2. Crée la commande "popup-media" pour afficher le fichier.
        string fileName = Path.GetFileName(candidateFile);
        var popupContent = new { url = $"https://www.thecontrolapp.co.uk/storage/{fileName}" };
        responseCommands.Add(new CommandStructure
        {
            Type = CommandCodes.PopupMedia,
            Content = JsonSerializer.SerializeToElement(popupContent)
        });

        // Envoie la charge utile contenant les DEUX commandes.
        await WebSocketsCommunicator.SendCommandAsync(senderId, responseCommands, false);
        Close();
	}
}
