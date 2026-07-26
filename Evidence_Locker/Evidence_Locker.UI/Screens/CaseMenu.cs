using Evidence_Locker.Core.Exceptions;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.UI.Screens
{
    public class CaseMenu
    {
        private readonly ICaseService _caseService;

        public CaseMenu(ICaseService caseService)
        {
            _caseService = caseService;
        }

        public void Show()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                ConsoleTheme.Header("CASE MANAGEMENT");
                Console.WriteLine("  1. Open New Case");
                Console.WriteLine("  2. View All Cases");
                Console.WriteLine("  3. View Case Details");
                Console.WriteLine("  4. Close Case");
                Console.WriteLine("  5. Mark Case Cold");
                Console.WriteLine("  6. Reopen Case");
                Console.WriteLine("  7. Back to Main Menu");

                int choice = InputHandler.GetInt("\nSelect an option");

                try
                {
                    switch (choice)
                    {
                        case 1: OpenNewCase(); break;
                        case 2: ViewAllCases(); break;
                        case 3: ViewCaseDetails(); break;
                        case 4: CloseCase(); break;
                        case 5: MarkCold(); break;
                        case 6: ReopenCase(); break;
                        case 7: back = true; break;
                        default: ConsoleTheme.Error("Invalid option."); break;
                    }
                }
                catch (InvalidCaseTransitionException ex)
                {
                    ConsoleTheme.Error(ex.Message);
                }
                catch (EvidenceNotFoundException ex)
                {
                    ConsoleTheme.Error(ex.Message);
                }

                if (!back) InputHandler.PressAnyKeyToContinue();
            }
        }

        private void OpenNewCase()
        {
            ConsoleTheme.SubHeader("Open New Case");
            string title = InputHandler.GetString("Case title");
            var newCase = _caseService.CreateCase(title);
            ConsoleTheme.Success($"Case #{newCase.CaseId} opened: \"{newCase.Title}\"");
        }

        private void ViewAllCases()
        {
            ConsoleTheme.SubHeader("All Cases");
            var cases = _caseService.GetAllCases().ToList();

            if (!cases.Any())
            {
                ConsoleTheme.Warning("No cases on file.");
                return;
            }

            foreach (var c in cases)
            {
                Console.Write($"  #{c.CaseId,-4} {c.Title,-35} ");
                ConsoleTheme.StatusTag(c.Status);
                Console.WriteLine($"   Opened: {c.DateOpened:yyyy-MM-dd}");
            }
        }

        private void ViewCaseDetails()
        {
            int id = InputHandler.GetInt("Case ID");
            var c = _caseService.GetCase(id);

            ConsoleTheme.SubHeader($"Case #{c.CaseId}: {c.Title}");
            Console.Write("Status: "); ConsoleTheme.StatusTag(c.Status); Console.WriteLine();
            Console.WriteLine($"Opened: {c.DateOpened:yyyy-MM-dd}");
            Console.WriteLine($"Closed: {(c.DateClosed.HasValue ? c.DateClosed.Value.ToString("yyyy-MM-dd") : "-")}");
            Console.WriteLine($"Evidence items: {c.Evidence.Count}");
            Console.WriteLine($"Detectives assigned: {c.AssignedDetectives.Count}");
        }

        private void CloseCase()
        {
            int id = InputHandler.GetInt("Case ID to close");
            if (InputHandler.Confirm($"Confirm closing case #{id}?"))
            {
                _caseService.CloseCase(id);
                ConsoleTheme.Success($"Case #{id} closed.");
            }
        }

        private void MarkCold()
        {
            int id = InputHandler.GetInt("Case ID to mark Cold");
            _caseService.MarkCold(id);
            ConsoleTheme.Success($"Case #{id} marked Cold.");
        }

        private void ReopenCase()
        {
            int id = InputHandler.GetInt("Case ID to reopen");
            _caseService.ReopenCase(id);
            ConsoleTheme.Success($"Case #{id} has been reopened.");
        }
    }
}

