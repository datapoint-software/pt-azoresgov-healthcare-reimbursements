using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchResult
    {
        public MainProcessCreationPatientSelectionFeatureSearchResult(int totalMatchCount, IReadOnlyCollection<Guid> patientIds, IReadOnlyCollection<MainProcessCreationPatientSelectionFeatureSearchResultPatient> patients)
        {
            TotalMatchCount = totalMatchCount;
            PatientIds = patientIds;
            Patients = patients;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> PatientIds { get; }

        public IReadOnlyCollection<MainProcessCreationPatientSelectionFeatureSearchResultPatient> Patients { get; }
    }
}