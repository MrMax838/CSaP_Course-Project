using System.Data;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell
{
    public class Shell
    {
        private Parser _parser;
        private CommandHandler _commandHandler;


        private Shell(Parser parser, CommandHandler commandHandler)
        {
            _parser = parser;
            _commandHandler = commandHandler;
        }


        public static Shell Create(ApplicationContext context)
        {
            Parser parser = new();
            CommandHandler commandHandler = new(context);

            return new Shell(parser, commandHandler);
        }

        public static Shell Create(Parser parser, CommandHandler commandHandler)
        {
            return new Shell(parser, commandHandler);
        }

        public void Start()
        {
            while (true)
            {
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;

                try
                {
                    ParsedCommand parsed = _parser.Parse(input);

                    _commandHandler.Handle(parsed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}