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
    }
}