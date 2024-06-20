using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessSearchResultModel
    {
        public MainProcessSearchFeatureProcessSearchResultModel(int totalMatchCount, IReadOnlyCollection<Guid> processIds, IReadOnlyCollection<MainProcessSearchFeatureEntityModel> entities, IReadOnlyCollection<MainProcessSearchFeaturePatientModel> patients, IReadOnlyCollection<MainProcessSearchFeatureProcessModel> processes)
        {
            TotalMatchCount = totalMatchCount;
            ProcessIds = processIds;
            Entities = entities;
            Patients = patients;
            Processes = processes;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> ProcessIds { get; }

        public IReadOnlyCollection<MainProcessSearchFeatureEntityModel> Entities { get; }

        public IReadOnlyCollection<MainProcessSearchFeaturePatientModel> Patients { get; }

        public IReadOnlyCollection<MainProcessSearchFeatureProcessModel> Processes { get; }
    }
}
