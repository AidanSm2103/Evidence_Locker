using Evidence_Locker.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The central entity in the system. A Case is a pure data container and holds no logic of its own  
/// All state transition rules live in CaseService 

namespace Evidence_Locker.Core.Models
{
    public class Case
    {
        public int CaseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public CaseStatus Status { get; set; }

        public List<Victim> Victims { get; set; } = new();
        public List<Suspect> Suspects { get; set; } = new();
        public List<Detective> AssignedDetectives { get; set; } = new();

        public List<Evidence> Evidence { get; set; } = new();
        public List<CaseNote> Notes { get; set; } = new();
    }
}
