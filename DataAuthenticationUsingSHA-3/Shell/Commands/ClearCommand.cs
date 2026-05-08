namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class ClearCommand : ICommand
    {
        public void Execute()
        {
            Console.Clear();
        }
    }
}