using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureCompleteResultModel
    {
        public ProcessCaptureCompleteResultModel(Guid processRowVersionId, ProcessStatus status)
        {
            ProcessRowVersionId = processRowVersionId;
            Status = status;
        }

        public Guid ProcessRowVersionId { get; }

        public ProcessStatus Status { get; }
    }
}
