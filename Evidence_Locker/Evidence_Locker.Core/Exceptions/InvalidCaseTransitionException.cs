using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Core.Exceptions
{
    public class InvalidCaseTransitionException : Exception
    {
        public InvalidCaseTransitionException(string message) : base(message) { }
    }
}
