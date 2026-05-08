namespace CSaP.CourseProject.Shell.Parsing
{
    public class ParsedCommand
    {
        public string Name { get; }
        public IReadOnlyList<string> Arguments { get; }
        public IReadOnlyList<string> Flags { get; }

        public ParsedCommand(string name, IReadOnlyList<string> arguments, IReadOnlyList<string> flags)
        {
            Name = name;
            Arguments = arguments;
            Flags = flags;
        }
    }
}