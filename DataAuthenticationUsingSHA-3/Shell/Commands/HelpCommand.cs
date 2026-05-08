namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class HelpCommand : ICommand
    {
        private CommandHandler _commandHandler;


        public HelpCommand(CommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }


        public void Execute()
        {
            Console.WriteLine("Available commands:\n");

            foreach (CommandDescriptor command in _commandHandler.GetCommands())
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