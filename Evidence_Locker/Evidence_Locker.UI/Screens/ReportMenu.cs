using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Interfaces;

namespace Evidence_Locker.UI.Screens
{
    public class ReportMenu
    {
        private readonly IReportService _reportService;

        public ReportMenu(IReportService reportService)
        {
            _reportService = reportService;
        }

        public void Show()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                ConsoleTheme.Header("SEARCH & REPORTS");
                Console.WriteLine("  1. Search Cases by Keyword");
                Console.WriteLine("  2. Case Counts by Status");
                Console.WriteLine("  3. Cases Opened in Date Range");
                Console.WriteLine("  4. Back to Main Menu");

                int choice = InputHandler.GetInt("\nSelect an option");

                switch (choice)
                {
                    case 1: SearchByKeyword(); break;
                    case 2: CaseCountsByStatus(); break;
                    case 3: CasesInDateRange(); break;
                    case 4: back = true; break;
                    default: ConsoleTheme.Error("Invalid option."); break;
                }

                if (!back) InputHandler.PressAnyKeyToContinue();
            }
        }

        private void SearchByKeyword()
        {
            string keyword = InputHandler.GetString("Search keyword");
            var results = _reportService.SearchByTitle(keyword).ToList();

            if (!results.Any())
            {
                ConsoleTheme.Warning("No matching cases.");
                return;
            }

            foreach (var c in results)
                Console.WriteLine($"  #{c.CaseId} {c.Title} [{c.Status}]");
        }

        private void CaseCountsByStatus()
        {
            var counts = _reportService.GetCaseCountsByStatus();
            ConsoleTheme.SubHeader("Case Counts by Status");

            foreach (var kvp in counts)
                Console.WriteLine($"  {kvp.Key,-10}: {kvp.Value}");
        }

        private void CasesInDateRange()
        {
            Console.Write("Start date (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out var start);
            Console.Write("End date (yyyy-MM-dd): ");
            DateTime.TryParse(Console.ReadLine(), out var end);

            var results = _reportService.GetCasesOpenedBetween(start, end).ToList();

            if (!results.Any())
            {
                ConsoleTheme.Warning("No cases opened in that range.");
                return;
            }

            foreach (var c in results)
                Console.WriteLine($"  #{c.CaseId} {c.Title} — opened {c.DateOpened:yyyy-MM-dd}");
        }
    }
}

