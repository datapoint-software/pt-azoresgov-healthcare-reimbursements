using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreationEntitySelection
{
    public sealed class ProcessCreationEntitySelectionSearchResult
    {
        public ProcessCreationEntitySelectionSearchResult(int totalMatchCount, IReadOnlyCollection<Guid> entityIds, IReadOnlyCollection<ProcessCreationEntitySelectionSearchResultEntity> entities)
        {
            TotalMatchCount = totalMatchCount;
            EntityIds = entityIds;
            Entities = entities;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> EntityIds { get; }

        public IReadOnlyCollection<ProcessCreationEntitySelectionSearchResultEntity> Entities { get; }
    }
}
