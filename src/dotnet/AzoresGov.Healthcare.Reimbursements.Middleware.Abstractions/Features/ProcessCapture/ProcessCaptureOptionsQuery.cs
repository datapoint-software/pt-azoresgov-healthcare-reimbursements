using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCaptureOptionsQuery : Query<ProcessCaptureOptionsResult>
    {
        public ProcessCaptureOptionsQuery(Guid userId, Guid processId)
        {
            UserId = userId;
            ProcessId = processId;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
    }
}