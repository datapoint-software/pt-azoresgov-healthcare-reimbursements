using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureEntitySearchResultModel
    {
        public MainProcessCreationFeatureEntitySearchResultModel(int totalMatchCount, IReadOnlyCollection<Guid> entityIds, IReadOnlyCollection<MainProcessCreationFeatureEntityModel> entities)
        {
            TotalMatchCount = totalMatchCount;
            EntityIds = entityIds;
            Entities = entities;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> EntityIds { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureEntityModel> Entities { get; }
    }
}