using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Domain
{
    public enum CaseStatus
    {
        Cold,
        Reopened,
        Closed
    }
    internal class Case
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public CaseStatus Status { get; set; }

        public Case(int id, string title, string summary, CaseStatus status)
        {
            Id = id;
            Title = title;
            Summary = summary;
            Status = status;
        }

        public void Close()
        {
            Status = CaseStatus.Closed;
        }

        public void Reopen()
        {
            Status = CaseStatus.Reopened;
        }

        private readonly List<Evidence> evidenceList = new List<Evidence>();
        public IReadOnlyList<Evidence> EvidenceList => evidenceList.AsReadOnly();

        public void AddEvidence(Evidence evidence)
        {
            if (evidence.CaseId != Id)
            {
                throw new InvalidOperationException("Evidence does not belong to this case.");
            }
            evidenceList.Add(evidence);
        }
    }
}
