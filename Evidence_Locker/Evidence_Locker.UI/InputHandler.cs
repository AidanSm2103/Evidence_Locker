using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Validated console input helpers.
// Exists so every screen doesn't reimplement its own retry loop, that logic lives here once

namespace Evidence_Locker.UI
{
    public static class InputHandler
    {
        public static string GetString(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{prompt}: ");
            Console.ResetColor();
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        // Loops forever until a valid int is entered 
        public static int GetInt(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                    return result;

                ConsoleTheme.Error("Enter a valid whole number.");
            }
        }

        public static T GetEnum<T>(string prompt) where T : struct, Enum
        {
            var values = Enum.GetValues<T>();

            Console.WriteLine($"\n{prompt}:");
            for (int i = 0; i < values.Length; i++)
                Console.WriteLine($"  {i + 1}. {values[i]}");

            while (true)
            {
                Console.Write("Choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= values.Length)
                    return values[choice - 1];

                ConsoleTheme.Error($"Enter a number between 1 and {values.Length}.");
            }
        }

        public static bool Confirm(string prompt)
        {
            Console.Write($"{prompt} (y/n): ");
            var response = Console.ReadLine()?.Trim().ToLower();
            return response == "y" || response == "yes";
        }

        public static void PressAnyKeyToContinue()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to continue...");
            Console.ResetColor();
            // true = don't echo the key pressed
            Console.ReadKey(true);
        }
    }
}
