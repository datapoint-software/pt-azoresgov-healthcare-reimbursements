using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchQuery : Query<ProcessSearchResult>
    {
        public ProcessSearchQuery(Guid userId, Guid? entityId, string? filter, IReadOnlyCollection<ProcessStatus>? status, int? skip, int? take)
        {
            UserId = userId;
            EntityId = entityId;
            Filter = filter;
            Status = status;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }
        
        public Guid? EntityId { get; }

        public string? Filter { get; }
        
        public IReadOnlyCollection<ProcessStatus>? Status { get; }
        
        public int? Skip { get; }
        
        public int? Take { get; }
    }
}