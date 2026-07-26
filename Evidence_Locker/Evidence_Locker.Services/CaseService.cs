using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Exceptions;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;

namespace Evidence_Locker.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;

        public CaseService(ICaseRepository caseRepository)
        {
            _caseRepository = caseRepository;
        }

        public Case CreateCase(string title)
        {
            var newCase = new Case
            {
                Title = title,
                DateOpened = DateTime.Now,
                Status = CaseStatus.Open
            };

            _caseRepository.Add(newCase);
            return newCase;
        }

        public Case GetCase(int caseId)
        {
            var found = _caseRepository.GetById(caseId);
            if (found is null)
                throw new EvidenceNotFoundException($"Case with ID {caseId} was not found.");

            return found;
        }

        public IEnumerable<Case> GetAllCases() => _caseRepository.GetAll();

        public void CloseCase(int caseId)
        {
            var caseToClose = GetCase(caseId);

            if (caseToClose.Status == CaseStatus.Closed)
                throw new InvalidCaseTransitionException(
                    $"Case {caseId} is already closed.");

            caseToClose.Status = CaseStatus.Closed;
            caseToClose.DateClosed = DateTime.Now;
            _caseRepository.Update(caseToClose);
        }

        public void ReopenCase(int caseId)
        {
            var caseToReopen = GetCase(caseId);

            if (caseToReopen.Status is CaseStatus.Open or CaseStatus.Reopened)
                throw new InvalidCaseTransitionException(
                    $"Case {caseId} is already active and cannot be reopened.");

            caseToReopen.Status = CaseStatus.Reopened;
            caseToReopen.DateClosed = null;
            _caseRepository.Update(caseToReopen);
        }

        public void MarkCold(int caseId)
        {
            var caseToMark = GetCase(caseId);

            if (caseToMark.Status != CaseStatus.Open && caseToMark.Status != CaseStatus.Reopened)
                throw new InvalidCaseTransitionException(
                    $"Case {caseId} must be Open or Reopened to be marked Cold. Current status: {caseToMark.Status}.");

            caseToMark.Status = CaseStatus.Cold;
            _caseRepository.Update(caseToMark);
        }
    }
}

