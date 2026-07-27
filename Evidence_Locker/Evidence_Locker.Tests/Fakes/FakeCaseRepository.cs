using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;

// In-memory stand-in for ICaseRepository, used only in tests
// No file I/O — this is what lets CaseServiceTests run instantly and never touch or depend on a real cases.json file

namespace Evidence_Locker.Tests.Fakes
{
    public class FakeCaseRepository : ICaseRepository
    {
        private readonly List<Case> _cases = new();

        public Case? GetById(int id) =>
            _cases.FirstOrDefault(c => c.CaseId == id);

        public IEnumerable<Case> GetAll() => _cases;

        public void Add(Case entity)
        {
            entity.CaseId = _cases.Any() ? _cases.Max(c => c.CaseId) + 1 : 1;
            _cases.Add(entity);
        }

        public void Update(Case entity)
        {
            int index = _cases.FindIndex(c => c.CaseId == entity.CaseId);
            if (index == -1) return;
            _cases[index] = entity;
        }

        public void Delete(int id) => _cases.RemoveAll(c => c.CaseId == id);

        public IEnumerable<Case> GetByStatus(CaseStatus status) =>
            _cases.Where(c => c.Status == status);

        public IEnumerable<Case> GetReopenedCases() =>
            GetByStatus(CaseStatus.Reopened);
    }
}

