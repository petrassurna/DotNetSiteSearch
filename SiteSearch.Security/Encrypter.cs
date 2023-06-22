using System.Security.Cryptography;


namespace SiteSearch.Security
{
  public class Encrypter
  {
    public static string EncryptString(string plainText, byte[] key, byte[] iv)
    {
      using (Aes aesAlg = Aes.Create())
      {
        aesAlg.Key = key;
        aesAlg.IV = iv;

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using (var msEncrypt = new System.IO.MemoryStream())
        {
          using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
          {
            using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
            {
              swEncrypt.Write(plainText);
            }

            byte[] encryptedBytes = msEncrypt.ToArray();
            string encryptedText = Convert.ToBase64String(encryptedBytes);
            return encryptedText;
          }
        }
      }
    }

    public static string DecryptString(string encryptedText, byte[] key, byte[] iv)
    {
      byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

      using (Aes aesAlg = Aes.Create())
      {
        aesAlg.Key = key;
        aesAlg.IV = iv;

        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using (var msDecrypt = new System.IO.MemoryStream(encryptedBytes))
        {
          using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
          {
            using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
            {
              string decryptedText = srDecrypt.ReadToEnd();
              return decryptedText;
            }
          }
        }
      }
    }
  }
}