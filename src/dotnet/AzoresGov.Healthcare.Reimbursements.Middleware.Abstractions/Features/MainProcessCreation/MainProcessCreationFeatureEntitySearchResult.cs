using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureEntitySearchResult
    {
        public MainProcessCreationFeatureEntitySearchResult(int totalMatchCount, IReadOnlyCollection<Guid> entityIds, IReadOnlyCollection<MainProcessCreationFeatureEntity> entities)
        {
            TotalMatchCount = totalMatchCount;
            EntityIds = entityIds;
            Entities = entities;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> EntityIds { get; }

        public IReadOnlyCollection<MainProcessCreationFeatureEntity> Entities { get; }
    }
}