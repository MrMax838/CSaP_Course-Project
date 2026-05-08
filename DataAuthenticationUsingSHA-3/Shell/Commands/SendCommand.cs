using System.Security.Cryptography;
using System.Text;
using CSaP.CourseProject.DataModel;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class SendCommand : ICommand
    {
        private readonly ApplicationContext _context;


        public SendCommand(ApplicationContext context)
        {
            _context = context;
        }


        public void Execute()
        {
            Console.Write("Message ID: ");
            string? messageID = Console.ReadLine();

            messageID = ValidateMessageId(messageID);

            Console.Write("Sender ID: ");
            string? senderID = Console.ReadLine();

            senderID = ValidateSenderID(senderID);

            Console.Write("Message: ");
            string messageText = Console.ReadLine() ?? " ";

            IUser sender = _context.Users.GetUser(senderID);
            byte[] data = Encoding.UTF8.GetBytes(messageText);
            byte[] signature = sender.Sign(data);

            SignedMessage message = new SignedMessage(messageID, sender.UserID, messageText, signature, DateTime.UtcNow);

            _context.Messages.Add(message);

            Console.WriteLine("Message signed and stored");
        }

        private string ValidateMessageId(string? messageID)
        {
            if (string.IsNullOrWhiteSpace(messageID)) throw new ArgumentException("MessageID cannot be null or empty.");

            if (!_context.Messages.Exists(messageID)) return messageID;
            else
            {
                string newID;

                do
                {
                    byte[] random = RandomNumberGenerator.GetBytes(32);

                    byte[] hash = SHA3_256.HashData(random);

                    newID = GetShortSHA3Hash(hash);
                }
                while (_context.Messages.Exists(newID));
                
                Console.WriteLine($"\n Provided messageID is already exists. Your messageID has been replaced with \"{newID}\"");

                return newID;
            }
        }
        
        private string ValidateSenderID(string? senderID)
        {
            if (string.IsNullOrWhiteSpace(senderID)) throw new ArgumentException("SenderID cannot be null or empty.");

            if (!_context.Users.Exists(senderID)) throw new ArgumentException($"Sender \"{senderID}\" doen't exist.");

            return senderID;
        }

        private string GetShortSHA3Hash(byte[] hash, int length = 8)
        {
            string hex = Convert.ToHexString(hash).ToLowerInvariant();

            return hex.Substring(0, length);
        }
    }
}