using Evidence_Locker.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A single piece of evidence logged against a case.
// Holds its own chain-of-custody history rather than that living on the Case itself, since custody is a property of the evidence

namespace Evidence_Locker.Core.Models
{
    public class Evidence
    {
        public int EvidenceId { get; set; }

        // Foreign-key style reference, not a navigation property
        public int CaseId { get; set; }
        public string Description { get; set; } = string.Empty;
        public EvidenceType Type { get; set; }
        public DateTime DateLogged { get; set; }

        public List<CustodyEntry> ChainOfCustody { get; set; } = new();
    }
}
