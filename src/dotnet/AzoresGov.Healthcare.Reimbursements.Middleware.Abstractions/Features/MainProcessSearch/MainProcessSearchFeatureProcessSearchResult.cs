using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessSearchResult
    {
        public MainProcessSearchFeatureProcessSearchResult(int totalMatchCount, IReadOnlyCollection<Guid> processIds, IReadOnlyCollection<MainProcessSearchFeatureEntity> entities, IReadOnlyCollection<MainProcessSearchFeaturePatient> patients, IReadOnlyCollection<MainProcessSearchFeatureProcess> processes)
        {
            TotalMatchCount = totalMatchCount;
            ProcessIds = processIds;
            Entities = entities;
            Patients = patients;
            Processes = processes;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> ProcessIds { get; }

        public IReadOnlyCollection<MainProcessSearchFeatureEntity> Entities { get; }

        public IReadOnlyCollection<MainProcessSearchFeaturePatient> Patients { get; }

        public IReadOnlyCollection<MainProcessSearchFeatureProcess> Processes { get; }
    }
}
