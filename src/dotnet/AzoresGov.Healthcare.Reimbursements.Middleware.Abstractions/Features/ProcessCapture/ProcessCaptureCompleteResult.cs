using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureCompleteResult
    {
        public ProcessCaptureCompleteResult(Guid processRowVersionId, ProcessStatus status)
        {
            ProcessRowVersionId = processRowVersionId;
            Status = status;
        }

        public Guid ProcessRowVersionId { get; }

        public ProcessStatus Status { get; }
    }
}