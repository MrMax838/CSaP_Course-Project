using CSaP.CourseProject.DataModel;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class RegisterNewUserCommand : ICommand
    {
        private readonly ApplicationContext _context;
        private readonly ParsedCommand _parsed;
        private readonly CommandDescription _description = new CommandDescription(
            "register",
            new List<CommandHelpEntry>()
            {
                    new("register <userID>", "register new user")
            }
        );


        public CommandDescription Description => _description;


        public RegisterNewUserCommand(ApplicationContext context, ParsedCommand parsed)
        {
            _context = context;
            _parsed = parsed;
        }

        public void Execute()
        {
            if (_parsed.Arguments.Count == 0) throw new FormatException("User ID required");

            string userID = _parsed.Arguments[0];

            if (string.IsNullOrWhiteSpace(userID)) throw new FormatException("Invalid user ID");
            if (_context.Messages.Exists(userID)) throw new InvalidOperationException("User already exists");

            User user = new User(userID, RSA.RSA.Create(_context.RsaKeySiza));

            _context.Users.Add(user);

            Console.WriteLine("User successfully registered\n");
        }
    }
}