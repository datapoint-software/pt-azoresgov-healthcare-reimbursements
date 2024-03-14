using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePatientResult
    {
        public ProcessCapturePatientResult(Guid processRowVersionId, Guid processPatientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientRowVersionId = processPatientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientRowVersionId { get; }
    }
}