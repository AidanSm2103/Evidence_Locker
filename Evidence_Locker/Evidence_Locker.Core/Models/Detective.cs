using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Core.Models
{
    public class Detective : Person
    {
        public string BadgeNumber { get; set; } = string.Empty;
        public string Precinct { get; set; } = string.Empty;
    }
}
