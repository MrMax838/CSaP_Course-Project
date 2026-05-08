using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell.Commands
{
    public class CommandDescriptor
    {
        private string _name;
        private Func<ParsedCommand, ICommand> _factory;
        private IReadOnlyList<CommandHelpEntry> _helpEntries;


        public string Name { get { return _name; } }
        public Func<ParsedCommand, ICommand> Factory { get { return _factory;} }
        public IReadOnlyList<CommandHelpEntry> HelpEntries { get { return _helpEntries; } }


        public CommandDescriptor(string name, Func<ParsedCommand, ICommand> factory, IReadOnlyList<CommandHelpEntry> helpEntries)
        {
            _name = name;
            _factory = factory;
            _helpEntries = helpEntries;
        }
    }
}