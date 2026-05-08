using System.Data;
using CSaP.CourseProject.Shell.Parsing;

namespace CSaP.CourseProject.Shell
{
    public class Shell
    {
        private Parser _parser;
        private CommandHandler _commandHandler;

        private bool _isRunning;


        private Shell(Parser parser, ApplicationContext context)
        {
            _parser = parser;
            _commandHandler = new CommandHandler(context, this);
        }


        public static Shell Create(ApplicationContext context)
        {
            Parser parser = new();

            return new Shell(parser, context);
        }

        public void Start()
        {
            _isRunning = true;

            while (_isRunning)
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

        public void Stop()
        {
            _isRunning = false;
        }
    }
}