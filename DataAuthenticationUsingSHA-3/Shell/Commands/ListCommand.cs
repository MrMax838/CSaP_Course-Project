using System.Diagnostics.Tracing;
using CSaP.CourseProject.DataModel;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class ListCommand : ICommand
    {
        private readonly ApplicationContext _context;
        private readonly ParsedCommand _parsed;


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
                    throw new FormatException(
                        "Unknown list target");
            }
        }

        private void ListUsers()
        {
            Console.WriteLine("Users:\n");

            foreach (User user in _context.Users.GetAllUsers())
            {
                Console.WriteLine($"- {user.UserID}");
            }
        }

        private void ListMessages()
        {
            bool isExtended = _parsed.Flags.Contains("--extended");
            IEnumerable<SignedMessage> messages = _context.Messages.GetAllMessage();

            if (!messages.Any())
            {
                Console.WriteLine("No messages found");

                return;
            }

            Console.WriteLine("Messages:\n");

            foreach (SignedMessage message in _context.Messages.GetAllMessage())
            {
                if (isExtended)
                {
                    Console.WriteLine(
                        $"""
                        ID: {message.MessageID}
                        Sender: {message.SenderID}
                        Timestamp: {message.Timestamp}

                        """);
                }
                else
                {
                    Console.WriteLine($"- {message.MessageID}");
                }
            }
        }
    }
}