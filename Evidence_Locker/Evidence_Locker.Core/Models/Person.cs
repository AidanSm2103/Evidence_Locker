using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Base class for anyone involved in a case
// Victim, Suspect, and Detective all inherit from this to share common identity fields 

namespace Evidence_Locker.Core.Models
{
    public abstract class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Computed, not stored — always derived from First/LastName so it can never go out of sync with them
        public string FullName => $"{FirstName} {LastName}";
    }
}
