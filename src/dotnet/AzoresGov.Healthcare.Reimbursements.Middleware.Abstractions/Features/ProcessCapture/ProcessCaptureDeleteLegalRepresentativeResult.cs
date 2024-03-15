using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureDeleteLegalRepresentativeResult
    {
        public ProcessCaptureDeleteLegalRepresentativeResult(Guid processRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
    }
}