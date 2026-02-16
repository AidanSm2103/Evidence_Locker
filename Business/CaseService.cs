using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evidence_Locker.Domain;

namespace Evidence_Locker.Business
{
    internal class CaseService
    {
        private List<Case> cases = new List<Case>();
        public void AddCase(Case newCase)
        {
            cases.Add(newCase);
        }
        public Case GetCaseById(int id)
        {
            return cases.FirstOrDefault(c => c.Id == id);
        }
        public List<Case> GetAllCases()
        {
            return cases;
        }
        public void CloseCase(int id)
        {
            var caseToClose = GetCaseById(id);
            if (caseToClose != null)
            {
                caseToClose.Close();
            }
        }
        public void ReopenCase(int id)
        {
            var caseToReopen = GetCaseById(id);
            if (caseToReopen != null)
            {
                    caseToReopen.Reopen();
            }
        }

        public void AddEvidenceToCase(int caseId, Evidence evidence)
        {
            var caseToUpdate = GetCaseById(caseId);

            if (caseToUpdate != null)
            {
                caseToUpdate.AddEvidence(evidence);
            }
        }
    }
}
