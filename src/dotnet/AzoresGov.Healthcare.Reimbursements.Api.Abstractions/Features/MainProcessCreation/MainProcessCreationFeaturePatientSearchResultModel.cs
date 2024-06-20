using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public class MainProcessCreationFeaturePatientSearchResultModel
    {
        public MainProcessCreationFeaturePatientSearchResultModel(int totalMatchCount, IReadOnlyCollection<Guid> patientIds, IReadOnlyCollection<MainProcessCreationFeatureEntityModel> entities, IReadOnlyCollection<MainProcessCreationFeaturePatientModel> patients)
        {
            TotalMatchCount = totalMatchCount;
            PatientIds = patientIds;
            Entities = entities;
            Patients = patients;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> PatientIds { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureEntityModel> Entities { get; }

        public IReadOnlyCollection<MainProcessCreationFeaturePatientModel> Patients { get; }
    }
}