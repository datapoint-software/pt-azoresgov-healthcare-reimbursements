using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchResultModel
    {
        public MainProcessCreationPatientSelectionFeatureSearchResultModel(int totalMatchCount, IReadOnlyCollection<Guid> patientIds, IReadOnlyCollection<MainProcessCreationPatientSelectionFeatureSearchResultPatientModel> patients)
        {
            TotalMatchCount = totalMatchCount;
            PatientIds = patientIds;
            Patients = patients;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> PatientIds { get; }

        public IReadOnlyCollection<MainProcessCreationPatientSelectionFeatureSearchResultPatientModel> Patients { get; }
    }
}