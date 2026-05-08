namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class ExiteCommand : ICommand
    {
        private readonly Shell _shell;


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