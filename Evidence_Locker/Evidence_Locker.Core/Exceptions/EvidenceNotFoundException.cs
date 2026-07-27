using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Thrown when a lookup by ID fails to find a matching record.
// Currently reused across Case and Evidence lookups 

namespace Evidence_Locker.Core.Exceptions
{
    public class EvidenceNotFoundException : Exception
    {
        public EvidenceNotFoundException(string message) : base(message) { }
    }
}
