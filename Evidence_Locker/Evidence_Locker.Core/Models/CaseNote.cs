using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
