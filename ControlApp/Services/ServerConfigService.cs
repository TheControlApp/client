// ==== File: ControlApp\Services\ServerConfigService.cs ====

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlApp.Models; // Assurez-vous que ce using est présent
using ControlApp.Utils;

namespace ControlApp.Services
{
    public static class ServerConfigService
    {
        private static List<string> _bannedWords = [];
        private static List<string> _bannedSites = [];

        public static IReadOnlyList<string> BannedWords => _bannedWords.AsReadOnly();
        public static IReadOnlyList<string> BannedSites => _bannedSites.AsReadOnly();
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// Initializes the service by fetching the complete configuration from the server
        /// via the ServerCommunicator.
        /// </summary>
        public static async Task InitializeAsync()
        {
            try
            {
                Utilities.LogInfo("Fetching server configuration...");

                // Appel de la méthode unique et centralisée dans le ServerCommunicator
                ServerConfig config = await ServerCommunicator.FetchServerConfigurationAsync();

                // Stocke les listes récupérées
                _bannedWords = config.BannedWords;
                _bannedSites = config.BannedSites;

                IsInitialized = true;
                Utilities.LogInfo($"Server configuration loaded successfully: {_bannedWords.Count} banned words, {_bannedSites.Count} banned sites.");
            }
            catch (Exception ex)
            {
                // Le service gère toutes les exceptions lancées par le Communicator
                IsInitialized = false;
                _bannedWords = [];
                _bannedSites = [];
                Utilities.LogError($"Failed to initialize ServerConfigService: {ex.Message}");
                // L'application continuera de fonctionner avec des listes vides, mais une erreur critique sera enregistrée.
            }
        }
    }
}