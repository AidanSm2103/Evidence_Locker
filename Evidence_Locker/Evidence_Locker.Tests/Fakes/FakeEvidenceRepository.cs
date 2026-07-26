using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;

namespace Evidence_Locker.Tests.Fakes
{
    public class FakeEvidenceRepository : IEvidenceRepository
    {
        private readonly List<Evidence> _evidence = new();

        public Evidence? GetById(int id) =>
            _evidence.FirstOrDefault(e => e.EvidenceId == id);

        public IEnumerable<Evidence> GetAll() => _evidence;

        public void Add(Evidence entity)
        {
            entity.EvidenceId = _evidence.Any() ? _evidence.Max(e => e.EvidenceId) + 1 : 1;
            _evidence.Add(entity);
        }

        public void Update(Evidence entity)
        {
            int index = _evidence.FindIndex(e => e.EvidenceId == entity.EvidenceId);
            if (index == -1) return;
            _evidence[index] = entity;
        }

        public void Delete(int id) => _evidence.RemoveAll(e => e.EvidenceId == id);

        public IEnumerable<Evidence> GetByCaseId(int caseId) =>
            _evidence.Where(e => e.CaseId == caseId);
    }
}
