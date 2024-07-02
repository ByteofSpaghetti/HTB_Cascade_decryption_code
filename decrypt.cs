using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class Crypto
{
    public static string EncryptString(string Plaintext, string Key)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(Plaintext);
        Aes aes = Aes.Create();
        aes.BlockSize = 128;
        aes.KeySize = 128;
        aes.IV = Encoding.UTF8.GetBytes("1tdyjCbY1Ix49842");
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.Mode = CipherMode.CBC;
        string result;
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
            }
            result = Convert.ToBase64String(memoryStream.ToArray());
        }
        return result;
    }

    public static string DecryptString(string EncryptedString, string Key)
    {
        byte[] array = Convert.FromBase64String(EncryptedString);
        Aes aes = Aes.Create();
        aes.KeySize = 128;
        aes.BlockSize = 128;
        aes.IV = Encoding.UTF8.GetBytes("1tdyjCbY1Ix49842");
        aes.Mode = CipherMode.CBC;
        aes.Key = Encoding.UTF8.GetBytes(Key);
        string @string;
        using (MemoryStream memoryStream = new MemoryStream(array))
        {
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                byte[] array2 = new byte[array.Length];
                int readBytes = cryptoStream.Read(array2, 0, array2.Length);
                @string = Encoding.UTF8.GetString(array2, 0, readBytes);
            }
        }
        return @string;
    }
}

public class Program
{
    public static void Main()
    {
        string encryptedPassword = "BQO5l5Kj9MdErXx6Q6AGOw== "; // Replace with your actual encrypted password
        string key = "c4scadek3y654321"; // The hardcoded key

        try
        {
            string decryptedPassword = Crypto.DecryptString(encryptedPassword, key);
            Console.WriteLine("Decrypted Password: " + decryptedPassword);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error decrypting password: " + ex.Message);
        }
    }
}
