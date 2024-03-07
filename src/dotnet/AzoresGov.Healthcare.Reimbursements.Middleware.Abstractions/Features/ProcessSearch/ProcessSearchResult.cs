using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchResult
    {
        public ProcessSearchResult(IReadOnlyCollection<ProcessSearchEntityResult> entities, IReadOnlyCollection<ProcessSearchPatientResult> patients, IReadOnlyCollection<ProcessSearchProcessResult> processes, int totalMatchCount)
        {
            Entities = entities;
            Patients = patients;
            Processes = processes;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<ProcessSearchEntityResult> Entities { get; }
        
        public IReadOnlyCollection<ProcessSearchPatientResult> Patients { get; }
        
        public IReadOnlyCollection<ProcessSearchProcessResult> Processes { get; }
        
        public int TotalMatchCount { get; }
    }
}