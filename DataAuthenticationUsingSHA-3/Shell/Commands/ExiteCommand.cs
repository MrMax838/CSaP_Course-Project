namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class ExiteCommand : ICommand
    {
        private readonly Shell _shell;
        private readonly CommandDescription _description = new CommandDescription(
            "exit",
            new List<CommandHelpEntry>()
            {
                new("exit", "Exit from the shell")
            }
        );


        public CommandDescription Description => _description;


        public ExiteCommand(Shell shell)
        {
            _shell = shell;
        }


        public void Execute()
        {
            _shell.Stop();

            Console.WriteLine("Shell terminated");
        }
    }
}