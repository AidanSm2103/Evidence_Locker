using Evidence_Locker.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Core.Models
{
    public class Evidence
    {
        public int EvidenceId { get; set; }
        public int CaseId { get; set; }
        public string Description { get; set; } = string.Empty;
        public EvidenceType Type { get; set; }
        public DateTime DateLogged { get; set; }

        public List<CustodyEntry> ChainOfCustody { get; set; } = new();
    }
}
