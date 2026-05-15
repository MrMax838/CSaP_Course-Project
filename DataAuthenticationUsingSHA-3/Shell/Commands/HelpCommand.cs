using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class HelpCommand : ICommand
    {
        private readonly CommandHandler _commandHandler;
        private readonly ParsedCommand _parsed;
        private readonly CommandDescription _description = new CommandDescription(
            "help",
            new List<CommandHelpEntry>()
            {
                new("help", "Show available commands")
            }
        );


        public CommandDescription Description => _description;


        public HelpCommand(CommandHandler commandHandler, ParsedCommand parsed)
        {
            _commandHandler = commandHandler;
            _parsed = parsed;
        }


        public void Execute()
        {
            foreach (CommandDescription command in _commandHandler.GetCommandDescriptions(_parsed))
            {
                foreach (CommandHelpEntry helpEntry in command.HelpEntries)
                {
                    Console.WriteLine(helpEntry.Usage);

                    Console.WriteLine($"    {helpEntry.Description}\n");
                }
            }
        }
    }
}