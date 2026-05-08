using CSaP.CourseProject.RSA;

namespace CSaP.CourseProject.DataModel
{
    public class User : IUser
    {
        private readonly string _userID;
        private readonly RSA.RSA _rsa;
        private readonly Wrapper _wrapper;


        public string UserID { get { return _userID; } }


        public User(string userID, RSA.RSA rsa)
        {
            if (string.IsNullOrWhiteSpace(userID)) throw new FormatException(nameof(User) + "Provided invalid name");

            _userID = userID;
            _rsa = rsa;
            _wrapper = new(_rsa);
        }


        public bool Verify(byte[] data, byte[] signature)
        {
            return _rsa.Verify(data, signature);
        }

        public byte[] Sign(byte[] data)
        {
            return _rsa.Sign(data);
        }

        public RSAData Export()
        {
            return _rsa.Export();
        }

        public SignedMessage SignMessage(string messageID, string message)
        {
            byte[] signature = _wrapper.Sign(message);

            return new SignedMessage(messageID, _userID, message, signature, DateTime.Now);
        }
    }
}