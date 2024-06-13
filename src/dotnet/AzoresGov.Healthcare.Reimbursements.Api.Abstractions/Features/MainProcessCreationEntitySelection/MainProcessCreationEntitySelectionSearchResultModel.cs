using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationEntitySelection
{
    public sealed class MainProcessCreationEntitySelectionSearchResultModel
    {
        public MainProcessCreationEntitySelectionSearchResultModel(int totalMatchCount, IReadOnlyCollection<Guid> entityIds, IReadOnlyCollection<MainProcessCreationEntitySelectionSearchResultEntityModel> entities)
        {
            TotalMatchCount = totalMatchCount;
            EntityIds = entityIds;
            Entities = entities;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> EntityIds { get; }

        public IReadOnlyCollection<MainProcessCreationEntitySelectionSearchResultEntityModel> Entities { get; }
    }
}
