using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsQuery : Query<ProcessCreationFeatureOptionsResult>
    {
        public ProcessCreationFeatureOptionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
