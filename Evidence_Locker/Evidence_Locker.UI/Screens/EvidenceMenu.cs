using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Exceptions;
using Evidence_Locker.Core.Interfaces;

namespace Evidence_Locker.UI.Screens
{
    public class EvidenceMenu
    {
        private readonly IEvidenceService _evidenceService;

        public EvidenceMenu(IEvidenceService evidenceService)
        {
            _evidenceService = evidenceService;
        }

        public void Show()
        {
            bool back = false;
            while (!back)
            {
                Console.Clear();
                ConsoleTheme.Header("EVIDENCE MANAGEMENT");
                Console.WriteLine("  1. Log New Evidence");
                Console.WriteLine("  2. View Evidence for a Case");
                Console.WriteLine("  3. Add Chain of Custody Entry");
                Console.WriteLine("  4. Delete Evidence");
                Console.WriteLine("  5. Back to Main Menu");

                int choice = InputHandler.GetInt("\nSelect an option");

                try
                {
                    switch (choice)
                    {
                        case 1: LogEvidence(); break;
                        case 2: ViewEvidenceForCase(); break;
                        case 3: AddCustodyEntry(); break;
                        case 4: DeleteEvidence(); break;
                        case 5: back = true; break;
                        default: ConsoleTheme.Error("Invalid option."); break;
                    }
                }
                catch (EvidenceNotFoundException ex)
                {
                    ConsoleTheme.Error(ex.Message);
                }

                if (!back) InputHandler.PressAnyKeyToContinue();
            }
        }

        private void LogEvidence()
        {
            ConsoleTheme.SubHeader("Log New Evidence");
            int caseId = InputHandler.GetInt("Case ID");
            string description = InputHandler.GetString("Description");
            var type = InputHandler.GetEnum<EvidenceType>("Evidence type");

            var evidence = _evidenceService.LogEvidence(caseId, description, type);
            ConsoleTheme.Success($"Evidence #{evidence.EvidenceId} logged against Case #{caseId}.");
        }

        private void ViewEvidenceForCase()
        {
            int caseId = InputHandler.GetInt("Case ID");
            var items = _evidenceService.GetEvidenceForCase(caseId).ToList();

            if (!items.Any())
            {
                ConsoleTheme.Warning("No evidence logged for this case.");
                return;
            }

            ConsoleTheme.SubHeader($"Evidence for Case #{caseId}");
            foreach (var e in items)
            {
                Console.WriteLine($"  #{e.EvidenceId,-4} [{e.Type}] {e.Description}  (custody entries: {e.ChainOfCustody.Count})");
            }
        }

        private void AddCustodyEntry()
        {
            int evidenceId = InputHandler.GetInt("Evidence ID");
            string handledBy = InputHandler.GetString("Handled by");
            string notes = InputHandler.GetString("Notes");

            _evidenceService.AddCustodyEntry(evidenceId, handledBy, notes);
            ConsoleTheme.Success($"Custody entry added to Evidence #{evidenceId}.");
        }

        private void DeleteEvidence()
        {
            int evidenceId = InputHandler.GetInt("Evidence ID to delete");
            if (InputHandler.Confirm($"Confirm deleting evidence #{evidenceId}?"))
            {
                _evidenceService.DeleteEvidence(evidenceId);
                ConsoleTheme.Success($"Evidence #{evidenceId} deleted.");
            }
        }
    }
}

