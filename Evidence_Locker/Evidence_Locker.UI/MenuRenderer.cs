using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.UI.Screens;

// The top-level menu loop
// Owns the three sub-menus and routes to each based on the user's choice

namespace Evidence_Locker.UI
{
    public class MenuRenderer
    {
        private readonly CaseMenu _caseMenu;
        private readonly EvidenceMenu _evidenceMenu;
        private readonly ReportMenu _reportMenu;

        public MenuRenderer(CaseMenu caseMenu, EvidenceMenu evidenceMenu, ReportMenu reportMenu)
        {
            _caseMenu = caseMenu;
            _evidenceMenu = evidenceMenu;
            _reportMenu = reportMenu;
        }

        public void Run()
        {
            Console.Clear();
            ConsoleTheme.PrintBanner();

            bool exit = false;
            while (!exit)
            {
                ConsoleTheme.Header("MAIN MENU");
                Console.WriteLine("  1. Case Management");
                Console.WriteLine("  2. Evidence Management");
                Console.WriteLine("  3. Search & Reports");
                Console.WriteLine("  4. Exit");

                int choice = InputHandler.GetInt("\nSelect an option");

                switch (choice)
                {
                    case 1: _caseMenu.Show(); break;
                    case 2: _evidenceMenu.Show(); break;
                    case 3: _reportMenu.Show(); break;
                    case 4:
                        exit = true;
                        ConsoleTheme.Success("Locking the evidence room. Goodbye, Detective.");
                        break;
                    default:
                        ConsoleTheme.Error("Invalid option.");
                        break;
                }
            }
        }
    }
}
