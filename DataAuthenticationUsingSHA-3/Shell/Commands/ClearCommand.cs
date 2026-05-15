namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class ClearCommand : ICommand
    {
        private readonly CommandDescription _description = new CommandDescription(
            "clear",
            new List<CommandHelpEntry>()
            {
                new("clear", "Clear console")
            }
        );


        public CommandDescription Description => _description;



        public void Execute()
        {
            Console.Clear();
        }
    }
}