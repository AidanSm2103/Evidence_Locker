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

// Tests for the case status state machine in CaseService
// Every test builds its own fresh CaseService + FakeCaseRepository, so tests never share state or affect each other's results

namespace Evidence_Locker.Tests
{
    public class CaseServiceTests
    {
        // Small helper so each test starts with a fresh service + repo
        private static CaseService CreateService(out FakeCaseRepository repo)
        {
            repo = new FakeCaseRepository();
            return new CaseService(repo);
        }

        [Fact]
        public void CreateCase_SetsStatusToOpen()
        {
            var service = CreateService(out _);

            var newCase = service.CreateCase("Missing person - Ashwood Park");

            Assert.Equal(CaseStatus.Open, newCase.Status);
            Assert.NotEqual(0, newCase.CaseId);
        }

        [Fact]
        public void CloseCase_WhenOpen_SetsStatusToClosedAndSetsDateClosed()
        {
            var service = CreateService(out _);
            var newCase = service.CreateCase("Warehouse break-in");

            service.CloseCase(newCase.CaseId);
            var updated = service.GetCase(newCase.CaseId);

            Assert.Equal(CaseStatus.Closed, updated.Status);
            Assert.NotNull(updated.DateClosed);
        }

        [Fact]
        public void CloseCase_WhenAlreadyClosed_ThrowsInvalidCaseTransitionException()
        {
            var service = CreateService(out _);
            var newCase = service.CreateCase("Arson - Fifth Street");
            service.CloseCase(newCase.CaseId);

            // Assert.Throws requires the call inside a lambda so it can actually execute the method and catch the exception itself 
            // Calling CloseCase directly here would just throw and fail the test rather than test anything
            Assert.Throws<InvalidCaseTransitionException>(
                () => service.CloseCase(newCase.CaseId));
        }

        [Fact]
        public void ReopenCase_WhenOpen_ThrowsInvalidCaseTransitionException()
        {
            var service = CreateService(out _);
            var newCase = service.CreateCase("Vandalism - Overpass");

            Assert.Throws<InvalidCaseTransitionException>(
                () => service.ReopenCase(newCase.CaseId));
        }

        [Fact]
        public void ReopenCase_WhenCold_SetsStatusToReopened()
        {
            var service = CreateService(out _);
            var newCase = service.CreateCase("Robbery - Fourth National");
            service.MarkCold(newCase.CaseId);

            service.ReopenCase(newCase.CaseId);
            var updated = service.GetCase(newCase.CaseId);

            Assert.Equal(CaseStatus.Reopened, updated.Status);
        }

        [Fact]
        public void MarkCold_WhenClosed_ThrowsInvalidCaseTransitionException()
        {
            var service = CreateService(out _);
            var newCase = service.CreateCase("Hit and run - Route 9");
            service.CloseCase(newCase.CaseId);

            Assert.Throws<InvalidCaseTransitionException>(
                () => service.MarkCold(newCase.CaseId));
        }

        [Fact]
        public void GetCase_WhenIdDoesNotExist_ThrowsEvidenceNotFoundException()
        {
            var service = CreateService(out _);

            Assert.Throws<EvidenceNotFoundException>(
                () => service.GetCase(999));
        }
    }
}

