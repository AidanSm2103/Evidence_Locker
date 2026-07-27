using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;
using Evidence_Locker.Data.Context;

// JSON-backed implementation of IEvidenceRepository 
// Structurally identical to CaseRepository — same load-once/save-on-write pattern, just operating on Evidence instead of Case

namespace Evidence_Locker.Data.Repositories
{
    public class EvidenceRepository : IEvidenceRepository
    {
        private readonly DataStore<Evidence> _dataStore;
        private List<Evidence> _evidence;

        public EvidenceRepository(string filePath)
        {
            _dataStore = new DataStore<Evidence>(filePath);
            _evidence = _dataStore.Load();
        }

        public Evidence? GetById(int id) =>
            _evidence.FirstOrDefault(e => e.EvidenceId == id);

        public IEnumerable<Evidence> GetAll() => _evidence;

        public void Add(Evidence entity)
        {
            entity.EvidenceId = _evidence.Any() ? _evidence.Max(e => e.EvidenceId) + 1 : 1;
            _evidence.Add(entity);
            _dataStore.Save(_evidence);
        }

        public void Update(Evidence entity)
        {
            int index = _evidence.FindIndex(e => e.EvidenceId == entity.EvidenceId);
            if (index == -1) return;

            _evidence[index] = entity;
            _dataStore.Save(_evidence);
        }

        public void Delete(int id)
        {
            _evidence.RemoveAll(e => e.EvidenceId == id);
            _dataStore.Save(_evidence);
        }

        public IEnumerable<Evidence> GetByCaseId(int caseId) =>
            _evidence.Where(e => e.CaseId == caseId);
    }
}

