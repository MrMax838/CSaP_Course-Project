using System.Collections.ObjectModel;
using CSaP.CourseProject.Service;
using CSaP.CourseProject.Shell.Commands;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell
{
    public class CommandHandler
    {
        private readonly ApplicationContext _context;
        private readonly Shell _shell;
        private readonly Dictionary<string, Func<ParsedCommand, ICommand>> _commands;


        public CommandHandler(ApplicationContext context, Shell shell)
        {
            _context = context;
            _shell = shell;
            
            _commands = new()
            {
                ["help"] = parsed => new HelpCommand(this, parsed),
                
                ["clear"] = _ => new ClearCommand(),
                
                ["exit"] = _ => new ExiteCommand(_shell),
                
                ["list"] = parsed => new ListCommand(_context, parsed),
                
                ["send"] = parsed => new SendCommand(_context, parsed),
                
                ["verify"] = parsed => new VerifyCommand(parsed, new MessageVerifier(_context)),
                
                ["tamper"] = parsed => new TamperCommand(_context, parsed, new MessageTamper(_context)),

                ["register"] = parsed => new RegisterNewUserCommand(_context, parsed),
                
                ["delete"] = parsed => new DeleteMessageCommand(_context, parsed)
            };
        }


        public void Handle(ParsedCommand parsed)
        {
            if (!_commands.TryGetValue(parsed.Name.ToLower(), out var factory)) throw new InvalidOperationException("Unknown command\n");

            ICommand command = factory(parsed);

            command.Execute();
        }

        public IReadOnlyCollection<CommandDescription> GetCommandDescriptions(ParsedCommand parsed)
        {
            Collection<CommandDescription> result = new();

            foreach (Func<ParsedCommand, ICommand> factory in _commands.Values)
            {
                ICommand command = factory(parsed);

                result.Add(command.Description);
            }

            return result;
        }
    }
}