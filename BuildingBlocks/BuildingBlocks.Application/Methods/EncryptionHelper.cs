using System.Security.Cryptography;
using System.Text;

namespace BuildingBlocks.Application.Methods;

public static class EncryptionHelper
{
    private const string EncryptionKey = "KeyKeepa@@##!!$$123Keepa98765400";

    public static string Encrypt(string plainText)
    {
        byte[] encryptedBytes;
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aesAlg.IV = new byte[aesAlg.BlockSize / 8];

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using var swEncrypt = new StreamWriter(csEncrypt);
                swEncrypt.Write(plainText);
            }
            encryptedBytes = msEncrypt.ToArray();
        }

        var encryptedText = Convert.ToBase64String(encryptedBytes);

        return encryptedText;
    }

    public static string Decrypt(string encryptedText)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

        using Aes aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
        aesAlg.IV = new byte[aesAlg.BlockSize / 8];

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
        using var msDecrypt = new MemoryStream(cipherTextBytes);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);
        return srDecrypt.ReadToEnd();
    }
}