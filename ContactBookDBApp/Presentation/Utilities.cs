

namespace ContactBookDBApp.Presentation
{
    public static class Utilities
    {
        public static string ReadStringUserInput(string prompt)
        {
            Console.Write(prompt);
            string userInput = string.Empty;
            while (string.IsNullOrWhiteSpace(userInput)) 
            {
                userInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    break;
                }   
            }
            return userInput;
            //return Console.ReadLine() ?? string.Empty;
        }

        public static int GetIntUserChoice(string prompt, int minOption, int maxOption)
        {
            int userChoice;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < minOption || userChoice > maxOption)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minOption} and {maxOption}.");
                Console.Write(prompt);
            }
            return userChoice;
        }
    }
}
