using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
