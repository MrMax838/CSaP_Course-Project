using System.Diagnostics.Tracing;
using CSaP.CourseProject.DataModel;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class ListCommand : ICommand
    {
        private readonly ApplicationContext _context;
        private readonly ParsedCommand _parsed;
        private readonly CommandDescription _description = new CommandDescription(
            "list",
            new List<CommandHelpEntry>()
            {
                new("list users", "Show all users"),
                new("list messages", "Show all messages"),
                new("list messages --extended", "Show all messages with detailed information")
            }
        );

        
        public CommandDescription Description => _description;


        public ListCommand(ApplicationContext context, ParsedCommand parsed)
        {
            _context = context;
            _parsed = parsed;
        }


        public void Execute()
        {
            if (_parsed.Arguments.Count == 0)
            {
                throw new FormatException("Specify what to list");
            }

            string target = _parsed.Arguments[0].ToLower();

            switch (target)
            {
                case "users":
                    ListUsers();
                    break;

                case "messages":
                    ListMessages();
                    break;

                default:
                    throw new FormatException("Unknown list target\n");
            }
        }

        private void ListUsers()
        {
            Console.WriteLine("Users:");

            foreach (User user in _context.Users.GetAll())
            {
                Console.WriteLine($"- {user.UserID}");
            }
            Console.WriteLine();
        }

        private void ListMessages()
        {
            bool isExtended = _parsed.Flags.Contains("--extended");
            IEnumerable<SignedMessage> messages = _context.Messages.GetAll();

            if (!messages.Any())
            {
                Console.WriteLine("No messages found\n");

                return;
            }

            Console.WriteLine("Messages:");

            foreach (SignedMessage message in _context.Messages.GetAll())
            {
                if (isExtended)
                {
                    Console.WriteLine(
                        $"""
                        ID: {message.MessageID}
                        Sender: {message.SenderID}
                        Message: {message.Message}
                        Signature: {Convert.ToHexString(message.Signature)}
                        Timestamp: {message.Timestamp}

                        """);
                }
                else
                {
                    Console.WriteLine($"- {message.MessageID}");
                }
            }
            
            Console.WriteLine();
        }
    }
}