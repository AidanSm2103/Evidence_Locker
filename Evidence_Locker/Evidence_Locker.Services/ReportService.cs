using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;

namespace Evidence_Locker.Services
{
    public class ReportService : IReportService
    {
        private readonly ICaseRepository _caseRepository;

        public ReportService(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public IEnumerable<Case> SearchByTitle(string keyword) =>
            _caseRepository.GetAll()
                .Where(c => c.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));

        public Dictionary<CaseStatus, int> GetCaseCountsByStatus() =>
            _caseRepository.GetAll()
                .GroupBy(c => c.Status)
                .ToDictionary(g => g.Key, g => g.Count());

        public IEnumerable<Case> GetCasesOpenedBetween(DateTime start, DateTime end) =>
            _caseRepository.GetAll()
                .Where(c => c.DateOpened >= start && c.DateOpened <= end)
                .OrderBy(c => c.DateOpened);
    }
}
