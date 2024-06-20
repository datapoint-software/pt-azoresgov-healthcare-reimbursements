using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public class MainProcessCreationFeaturePatientSearchResult
    {
        public MainProcessCreationFeaturePatientSearchResult(int totalMatchCount, IReadOnlyCollection<Guid> patientIds, IReadOnlyCollection<MainProcessCreationFeatureEntity> entities, IReadOnlyCollection<MainProcessCreationFeaturePatient> patients)
        {
            TotalMatchCount = totalMatchCount;
            PatientIds = patientIds;
            Entities = entities;
            Patients = patients;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> PatientIds { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureEntity> Entities { get; }

        public IReadOnlyCollection<MainProcessCreationFeaturePatient> Patients { get; }
    }
}