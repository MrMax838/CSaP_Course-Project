using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public class CommandDescription
    {
        private string _name;
        private IReadOnlyList<CommandHelpEntry> _helpEntries;


        public string Name { get { return _name; } }
        public IReadOnlyList<CommandHelpEntry> HelpEntries { get { return _helpEntries; } }


        public CommandDescription(string name, IReadOnlyList<CommandHelpEntry> helpEntries)
        {
            _name = name;
            _helpEntries = helpEntries;
        }
    }
}