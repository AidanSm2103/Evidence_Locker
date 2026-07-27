using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// A timestamped log entry attached to a case
// Currently defined but not yet wired into any service/UI screen, next feature to add

namespace Evidence_Locker.Core.Models
{
    public class CaseNote
    {
        public int CaseNoteId { get; set; }
        public DateTime Timestamp { get; set; }
        public string AuthoredBy { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
