using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchResult
    {
        public ProcessCreationEntitySearchResult(IReadOnlyCollection<ProcessCreationEntityResult> entities, IReadOnlyCollection<Guid> entityIds, int totalMatchCount)
        {
            Entities = entities;
            EntityIds = entityIds;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<ProcessCreationEntityResult> Entities { get; }
        
        public IReadOnlyCollection<Guid> EntityIds { get; }
        
        public int TotalMatchCount { get; }
    }
}