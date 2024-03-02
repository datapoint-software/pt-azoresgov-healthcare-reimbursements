using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchResultModel
    {
        public ProcessCreationEntitySearchResultModel(IReadOnlyCollection<ProcessCreationEntityResultModel> entities, IReadOnlyCollection<Guid> entityIds, int totalMatchCount)
        {
            Entities = entities;
            EntityIds = entityIds;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<ProcessCreationEntityResultModel> Entities { get; }
        
        public IReadOnlyCollection<Guid> EntityIds { get; }
        
        public int TotalMatchCount { get; }
    }
}