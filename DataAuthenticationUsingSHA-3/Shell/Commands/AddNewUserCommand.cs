using CSaP.CourseProject.DataModel;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class AddNewUserCommand : ICommand
    {
        private ApplicationContext _context;
        private ParsedCommand _parsed;


        public AddNewUserCommand(ApplicationContext context, ParsedCommand parsed)
        {
            _context = context;
            _parsed = parsed;
        }

        public void Execute()
        {
            if (_parsed.Arguments.Count == 0) throw new FormatException("Message ID required");

            string userID = _parsed.Arguments[0];

            if (string.IsNullOrWhiteSpace(userID)) throw new FormatException("Invalid user ID");
            if (_context.Messages.Exists(userID)) throw new InvalidOperationException("User already exists");

            User user = new User(userID, RSA.RSA.Create(_context.RsaKeySiza));

            _context.Users.Add(user);

            Console.WriteLine("User successfully added");
        }
    }
}