namespace CSaP.CourseProject.Shell.Commands
{
    public sealed class CommandHelpEntry
    {
        private string _usage;
        private string _description;


        public string Usage { get { return _usage; } }
        public string Description { get {return _description; } }


        public CommandHelpEntry(string usage, string description)
        {
            _usage = usage;
            _description = description;
        }
    }
}