using ControlApp.Commands;
using ControlApp.Models;
using ControlApp.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControlApp.Services
{
    /// <summary>
    /// Handles loading and providing application settings from appsettings.json.
    /// </summary>
    public static class ConfigurationService
    {
        private static readonly string _configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        private static readonly object _fileLock = new();
        private static readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            // This converter ensures that enums are written as strings (e.g., "MEDIUM")
            // rather than their integer values.
            Converters = { new JsonStringEnumConverter() }
        }; 
        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        /// <summary>
        /// Gets the loaded application configuration.
        /// </summary>
        public static CommandConfig CommandSettings { get; private set; }

        /// <summary>
        /// Loads configuration from appsettings.json and binds it to the Settings property.
        /// </summary>
        public static void LoadConfiguration()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    throw new FileNotFoundException("appsettings.json not found. A default will be created.");
                }
                var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

                CommandSettings = configuration.Get<CommandConfig>();
                // Check if critical sections are missing after loading.
                if (CommandSettings?.General?.DisallowedCommands == null || CommandSettings.General.MiscellaneousConfigs == null)
                {
                    throw new InvalidDataException("Configuration file is incomplete. A default will be created.");
                }
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is JsonException || ex is InvalidDataException)
            {
                Utilities.LogWarning($"Configuration error: {ex.Message}. Applying and saving default settings.");
                CommandSettings = GetDefaultSettings();
                SaveSettings(); // Create a fresh, valid appsettings.json
            }
            // Ensure consistency and set dynamic defaults. This runs for both loaded and default settings.
            ProcessAndValidateSettings();
        }

        /// <summary>
        /// Creates a default, safe-to-run configuration object.
        /// </summary>
        /// <returns>A new CommandConfig instance with default values.</returns>
        private static CommandConfig GetDefaultSettings()
        {
            return new CommandConfig
            {
                General = new GeneralCommandConfig
                {
                    DisallowedCommands = new DisAllowGeneralCommandConfig
                    {
                        // By default, allow most commands but disable potentially dangerous ones.
                        DisallowPopUps = false,
                        DisallowWriteForMe = false,
                        DisallowSendOrDelete = true,
                        //DisallowSpinner = false,
                        DisallowWatchForMe = false,
                        DisallowSubliminals = true,
                        DisallowAudios = true,
                        DisallowDownloads = true,
                        DisallowRunnableFiles = true, // Dangerous
                        DisallowWallpapers = true,
                        DisallowOpenWebsite = true,
                        DisallowScreenshots = true, // Dangerous
                        DisallowWebcam = true,     // Dangerous
                        DisallowDisableMouse = true, // Dangerous
                        DisallowDisableInput = true, // Dangerous
                        DisallowTTS = true,
                        DisallowTwitterPoster = true,
                        DisallowMessages = false,
                        DisallowOutstandingReminders = false
                    },
                    MiscellaneousConfigs = new MiscellaneousGeneralCommandConfig
                    {
                        // DownloadsFolderPath will be set by ProcessAndValidateSettings
                        DownloadsFolderPath = string.Empty,
                        AutoRunSentExe = false,
                        DarkMode = false,
                        RunAllOutstanding = false,
                        RunAllOutstandingDelaySeconds = 0,
                        Showblocked = false,
                        WebcamCountdown = true
                    }
                },
                PopUps = new PopUpsCommandConfig
                {
                    PanelConfig = new PanelPopUpsCommandConfig
                    {
                        PopupsClickability = PanelPopupsClickability.STANDARD,
                        PopupsFullScreen = false,
                        PopupsMovement = PanelPopupsMovement.NONE,
                        PopupsOpacity = PanelPopupsOpacity.STANDARD
                    },
                    PopupsDisplayBehavior = PopupsDisplayBehavior.ONE,
                    PopupsDisplayLength = PopupsDisplayLength.SHORT 
                },
                WallPaper = new WallPaperCommandConfig
                {
                    Fitting = WallpaperFitting.FITSCREEN
                }
            };
        }

        /// <summary>
        /// Performs post-load validation and applies dynamic defaults.
        /// </summary>
        private static void ProcessAndValidateSettings()
        {
            // Logic to ensure disallowed input also disallows mouse
            if (CommandSettings.General.DisallowedCommands.DisallowDisableInput && !CommandSettings.General.DisallowedCommands.DisallowDisableMouse)
            {
                CommandSettings.General.DisallowedCommands.DisallowDisableMouse = true;
                Utilities.LogInfo("Configuration updated: Disallowing mouse input because keyboard/input is disallowed.");
            }

            // Logic to set default download path if not specified
            string? downloadPath = CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath;
            if (string.IsNullOrEmpty(downloadPath))
            {
                CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath = GetDownloadPath() + "\\";
            }
            else if (!downloadPath.EndsWith('\\'))
            {
                CommandSettings.General.MiscellaneousConfigs.DownloadsFolderPath += "\\";
            }
        }

        /// <summary>
        /// Saves the current state of the Settings object back to appsettings.json.
        /// This method is thread-safe.
        /// </summary>
        public static void SaveSettings()
        {
            lock (_fileLock)
            {
                string jsonString = JsonSerializer.Serialize(CommandSettings, options);
                File.WriteAllText(_configFilePath, jsonString);
            }
        }

        /// <summary>
        /// Reads the loaded configuration and returns a list of command codes for features that the user has disabled.
        /// </summary>
        /// <returns>A List of strings, where each string is a command code from the CommandCodes class.</returns>
        public static List<string> GetDisallowedCommandsList()
        {
            if (CommandSettings == null)
            {
                throw new InvalidOperationException("Configuration has not been loaded. Call LoadConfiguration() first.");
            }

            var disallowedCommands = new List<string>();
            var settings = CommandSettings.General.DisallowedCommands;

            // UI & Interaction
            if (settings.DisallowPopUps) 
            { 
                disallowedCommands.Add(CommandCodes.PopupText);
                disallowedCommands.Add(CommandCodes.PopupMedia);
            }
            if (settings.DisallowWriteForMe) disallowedCommands.Add(CommandCodes.WriteForMe);
            if (settings.DisallowSendOrDelete) disallowedCommands.Add(CommandCodes.SendDelete);
            //if (settings.DisallowSpinner) disallowedCommands.Add(CommandCodes.Spinner);
            if (settings.DisallowWatchForMe) disallowedCommands.Add(CommandCodes.WatchForMe);

            // System & File Ops
            if (settings.DisallowAudios) disallowedCommands.Add(CommandCodes.Audio);
            if (settings.DisallowDownloads) disallowedCommands.Add(CommandCodes.Download);
            if (settings.DisallowRunnableFiles) disallowedCommands.Add(CommandCodes.Runnable);
            if (settings.DisallowWallpapers) disallowedCommands.Add(CommandCodes.Wallpaper);
            if (settings.DisallowOpenWebsite) disallowedCommands.Add(CommandCodes.Website);

            // Surveillance & Control
            if (settings.DisallowScreenshots) disallowedCommands.Add(CommandCodes.Screenshot);
            if (settings.DisallowWebcam) disallowedCommands.Add(CommandCodes.Webcam);
            if (settings.DisallowDisableMouse) disallowedCommands.Add(CommandCodes.MouseDisable);
            if (settings.DisallowDisableInput) disallowedCommands.Add(CommandCodes.InputDisable);
            if (settings.DisallowTTS) disallowedCommands.Add(CommandCodes.TTS);

            // Social
            if (settings.DisallowTwitterPoster) disallowedCommands.Add(CommandCodes.Twitter);

            if (settings.DisallowSubliminals)
            {
                disallowedCommands.Add(CommandCodes.SubliminalImage);
                disallowedCommands.Add(CommandCodes.SubliminalText);
                disallowedCommands.Add(CommandCodes.SubliminalLoop);
            }

            return disallowedCommands;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern uint SHGetKnownFolderPath(ref Guid id, int flags, nint token, out nint path);

        private static string GetDownloadPath()
        {
            if (Environment.OSVersion.Version.Major < 6)
            {
                throw new NotSupportedException();
            }
            uint sysCallCode = SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out nint pathPtr);
            if (sysCallCode != 0) throw new IOException("System call failed with error code " + sysCallCode); // if sys call returns something other than S_OK...
            string? result = Marshal.PtrToStringUni(pathPtr);
            Marshal.FreeCoTaskMem(pathPtr);
            if (result == null)
            {
                throw new IOException("System could not retrieve download destination");
            }
            return result;
        }
    }
}

