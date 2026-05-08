using System.Security.Cryptography;
using System.Text;
using CSaP.CourseProject.DataModel;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class SendCommand : ICommand
    {
        private readonly ApplicationContext _context;
        private readonly ParsedCommand _parsed;


        public SendCommand(ApplicationContext context, ParsedCommand parsed)
        {
            _context = context;
            _parsed = parsed;
        }


        public void Execute()
        {
            Console.Write("Message ID: ");
            string? messageID = Console.ReadLine();

            Console.Write("Sender ID: ");
            string? senderID = Console.ReadLine();

            Console.Write("Message: ");
            string? text = Console.ReadLine();

            ValidateInput(messageID, senderID, text);

            IUser sender = _context.Users.Get(senderID!);

            byte[] data = Encoding.UTF8.GetBytes(text!);
            
            byte[] signature = sender.Sign(data);

            SignedMessage message = new SignedMessage(messageID!, sender.UserID, text!, signature, DateTime.UtcNow);

            _context.Messages.Add(message);

            Console.WriteLine("Message signed and stored\n");
        }

        private void ValidateInput(string? messageID, string? senderID, string? text)
        {
            if (string.IsNullOrWhiteSpace(messageID)) throw new FormatException("Invalid message ID");

            if (_context.Messages.Exists(messageID)) throw new InvalidOperationException("Message already exists");

            if (string.IsNullOrWhiteSpace(senderID)) throw new FormatException("Invalid sender");

            if (!_context.Users.Exists(senderID)) throw new InvalidOperationException("Sender not found");

            if (string.IsNullOrWhiteSpace(text)) throw new FormatException("Message is empty");
        }
    }
}