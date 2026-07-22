using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Core.Models
{
    public class Victim : Person
    {
        public DateTime? DateOfIncident { get; set; }
    }
}
