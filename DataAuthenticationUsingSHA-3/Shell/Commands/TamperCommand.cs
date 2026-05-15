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
        private readonly CommandDescription _description = new CommandDescription(
            "tamper",
            new List<CommandHelpEntry>()
            {
                new("tamper <messageID>", "Simulate message tampering attack"),
                new("tamper <messageID> --message", "Simulate message tampering attack"),
                new("tamper <messageID> --sender", "Simulate sender tampering attack")
            }
        );


        public CommandDescription Description => _description;


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

            bool isMessage = _parsed.Flags.Contains("--message");
            bool isSender = _parsed.Flags.Contains("--sender");

            string realSender = _context.Messages.Get(originalMessageID).SenderID;
            string? target = realSender;
            string? tamperedMessage = _context.Messages.Get(originalMessageID).Message;

            Console.Write("New message ID: ");
            string? newMessageID = Console.ReadLine();

            if (isSender)
            {
                Console.Write("Target: ");
                target = Console.ReadLine();
            }

            if (_parsed.Flags.Select(f => f.StartsWith('-')).Count() == 0 || isMessage)
            {
                Console.Write("Tampered message: ");
                tamperedMessage = Console.ReadLine();     
            }

            ValidateInput(newMessageID, target, tamperedMessage);

            SignedMessage tampered = _tamperService.Tamper(originalMessageID, newMessageID!, target!, tamperedMessage!);

            _context.Messages.Add(tampered);

            Console.WriteLine("Message tampered successfully\n");
        }

        private void ValidateInput(string? messageID, string? senderID, string? text)
        {
            if (string.IsNullOrWhiteSpace(messageID)) throw new FormatException("Invalid message ID\n");

            if (_context.Messages.Exists(messageID)) throw new InvalidOperationException("Message already exists\n");

            if (string.IsNullOrWhiteSpace(senderID)) throw new FormatException("Invalid sender\n");

            if (!_context.Users.Exists(senderID)) throw new InvalidOperationException("Sender not found\n");

            if (string.IsNullOrWhiteSpace(text)) throw new FormatException("Message is empty\n");
        }
    }
}