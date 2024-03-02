using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationPatientSearchResult
    {
        public ProcessCreationPatientSearchResult(IReadOnlyCollection<Guid> patientIds, IReadOnlyCollection<ProcessCreationPatientResult> patients, int totalMatchCount)
        {
            PatientIds = patientIds;
            Patients = patients;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<Guid> PatientIds { get; }
        
        public IReadOnlyCollection<ProcessCreationPatientResult> Patients { get; }
        
        public int TotalMatchCount { get; }
    }
}