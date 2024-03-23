using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureCompleteModel
    {
        public ProcessCaptureCompleteModel(Guid processId, Guid processRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }
    }
}
