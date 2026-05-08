using CSaP.CourseProject.DataModel;
using CSaP.CourseProject.Service;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class TamperCommand : ICommand
    {
        private readonly ApplicationContext _context;
        private readonly ParsedCommand _parsed;
        private readonly MessageTamper _tamperService;


        public TamperCommand(ApplicationContext context, ParsedCommand parsed, MessageTamper tamperService)
        {
            _context = context;
            _parsed = parsed;
            _tamperService = tamperService;
        }


        public void Execute()
        {
            if (_parsed.Arguments.Count == 0) throw new FormatException("Original message ID required");

            string originalMessageID = _parsed.Arguments[0];

            Console.Write("New message ID: ");
            string? newMessageID = Console.ReadLine();

            string realSender = _context.Users.Get(originalMessageID).UserID;

            Console.Write("Target: ");
            string? target = Console.ReadLine();

            Console.Write("Tampered message: ");
            string? tamperedMessage = Console.ReadLine();

            ValidateInput(newMessageID, target, tamperedMessage);

            SignedMessage tampered = _tamperService.Tamper(originalMessageID, newMessageID!, target!, tamperedMessage!);
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