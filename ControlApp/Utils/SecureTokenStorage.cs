using System.Security.Cryptography;
using System.Text;

namespace ControlApp.Utils;

/// <summary>
/// Provides a secure method for storing and retrieving a user token
/// using the Windows Data Protection API (DPAPI).
/// </summary>
public static class SecureTokenStorage
{
    // Define a path within the user's AppData folder to store the encrypted token.
    private static readonly string FilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "TheControlApp", // A dedicated folder for your application
        "user.token");

    // An additional "salt" to add another layer of uniqueness to the encryption.
    // This makes it harder for other applications to accidentally decrypt the data.
    private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("TheControlApp.v1.Salt");

    /// <summary>
    /// Encrypts a token using DPAPI and saves it to a file.
    /// </summary>
    /// <param name="token">The plain-text token to store.</param>
    public static void SaveToken(string token)
    {
        // Ensure the directory exists before writing the file.
        Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

        byte[] tokenBytes = Encoding.UTF8.GetBytes(token);

        // Use DPAPI to encrypt the token. The scope is set to the current user.
        byte[] encryptedBytes = ProtectedData.Protect(tokenBytes, Entropy, DataProtectionScope.CurrentUser);

        // Save the encrypted bytes to the file.
        File.WriteAllBytes(FilePath, encryptedBytes);
    }

    /// <summary>
    /// Reads the encrypted token from the file and decrypts it using DPAPI.
    /// </summary>
    /// <returns>The decrypted token, or null if it doesn't exist or fails to decrypt.</returns>
    public static string ReadToken()
    {
        if (!File.Exists(FilePath))
        {
            return null;
        }

        try
        {
            byte[] encryptedBytes = File.ReadAllBytes(FilePath);

            // Decrypt the data using the same scope and entropy.
            byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, Entropy, DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
        catch (CryptographicException)
        {
            // This can happen if the file is corrupted or moved to another user/computer.
            // Treat it as if the token doesn't exist.
            DeleteToken(); // Clean up the invalid file.
            Application.Restart();
            return null;
        }
    }

    /// <summary>
    /// Deletes the stored token file. Useful for a logout feature.
    /// </summary>
    public static void DeleteToken()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }
}