using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Exceptions;
using Evidence_Locker.Core.Interfaces;
using Evidence_Locker.Core.Models;

// Implements IEvidenceService
// Depends on both IEvidenceRepository and ICaseRepository the second one exists purely so LogEvidence can confirm a case actually exists before attaching evidence to it


namespace Evidence_Locker.Services
{
    public class EvidenceService : IEvidenceService
    {
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly ICaseRepository _caseRepository;

        public EvidenceService(IEvidenceRepository evidenceRepository, ICaseRepository caseRepository)
        {
            _evidenceRepository = evidenceRepository;
            _caseRepository = caseRepository;
        }

        public Evidence LogEvidence(int caseId, string description, EvidenceType type)
        {
            // Validate the case actually exists before attaching evidence to it
            var parentCase = _caseRepository.GetById(caseId);
            if (parentCase is null)
                throw new EvidenceNotFoundException($"Cannot log evidence: case {caseId} does not exist.");

            var evidence = new Evidence
            {
                CaseId = caseId,
                Description = description,
                Type = type,
                DateLogged = DateTime.Now
            };

            _evidenceRepository.Add(evidence);
            return evidence;
        }

        public Evidence GetEvidence(int evidenceId)
        {
            var found = _evidenceRepository.GetById(evidenceId);
            if (found is null)
                throw new EvidenceNotFoundException($"Evidence with ID {evidenceId} was not found.");

            return found;
        }

        public IEnumerable<Evidence> GetEvidenceForCase(int caseId) =>
            _evidenceRepository.GetByCaseId(caseId);

        public void AddCustodyEntry(int evidenceId, string handledBy, string notes)
        {
            var evidence = GetEvidence(evidenceId);

            var entry = new CustodyEntry
            {
                CustodyEntryId = evidence.ChainOfCustody.Any()
                    ? evidence.ChainOfCustody.Max(e => e.CustodyEntryId) + 1
                    : 1,
                HandledBy = handledBy,
                DateTransferred = DateTime.Now,
                Notes = notes
            };

            evidence.ChainOfCustody.Add(entry);
            _evidenceRepository.Update(evidence);
        }

        public void DeleteEvidence(int evidenceId)
        {
            // Confirms it exists before deleting
            GetEvidence(evidenceId);
            _evidenceRepository.Delete(evidenceId);
        }
    }
}
