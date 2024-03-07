using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessSearch
{
    public sealed class ProcessSearchResultModel
    {
        public ProcessSearchResultModel(IReadOnlyCollection<ProcessSearchEntityResultModel> entities, IReadOnlyCollection<ProcessSearchPatientResultModel> patients, IReadOnlyCollection<ProcessSearchProcessResultModel> processes, int totalMatchCount)
        {
            Entities = entities;
            Patients = patients;
            Processes = processes;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<ProcessSearchEntityResultModel> Entities { get; }
        
        public IReadOnlyCollection<ProcessSearchPatientResultModel> Patients { get; }
        
        public IReadOnlyCollection<ProcessSearchProcessResultModel> Processes { get; }
        
        public int TotalMatchCount { get; }
    }
}