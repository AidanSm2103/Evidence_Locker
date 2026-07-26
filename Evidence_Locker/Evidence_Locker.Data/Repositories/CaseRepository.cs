using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;
using Evidence_Locker.Data.Context;

namespace Evidence_Locker.Data.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly DataStore<Case> _dataStore;
        private List<Case> _cases;

        public CaseRepository(string filePath)
        {
            _dataStore = new DataStore<Case>(filePath);
            _cases = _dataStore.Load();
        }

        public Case? GetById(int id) =>
            _cases.FirstOrDefault(c => c.CaseId == id);

        public IEnumerable<Case> GetAll() => _cases;

        public void Add(Case entity)
        {
            entity.CaseId = _cases.Any() ? _cases.Max(c => c.CaseId) + 1 : 1;
            _cases.Add(entity);
            _dataStore.Save(_cases);
        }

        public void Update(Case entity)
        {
            int index = _cases.FindIndex(c => c.CaseId == entity.CaseId);
            if (index == -1) return;

            _cases[index] = entity;
            _dataStore.Save(_cases);
        }

        public void Delete(int id)
        {
            _cases.RemoveAll(c => c.CaseId == id);
            _dataStore.Save(_cases);
        }

        public IEnumerable<Case> GetByStatus(CaseStatus status) =>
            _cases.Where(c => c.Status == status);

        public IEnumerable<Case> GetReopenedCases() =>
            GetByStatus(CaseStatus.Reopened);
    }
}

