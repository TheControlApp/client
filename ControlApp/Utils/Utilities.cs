using ControlApp.Models;
using FluentFTP.Helpers;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;

namespace ControlApp.Utils;

public static class Utilities {

    private static readonly string LOG_INFO_PREFIX = "[INFO]";
    private static readonly string LOG_WARN_PREFIX = "[WARNING]";
    private static readonly string LOG_ERR_PREFIX = "[ERROR]";

    private static readonly Regex splitterRegex =
        new Regex(@",?\[([a-zA-Z0-9:/\\\.\-|: =]*)]", RegexOptions.None, TimeSpan.FromSeconds(1));

    private static readonly string logFolderName = "logs";
    private static readonly string logPathFormat = @"yyyy-MM-dd-HH-mm-ss"".txt""";

    private static StreamWriter? logWriter;
        
    private static StreamWriter InitializeLog() {
        if (!File.Exists(logFolderName)) Directory.CreateDirectory(logFolderName);
        string logFileName = DateTime.Now.ToString(logPathFormat);
        string logPath = Path.Join(logFolderName, logFileName);
        StreamWriter logStream = new StreamWriter(File.Create(logPath));
        Console.WriteLine("Created log at " + logPath);
        return logStream;
    }

    private static string CreateTimePrefix() {
        return DateTime.Now.ToString(@"dd/MM/yyyy HH:mm:ss.ffff");
    }

    public static void LogInfo(string message) {
        logWriter ??= InitializeLog();
        string logMessage = string.Join(' ', CreateTimePrefix(), LOG_INFO_PREFIX, message);
        Console.WriteLine(logMessage);
        logWriter.WriteLine(logMessage);
    }

    public static void LogWarning(string message) {
        logWriter ??= InitializeLog();
        string logMessage = string.Join(' ', CreateTimePrefix(), LOG_WARN_PREFIX, message);
        Console.WriteLine(logMessage);
        logWriter.WriteLine(logMessage);
    }
        

    public static void LogError(string message) {
        logWriter ??= InitializeLog();
        string logMessage = string.Join(' ', CreateTimePrefix(), LOG_ERR_PREFIX, message);
        Console.WriteLine(logMessage);
        logWriter.WriteLine(logMessage);
    }
        
    private static bool IsValidPath(string filePath) {
        foreach (char invalid in Path.GetInvalidPathChars()) {
            if (filePath.Contains(invalid)) {
                return false;
            }
        }
        return true;
    }

    public static bool IsFile(string filePath) {
        if (!IsValidPath(filePath)) return false;
        return !Path.GetExtension(filePath).IsBlank();
    }

    public static bool IsAudioFile(string filePath) {
        if (!IsValidPath(filePath)) return false;
        return Path.GetExtension(filePath) switch {
            ".mp3" or ".wav" or ".m4a" => true,
            _ => false
        };
    }

    public static bool IsImageFile(string filePath) {
        if (!IsValidPath(filePath)) return false;
        return Path.GetExtension(filePath) switch {
            ".jpg" or ".jpeg" or ".png" => true,
            _ => false
        };
    }

    public static bool IsAnimatedFile(string filePath) {
        if (!IsValidPath(filePath)) return false;
        return Path.GetExtension(filePath) switch {
            ".mov" or ".mpg" or ".mpeg" or ".avi" or ".webm" or ".webp" or ".mp4" or ".gif" => true,
            _ => false
        };
    }

    public static bool IsExecutableFile(string filePath) {
        if (!IsValidPath(filePath)) return false;
        return Path.GetExtension(filePath) switch {
            ".exe" or ".bat" or ".jar" => true,
            _ => false
        };
    }
        
    public static bool IsWebPage(string input)
    {
        if (Uri.TryCreate(input, UriKind.Absolute, out Uri? uriResult))
        {
            return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        }
        return false;
    }
        
    public static string[] SeparateArrayString(string separate) {
        List<string> output = new List<string>();
        Match match = splitterRegex.Match(separate);
        while (match.Success) {
            output.Add(match.Groups[1].Value);
            match = match.NextMatch();
        }
        return output.ToArray();
    }

    public static Form? GetForm(Type type) {
        foreach (Form form in Application.OpenForms) {
            if (form.GetType() == type) {
                return form;
            }
        }
        return null;
    }

    public static string GetLastItemFromUrl(string content)
    {
        return content.Substring(content.LastIndexOf('/') + 1);
    }
}