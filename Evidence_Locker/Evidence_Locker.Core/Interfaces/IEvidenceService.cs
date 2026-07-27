using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Defines evidence logging and chain-of-custody operations
// Also validates that evidence belongs to a real case 

namespace Evidence_Locker.Core.Interfaces
{
    public interface IEvidenceService
    {
        Evidence LogEvidence(int caseId, string description, EvidenceType type);
        Evidence GetEvidence(int evidenceId);
        IEnumerable<Evidence> GetEvidenceForCase(int caseId);
        void AddCustodyEntry(int evidenceId, string handledBy, string notes);
        void DeleteEvidence(int evidenceId);
    }
}
