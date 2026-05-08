using System.Text;

namespace CSaP.CourseProject.RSA
{
    public class Wrapper
    {
        private RSA _rsa;


        public Wrapper(RSA rsa)
        {
            _rsa = rsa;
        }


        public byte[] Encrypt(string text)
        {
            return _rsa.Encrypt(Encoding.UTF8.GetBytes(text));
        }

        public string DecryptToString(byte[] cipher)
        {
            return Encoding.UTF8.GetString(_rsa.Decrypt(cipher));
        }

        public byte[] Sign(string text)
        {
            return _rsa.Sign(Encoding.UTF8.GetBytes(text));
        }

        public bool Verify(string text, byte[] signature)
        {
            return _rsa.Verify(Encoding.UTF8.GetBytes(text), signature);
        }

        public byte[] SignFile(string path)
        {
            byte[] data = File.ReadAllBytes(path);

            return _rsa.Sign(data);
        }

        public bool VerifyFile(string path, byte[] signature)
        {
            byte[] data = File.ReadAllBytes(path);

            return _rsa.Verify(data, signature);
        }
    }
}