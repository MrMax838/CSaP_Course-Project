namespace CSaP.CourseProject;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("> ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            //CommandHandler.Execute(input);
        }
    }
}
