using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreationEntitySelection
{
    public sealed class ProcessCreationEntitySelectionSearchResultModel
    {
        public ProcessCreationEntitySelectionSearchResultModel(int totalMatchCount, IReadOnlyCollection<Guid> entityIds, IReadOnlyCollection<ProcessCreationEntitySelectionSearchResultEntityModel> entities)
        {
            TotalMatchCount = totalMatchCount;
            EntityIds = entityIds;
            Entities = entities;
        }

        public int TotalMatchCount { get; }

        public IReadOnlyCollection<Guid> EntityIds { get; }

        public IReadOnlyCollection<ProcessCreationEntitySelectionSearchResultEntityModel> Entities { get; }
    }
}
