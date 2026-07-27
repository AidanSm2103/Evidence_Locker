using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A single link in the chain of custody for a piece of evidence: records who handled it and when
// Entries are append-only: once created, a CustodyEntry is never edited, only added to the list

namespace Evidence_Locker.Core.Models
{
    public class CustodyEntry
    {
        public int CustodyEntryId { get; set; }
        public string HandledBy { get; set; } = string.Empty;
        public DateTime DateTransferred { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
