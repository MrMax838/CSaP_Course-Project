using System.Text;

namespace CSaP.CourseProject.RSA;

public class RSASignatureTet
{
    [Fact]
    public void SignVerify_ShouldWork()
    {
        var rsa = RSA.Create();

        byte[] data = Encoding.UTF8.GetBytes("Hello RSA");

        byte[] signature = rsa.Sign(data);

        bool result = rsa.Verify(data, signature);

        Assert.True(result);
    }

    [Fact]
    public void Verify_ShouldFail_WhenDataChanged()
    {
        var rsa = RSA.Create();

        byte[] data = Encoding.UTF8.GetBytes("Hello");
        byte[] signature = rsa.Sign(data);

        byte[] modified = Encoding.UTF8.GetBytes("Hello!");

        Assert.False(rsa.Verify(modified, signature));
    }

    [Fact]
    public void Verify_ShouldFail_WhenSignatureCorrupted()
    {
        var rsa = RSA.Create();

        byte[] data = Encoding.UTF8.GetBytes("Hello");
        byte[] signature = rsa.Sign(data);

        signature[10] ^= 0xFF;

        Assert.False(rsa.Verify(data, signature));
    }

    [Fact]
    public void Verify_ShouldFail_WithDifferentKey()
    {
        var rsa1 = RSA.Create();
        var rsa2 = RSA.Create();

        byte[] data = Encoding.UTF8.GetBytes("Hello");

        byte[] signature = rsa1.Sign(data);

        Assert.False(rsa2.Verify(data, signature));
    }

    [Fact]
    public void SignVerifyFile_ShouldReturnTrue()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string path = Path.GetTempFileName();

        try
        {
            File.WriteAllText(path, "Hello RSA File");

            byte[] signature = wrapper.SignFile(path);

            bool result = wrapper.VerifyFile(path, signature);

            Assert.True(result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void VerifyFile_ModifiedFile_ShouldReturnFalse()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string path = Path.GetTempFileName();

        try
        {
            File.WriteAllText(path, "Original content");

            byte[] signature = wrapper.SignFile(path);

            File.WriteAllText(path, "Modified content");

            bool result = wrapper.VerifyFile(path, signature);

            Assert.False(result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void VerifyFile_WithDifferentKey_ShouldReturnFalse()
    {
        var rsaA = RSA.Create();
        var rsaB = RSA.Create();

        var wrapperA = new Wrapper(rsaA);
        var wrapperB = new Wrapper(rsaB);

        string path = Path.GetTempFileName();

        try
        {
            File.WriteAllText(path, "Hello");

            byte[] signature = wrapperA.SignFile(path);

            bool result = wrapperB.VerifyFile(path, signature);

            Assert.False(result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void SignFile_EmptyFile_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string path = Path.GetTempFileName();

        try
        {
            File.WriteAllBytes(path, []);

            byte[] signature = wrapper.SignFile(path);

            bool result = wrapper.VerifyFile(path, signature);

            Assert.True(result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void SignFile_BinaryFile_ShouldWork()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string path = Path.GetTempFileName();

        try
        {
            byte[] binaryData = new byte[1024];

            Random.Shared.NextBytes(binaryData);

            File.WriteAllBytes(path, binaryData);

            byte[] signature = wrapper.SignFile(path);

            bool result = wrapper.VerifyFile(path, signature);

            Assert.True(result);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void VerifyFile_CorruptedSignature_ShouldReturnFalse()
    {
        var rsa = RSA.Create();
        var wrapper = new Wrapper(rsa);

        string path = Path.GetTempFileName();

        try
        {
            File.WriteAllText(path, "Hello");

            byte[] signature = wrapper.SignFile(path);

            signature[10] ^= 0xFF;

            bool result = wrapper.VerifyFile(path, signature);

            Assert.False(result);
        }
        finally
        {
            File.Delete(path);
        }
    }
}