using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Thrown when a case status change violates the allowed state machine 
// Thrown by CaseService, caught and displayed by the UI layer.

namespace Evidence_Locker.Core.Exceptions
{
    public class InvalidCaseTransitionException : Exception
    {
        public InvalidCaseTransitionException(string message) : base(message) { }
    }
}
