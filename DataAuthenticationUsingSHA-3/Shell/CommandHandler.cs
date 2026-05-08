using CSaP.CourseProject.Service;
using CSaP.CourseProject.Shell.Commands;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell
{
    public class CommandHandler
    {
        private readonly ApplicationContext _context;
        private readonly Dictionary<string, CommandDescriptor> _commands;
        private readonly Shell _shell;


        public CommandHandler(ApplicationContext context, Shell shell)
        {
            _context = context;
            _shell = shell;
            
            _commands = new()
            {
                ["help"] = new CommandDescriptor(
                    "help", 
                    _ => new HelpCommand(this), 
                    new List<CommandHelpEntry>()
                    {
                        new("help", "Show available commands")
                    }),
                
                ["clear"] = new CommandDescriptor(
                    "clear", 
                     _ => new ClearCommand(), 
                    new List<CommandHelpEntry>()
                    {
                        new("clear", "Clear console")
                    }),
                
                ["exit"] = new CommandDescriptor(
                    "exit", 
                    _ => new ExiteCommand(_shell), 
                    new List<CommandHelpEntry>()
                    {
                        new("exit", "Exit from the shell")
                    }),
                
                ["list"] = new CommandDescriptor(
                    "list", 
                    parsed => new ListCommand(_context, parsed),
                    new List<CommandHelpEntry>()
                    {
                        new("list users", "Show all users"),
                        new("list messages", "Show all messages"),
                        new("list messages --extended", "Show all messages with detailed information")
                    }),
                
                ["send"] = new CommandDescriptor(
                    "send", 
                    parsed => new SendCommand(_context, parsed), 
                    new List<CommandHelpEntry>()
                    {
                            new("send", "Create and sign message")
                    }),
                
                ["verify"] = new CommandDescriptor(
                    "verify",
                    parsed => new VerifyCommand(parsed, new MessageVerifier(_context)), 
                    new List<CommandHelpEntry>()
                    {
                            new("verify <messageID>", "Verify message signature"),
                            new("verify <messageID> --extended", "Show detailed verification process")
                    }),
                
                ["tamper"] = new CommandDescriptor(
                    "tamper", 
                    parsed => new TamperCommand(_context, parsed, new MessageTamper(_context)), 
                    new List<CommandHelpEntry>()
                    {
                            new("tamper <messageID>", "Simulate message tampering attack")
                    }),

                ["add"] = new CommandDescriptor(
                    "add", 
                    parsed => new AddNewUserCommand(_context, parsed), 
                    new List<CommandHelpEntry>()
                    {
                            new("add <userID>", "Add new user")
                    }),
                
                ["deleteMessage"] = new CommandDescriptor(
                    "delete", 
                    parsed => new DeleteMessageCommand(_context, parsed), 
                    new List<CommandHelpEntry>()
                    {
                        new("delete <messageID>", "Delete message")
                    })
            };
        }


        public void Handle(ParsedCommand parsed)
        {
            if (!_commands.TryGetValue(parsed.Name.ToLower(), out var commandDescription)) throw new InvalidOperationException("Unknown command");

            ICommand command = commandDescription.Factory(parsed);

            command.Execute();
        }

        public IReadOnlyCollection<CommandDescriptor> GetCommands()
        {
            return _commands.Values;
        }
    }
}