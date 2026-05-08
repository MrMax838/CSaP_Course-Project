namespace CSaP.CourseProject.Shell.Parsing
{
    public sealed class Parser
    {
        public ParsedCommand Parse(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0) throw new FormatException("Command is empty");

            string name = parts[0];
            List<string> arguments = new();
            List<string> flags = new();

            foreach (string part in parts.Skip(1))
            {
                if (part.StartsWith('-'))
                {
                    flags.Add(part);
                }
                else
                {
                    arguments.Add(part);
                }
            }

            return new ParsedCommand(name, arguments, flags);
        }
    }
}