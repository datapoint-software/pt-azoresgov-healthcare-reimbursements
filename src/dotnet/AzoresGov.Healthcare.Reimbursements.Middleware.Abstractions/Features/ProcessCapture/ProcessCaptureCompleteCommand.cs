using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureCompleteCommand : Command<ProcessCaptureCompleteResult>
    {
        public ProcessCaptureCompleteCommand(Guid userId, Guid processId, Guid processRowVersionId)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid UserId { get; }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }
    }
}
