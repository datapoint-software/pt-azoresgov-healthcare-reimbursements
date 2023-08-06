using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsQuery : Query<ProcessCreationOptionsResult>
    {
        public ProcessCreationOptionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
