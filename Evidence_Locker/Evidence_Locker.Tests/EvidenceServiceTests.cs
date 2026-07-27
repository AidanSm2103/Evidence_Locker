using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Core.Enums;
using Evidence_Locker.Core.Exceptions;
using Evidence_Locker.Services;
using Evidence_Locker.Tests.Fakes;
using Xunit;

// Tests for EvidenceService
// EvidenceService depends on bothIEvidenceRepository and ICaseRepository, since logging evidence requires confirming the parent case actually exists first

namespace Evidence_Locker.Tests
{
    public class EvidenceServiceTests
    {
        private static EvidenceService CreateService(out FakeCaseRepository caseRepo, out FakeEvidenceRepository evidenceRepo)
        {
            caseRepo = new FakeCaseRepository();
            evidenceRepo = new FakeEvidenceRepository();
            return new EvidenceService(evidenceRepo, caseRepo);
        }

        [Fact]
        public void LogEvidence_WhenCaseExists_AddsEvidence()
        {
            var service = CreateService(out var caseRepo, out _);
            // A real CaseService is used here to create the case, so the case exists exactly the way it would  in production 

            var caseService = new CaseService(caseRepo);
            var newCase = caseService.CreateCase("Bank fraud - Meridian Trust");

            var evidence = service.LogEvidence(newCase.CaseId, "Ledger printout", EvidenceType.Document);

            Assert.Equal(newCase.CaseId, evidence.CaseId);
            Assert.NotEqual(0, evidence.EvidenceId);
        }

        [Fact]
        public void LogEvidence_WhenCaseDoesNotExist_ThrowsEvidenceNotFoundException()
        {
            var service = CreateService(out _, out _);

            // No case was ever created — caseId 999 doesn't exist in the fake repo, which is exactly the scenario this test targets
            Assert.Throws<EvidenceNotFoundException>(
                () => service.LogEvidence(999, "Mystery item", EvidenceType.Physical));
        }

        [Fact]
        public void AddCustodyEntry_AppendsEntryToChain()
        {
            var service = CreateService(out var caseRepo, out _);
            var caseService = new CaseService(caseRepo);
            var newCase = caseService.CreateCase("Grand theft auto - Lot 12");
            var evidence = service.LogEvidence(newCase.CaseId, "Recovered vehicle key", EvidenceType.Physical);

            service.AddCustodyEntry(evidence.EvidenceId, "Officer Reyes", "Transferred to forensics lab");
            var updated = service.GetEvidence(evidence.EvidenceId);

            // Confirms both that exactly one entry was added, and that its content matches what was passed in 
            Assert.Single(updated.ChainOfCustody);
            Assert.Equal("Officer Reyes", updated.ChainOfCustody[0].HandledBy);
        }

        [Fact]
        public void DeleteEvidence_WhenIdDoesNotExist_ThrowsEvidenceNotFoundException()
        {
            var service = CreateService(out _, out _);

            Assert.Throws<EvidenceNotFoundException>(
                () => service.DeleteEvidence(999));
        }
    }
}
