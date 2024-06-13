using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationEntitySelection
{
    public sealed class MainProcessCreationEntitySelectionSearchResult
    {
        public MainProcessCreationEntitySelectionSearchResult(int totalMatchCount, IReadOnlyCollection<Guid> entityIds, IReadOnlyCollection<MainProcessCreationEntitySelectionSearchResultEntity> entities)
        {
            TotalMatchCount = totalMatchCount;
            EntityIds = entityIds;
            Entities = entities;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> EntityIds { get; }

        public IReadOnlyCollection<MainProcessCreationEntitySelectionSearchResultEntity> Entities { get; }
    }
}
