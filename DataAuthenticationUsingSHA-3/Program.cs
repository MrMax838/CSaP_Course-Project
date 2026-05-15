using CSaP.CourseProject.Service;

namespace CSaP.CourseProject;
class Program
{
    static void Main()
    {
        try
        {
            ApplicationContext context = Initializer.Initialize(cleanInitialization: false);

            Shell.Shell shell = Shell.Shell.Create(context);

            shell.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
        }

        Console.WriteLine("Please, press any key...");
        Console.ReadKey();
    }
}
