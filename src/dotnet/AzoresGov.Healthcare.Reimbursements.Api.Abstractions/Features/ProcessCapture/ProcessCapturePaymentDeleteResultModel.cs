using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentDeleteResultModel
    {
        public ProcessCapturePaymentDeleteResultModel(Guid processRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
    }
}