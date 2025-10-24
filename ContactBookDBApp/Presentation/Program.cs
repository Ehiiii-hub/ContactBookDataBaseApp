using ContactBookDBApp.DataTransferObject.RequestObject;
using ContactBookDBApp.Repository;

namespace ContactBookDBApp.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("Hello, World!"); 
            ConsoleContactBookApp app = new ConsoleContactBookApp();
            while (true)
            {
                app.Run();
                break;
            }
        }
    }
}
