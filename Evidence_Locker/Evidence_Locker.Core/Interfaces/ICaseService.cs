using Evidence_Locker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Core.Interfaces
{
    public interface ICaseService
    {
        Case CreateCase(string title);
        Case GetCase(int caseId);
        IEnumerable<Case> GetAllCases();
        void CloseCase(int caseId);
        void ReopenCase(int caseId);
        void MarkCold(int caseId);
    }
}
