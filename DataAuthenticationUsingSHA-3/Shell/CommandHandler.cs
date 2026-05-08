using CSaP.CourseProject.Shell.Commands;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell
{
    public class CommandHandler
    {
        private readonly ApplicationContext _context;
        private readonly Dictionary<string, CommandDescriptor> _commands;


        public CommandHandler(ApplicationContext context)
        {
            _context = context;
            
            _commands = new()
            {
                ["deleteMessage"] = new CommandDescriptor(
                    "", 
                    _ => new DeleteMessageCommand(), 
                    new List<CommandHelpEntry>()
                    {
                        new("", "")
                    }),
                
                ["help"] = new CommandDescriptor(
                    "help", 
                    _ => new HelpCommand(this), 
                    new List<CommandHelpEntry>()
                    {
                        new("help", "Show available commands")
                    }),
                
                ["exit"] = new CommandDescriptor(
                    "", 
                    _ => new ExiteCommand(), 
                    new List<CommandHelpEntry>()
                    {
                        new("", "")
                    }),
                
                ["list"] = new CommandDescriptor(
                    "list", 
                    parsed => new ListCommand(_context, parsed),
                    new List<CommandHelpEntry>()
                    {
                        new("list users", "Show all users"),
                        new("list messages", "Show all messages")
                    }),
                
                ["send"] = new CommandDescriptor(
                    "", 
                    _ => new SendCommand(_context), 
                    new List<CommandHelpEntry>()
                    {
                            new("", "")
                    }),
                
                ["tamper"] = new CommandDescriptor(
                    "", 
                    _ => new TamperCommand(), 
                    new List<CommandHelpEntry>()
                    {
                            new("", "")
                    }),
                
                ["verify"] = new CommandDescriptor(
                    "", 
                    _ => new VerifyCommand(), 
                    new List<CommandHelpEntry>()
                    {
                            new("", "")
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