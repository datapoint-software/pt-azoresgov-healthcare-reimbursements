using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationPatientSearchResultModel
    {
        public ProcessCreationPatientSearchResultModel(IReadOnlyCollection<Guid> patientIds, IReadOnlyCollection<ProcessCreationPatientResultModel> patients, int totalMatchCount)
        {
            PatientIds = patientIds;
            Patients = patients;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<Guid> PatientIds { get; }
        
        public IReadOnlyCollection<ProcessCreationPatientResultModel> Patients { get; }
        
        public int TotalMatchCount { get; }
    }
}