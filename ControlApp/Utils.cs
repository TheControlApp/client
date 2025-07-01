using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using FluentFTP.Helpers;

namespace ControlApp;

internal static class Utils
{

    private static readonly string LOG_INFO_PREFIX = "[INFO]";
    private static readonly string LOG_WARN_PREFIX = "[WARNING]";
    private static readonly string LOG_ERR_PREFIX = "[ERROR]";
    private static readonly Regex splitterRegex = new Regex(@",?\[([a-zA-Z0-9:/\\\.\-|: =]*)]", RegexOptions.None, TimeSpan.FromSeconds(1));
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
        string logMessage = String.Join(' ', CreateTimePrefix(), LOG_INFO_PREFIX, message);
        Console.WriteLine(logMessage);
        logWriter.WriteLine(logMessage);
    }

    public static void LogWarning(string message) {
        logWriter ??= InitializeLog();
        string logMessage = String.Join(' ', CreateTimePrefix(), LOG_WARN_PREFIX, message);
        Console.WriteLine(logMessage);
        logWriter.WriteLine(logMessage);
    }
        

    public static void LogError(string message) {
        logWriter ??= InitializeLog();
        string logMessage = String.Join(' ', CreateTimePrefix(), LOG_ERR_PREFIX, message);
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
            ".mp3" or ".wav" => true,
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
            ".exe" or ".bat" => true,
            _ => false
        };
    }
        
    public static bool IsWebPage(string input)
    {
        if (Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult))
        {
            return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
        }
        return false;
    }
        
    public static string[] SeparateString(string separate) {
        List<string> output = new List<string>();
        Match match = splitterRegex.Match(separate);
        while (match.Success) {
            output.Add(match.Groups[1].Value);
            match = match.NextMatch();
        }
        return output.ToArray();
    }
    public static string Encrypt(string line)
    {
        string returnString = "";
        string publickey = "santhosh";
        string secretkey = "engineer";
        try 
        {
            byte[] secretkeyByte = Encoding.UTF8.GetBytes(secretkey);
            byte[] publickeybyte = Encoding.UTF8.GetBytes(publickey);
            byte[] inputbyteArray = Encoding.UTF8.GetBytes(line);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider()) 
            {
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(publickeybyte, secretkeyByte), CryptoStreamMode.Write);
                cryptoStream.Write(inputbyteArray, 0, inputbyteArray.Length);
                cryptoStream.FlushFinalBlock();
                returnString = Convert.ToBase64String(memoryStream.ToArray());
            }
        } catch (Exception ex) {
            LogWarning("Error while encrypting \"" + line + "\": " + ex.Message);
        }
        returnString = returnString.Replace("\\", "xxx");
        returnString = returnString.Replace("&", "yyy");
        returnString = returnString.Replace("/", "zzz");
        returnString = returnString.Replace("]", "aaa");
        returnString = returnString.Replace("G0", "ppp");
        returnString = returnString.Replace("0x", "lll");
        return returnString;
    }
        
    public static string Decrypt(string line)
    {
        string returnString = "";
        line = line.Replace("xxx", "\\");
        line = line.Replace("yyy", "&");
        line = line.Replace("zzz", "/");
        line = line.Replace("aaa", "]");
        line = line.Replace("ppp", "G0");
        line = line.Replace("lll", "0x");
        line = line.Replace(" ", "+");
        string publicKey = "santhosh";
        string privateKey = "engineer";
        try 
        {
            byte[] privatekeyByte = Encoding.UTF8.GetBytes(privateKey);
            byte[] publickeybyte = Encoding.UTF8.GetBytes(publicKey);
            byte[] inputbyteArray = Convert.FromBase64String(line);
            using DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(publickeybyte, privatekeyByte), CryptoStreamMode.Write);
            cryptoStream.Write(inputbyteArray, 0, inputbyteArray.Length);
            cryptoStream.FlushFinalBlock();
            returnString = Encoding.UTF8.GetString(memoryStream.ToArray());
        } 
        catch (Exception ex) 
        {
            LogWarning($"Error while decrypting \"{line}\": {ex.Message}");
        }
        return returnString;
    }

    public static bool CheckEnabled(string option) {
        string? configOutput = ConfigurationManager.AppSettings[option];
        if (configOutput == null) {
            LogError($"Option {option} is not defined in config, returning false");
            return false;
        }
        try {
            return Convert.ToBoolean(configOutput);
        } catch (FormatException) { // ignoring value instead of just catching any exception because this is the only exception we expect, anything else is an actual exception and should actually crash the program
            LogError($"Cannot convert value {configOutput} of {option} to boolean, returning false");
            return false;
        }
    }

    public static Form? GetForm(Type type) {
        foreach (Form form in Application.OpenForms) {
            if (form.GetType() == type) {
                return form;
            }
        }
        return null;
    }
}