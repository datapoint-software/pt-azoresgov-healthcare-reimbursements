using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationPatientSearchQuery : Query<ProcessCreationPatientSearchResult>
    {
        public ProcessCreationPatientSearchQuery(Guid userId, Guid entityId, string? filter, int? skip, int? take)
        {
            UserId = userId;
            EntityId = entityId;
            Filter = filter;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }
        
        public Guid EntityId { get; }
        
        public string? Filter { get; }
        
        public int? Skip { get; }
        
        public int? Take { get; }
    }
}