using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchResult
    {
        public ProcessCreationEntitySearchResult(IReadOnlyCollection<ProcessCreationEntitySearchEntityResult> entities, IReadOnlyCollection<Guid> matches, int totalMatchCount)
        {
            Entities = entities;
            Matches = matches;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<ProcessCreationEntitySearchEntityResult> Entities { get; }
        
        public IReadOnlyCollection<Guid> Matches { get; }
        
        public int TotalMatchCount { get; }
    }
}