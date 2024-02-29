using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchResultModel
    {
        public ProcessCreationEntitySearchResultModel(IReadOnlyCollection<ProcessCreationEntitySearchEntityResultModel> entities, IReadOnlyCollection<Guid> matches, int totalMatchCount)
        {
            Entities = entities;
            Matches = matches;
            TotalMatchCount = totalMatchCount;
        }

        public IReadOnlyCollection<ProcessCreationEntitySearchEntityResultModel> Entities { get; }
        
        public IReadOnlyCollection<Guid> Matches { get; }
        
        public int TotalMatchCount { get; }
    }
}