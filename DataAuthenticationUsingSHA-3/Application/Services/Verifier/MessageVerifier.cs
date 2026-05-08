using System.Security.Cryptography;
using System.Text;
using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject.Service
{
    public sealed class MessageVerifier
    {
        private readonly ApplicationContext _context;


        public MessageVerifier(ApplicationContext context)
        {
            _context = context;
        }

        public VerificationResult Verify(string messageID)
        {
            SignedMessage message = _context.Messages.Get(messageID);

            IUser user = _context.Users.Get(message.SenderID);

            byte[] data = Encoding.UTF8.GetBytes(message.Message);

            bool isValid = user.Verify(data, message.Signature);

            byte[] hash = SHA3_256.HashData(data);

            return new VerificationResult(isValid, message.MessageID, message.SenderID, hash, message.Signature, message.Timestamp);
        }
    }
}