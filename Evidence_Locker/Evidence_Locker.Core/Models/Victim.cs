using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Core.Models
{
    // A person who was affected by the incident under investigation
    public class Victim : Person
    {
        public DateTime? DateOfIncident { get; set; }
    }
}
