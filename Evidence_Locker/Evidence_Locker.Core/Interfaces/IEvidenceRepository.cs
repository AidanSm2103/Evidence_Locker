using Evidence_Locker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Evidence-specific repository contract.

namespace Evidence_Locker.Core.Interfaces
{
    public interface IEvidenceRepository : IRepository<Evidence>
    {
        IEnumerable<Evidence> GetByCaseId(int caseId);
    }
}
