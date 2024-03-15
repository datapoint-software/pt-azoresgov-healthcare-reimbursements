using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureDeleteLegalRepresentativeResultModel
    {
        public ProcessCaptureDeleteLegalRepresentativeResultModel(Guid processRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
    }
}