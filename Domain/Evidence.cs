using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Locker.Domain
{
    public enum EvidenceType
    {
        WitnessStatement,
        ForensicReport,
        PhysicalItem
    }
    internal class Evidence
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public EvidenceType Type { get; set; }
        public int CaseId { get; set; }
        public DateTime DateCollected { get; set; }
        public Evidence(int id, string description, EvidenceType type, int caseId, DateTime dateCollected)
        {
            Id = id;
            Description = description;
            Type = type;
            CaseId = caseId;
            DateCollected = dateCollected;
        }
    }
}
