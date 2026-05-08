using CSaP.CourseProject.Service;

namespace CSaP.CourseProject;

class Program
{
    static void Main()
    {
        try
        {
            ApplicationContext context = Initializer.Initialize();

            Shell.Shell shell = Shell.Shell.Create(context);

            shell.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
        }

        Console.WriteLine("Simulation finished");
        Console.ReadKey();
    }
}
