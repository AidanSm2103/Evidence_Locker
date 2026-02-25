using Evidence_Locker.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker
{
    internal class Program
    {
        public enum MenuOption
        {
            RegisterCase = 1,
            ViewAllCases,
            ViewCaseById,
            AddEvidence,
            CloseCase,
            ReopenCase,
            Exit
        }
        
        private static CaseService caseService = new CaseService();

        static void Main(string[] args)
        {
            ConfigureConsole();
            ShowStartupScreen();

            Console.ReadKey(true);

            while (true)
            {
                DrawMenu();

                var input = Console.ReadLine();

                if (!Enum.TryParse(input, out MenuOption selectedOption))
                { 
                    continue;
                }

                switch (selectedOption)
                {
                    case MenuOption.RegisterCase:

                        break;

                    case MenuOption.ViewAllCases:
                        ViewAllCases();    
                        break;

                    case MenuOption.ViewCaseById:

                        break;

                    case MenuOption.AddEvidence:

                        break;

                    case MenuOption.CloseCase:

                        break;

                    case MenuOption.ReopenCase:

                        break;

                    case MenuOption.Exit:

                        return;
                }
            }
        }

        static void ConfigureConsole()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
        }

        static void ShowStartupScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("EVIDENCE LOCKER SYSTEM v1.0");
            Console.WriteLine("Secure Case Registry");
            Console.WriteLine("----------------------------------------");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine("Press any key to access terminal...");
        }

        static void DrawMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║            EVIDENCE LOCKER SYSTEM          ║");
            Console.WriteLine("╠════════════════════════════════════════════╣");

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("║                                            ║");
            Console.WriteLine("║  [1] Register New Case                     ║");
            Console.WriteLine("║  [2] View All Cases                        ║");
            Console.WriteLine("║  [3] View Case By ID                       ║");
            Console.WriteLine("║  [4] Add Evidence To Case                  ║");
            Console.WriteLine("║  [5] Close Case                            ║");
            Console.WriteLine("║  [6] Reopen Case                           ║");
            Console.WriteLine("║  [7] Exit                                  ║");
            Console.WriteLine("║                                            ║");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚════════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.Write("Select operation: ");
        }

        static void ViewAllCases()
        {
            Console.Clear();

            var cases = caseService.GetAllCases();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("CASE REGISTRY");
            Console.WriteLine("----------------------------------------");

            Console.ForegroundColor = ConsoleColor.Gray;

            if (cases.Count == 0)
            {
                Console.WriteLine("[INFO] No cases registered.");
                return;
            }

            Console.WriteLine("ID    STATUS               EVIDENCE COUNT");
            Console.WriteLine("----------------------------------------");

            foreach (var c in cases)
            {
                Console.WriteLine(
                    $"{c.Id,-5} {c.Status,-20} {c.EvidenceList.Count}"
                );
            }

            Console.WriteLine("----------------------------------------");
        }
    }
}
