using Shouldly;
using System.Text;

namespace SiteSearch.Security.Tests
{
  public class EncrypterTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EncryptionTest()
    {
      string plainText = "Hello, World!";
      byte[] key = Encoding.UTF8.GetBytes("aDfvBl567dfGBCr4"); // 128-bit key
      byte[] iv = Encoding.UTF8.GetBytes("aDfvBl567dfGBCr4"); // 128-bit IV

      string encryptedText = Encrypter.EncryptString(plainText, key, iv);
      encryptedText.ShouldBe("N68qnZS8cHZa8f//BxC5Sw==");

      string decryptedText = Encrypter.DecryptString(encryptedText, key, iv);
      decryptedText.ShouldBe(plainText);
    }
  }
}