using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class DeleteMessageCommand : ICommand
    {
        private readonly ApplicationContext _context;
        private readonly ParsedCommand _parsed;


        public DeleteMessageCommand(ApplicationContext context, ParsedCommand parsed)
        {
            _context = context;
            _parsed = parsed;
        }


        public void Execute()
        {
            if (_parsed.Arguments.Count == 0) throw new FormatException("Message ID required");

            string messageID = _parsed.Arguments[0];

            if (!_context.Messages.Exists(messageID)) throw new InvalidOperationException("Message not found");

            _context.Messages.Delete(messageID);

            Console.WriteLine("Message deleted successfully");
        }
    }
}