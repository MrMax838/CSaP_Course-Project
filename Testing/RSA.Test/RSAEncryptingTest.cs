using System.Security.Cryptography;

namespace CSaP.CourseProject.RSA;

public class RSAEncryptingTest
{
    [Fact]
    public void EncryptDecrypt_ShouldReturnOriginalMessage()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "Hello RSA";

        byte[] cipher = wrapper.Encrypt(message);
        string result = wrapper.DecryptToString(cipher);

        Assert.Equal(message, result);
    }

    [Fact]
    public void EncryptDecrypt_ShouldSupportUnicode()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "Привіт 🌍 RSA шифрування";

        byte[] cipher = wrapper.Encrypt(message);
        string result = wrapper.DecryptToString(cipher);

        Assert.Equal(message, result);
    }

    [Fact]
    public void EncryptDecrypt_LongMessage_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = new string('A', 2000);

        byte[] cipher = wrapper.Encrypt(message);
        string result = wrapper.DecryptToString(cipher);

        Assert.Equal(message, result);
    }

    [Fact]
    public void EncryptDecrypt_RandomData_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        byte[] random = new byte[256];
        RandomNumberGenerator.Fill(random);

        string message = Convert.ToBase64String(random);

        byte[] cipher = wrapper.Encrypt(message);
        string result = wrapper.DecryptToString(cipher);

        Assert.Equal(message, result);
    }

    [Fact]
    public void EncryptDecrypt_SingleCharacter_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "A";

        byte[] cipher = wrapper.Encrypt(message);
        string result = wrapper.DecryptToString(cipher);

        Assert.Equal(message, result);
    }

    [Fact]
    public void EncryptDecrypt_EmptyString_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "";

        byte[] cipher = wrapper.Encrypt(message);
        string result = wrapper.DecryptToString(cipher);

        Assert.Equal(message, result);
    }

    [Fact]
    public void Decrypt_WithDifferentKey_ShouldFail()
    {
        var rsa1 = RSA.Create();
        var wrapper1 = new Wrapper(rsa1);

        var rsa2 = RSA.Create();
        var wrapper2 = new Wrapper(rsa2);

        string message = "Secret";

        byte[] cipher = wrapper1.Encrypt(message);

        Assert.ThrowsAny<Exception>(() => wrapper2.DecryptToString(cipher));
    }

    [Fact]
    public void Decrypt_CorruptedCipher_ShouldFail()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "Hello";

        byte[] cipher = wrapper.Encrypt(message);

        cipher[10] ^= 0xFF; // псуємо байт

        Assert.ThrowsAny<Exception>(() => rsa.Decrypt(cipher));
    }

    [Fact]
    public void Encrypt_OutputLength_ShouldBeMultipleOfKeySize()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "Test message";

        byte[] cipher = wrapper.Encrypt(message);

        int keySize = RSAKeyGenerator.GetModuleByteSize(rsa.PublicKey.Module);

        Assert.True(cipher.Length % keySize == 0);
    }

    [Fact]
    public void Encrypt_SameMessage_ShouldProduceDifferentCipher()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string message = "Hello";

        byte[] cipher1 = wrapper.Encrypt(message);
        byte[] cipher2 = wrapper.Encrypt(message);

        Assert.NotEqual(cipher1, cipher2);
    }

    [Fact]
    public void EncryptDecrypt_ManyRandomMessages_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        for (int i = 0; i < 50; i++)
        {
            byte[] random = new byte[128];
            RandomNumberGenerator.Fill(random);

            string message = Convert.ToBase64String(random);

            byte[] cipher = wrapper.Encrypt(message);
            string result = wrapper.DecryptToString(cipher);

            Assert.Equal(message, result);
        }
    }
}