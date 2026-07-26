using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Models;

namespace Evidence_Locker.Core.Interfaces
{
    public interface IReportService
    {
        IEnumerable<Case> SearchByTitle(string keyword);
        Dictionary<CaseStatus, int> GetCaseCountsByStatus();
        IEnumerable<Case> GetCasesOpenedBetween(DateTime start, DateTime end);
    }
}
