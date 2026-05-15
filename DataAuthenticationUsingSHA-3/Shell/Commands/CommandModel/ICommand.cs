using CSaP.CourseProject.Shell.Commands;

namespace CSaP.CourseProject
{
    public interface ICommand
    {
        public CommandDescription Description { get; }

        
        void Execute();
    }
}