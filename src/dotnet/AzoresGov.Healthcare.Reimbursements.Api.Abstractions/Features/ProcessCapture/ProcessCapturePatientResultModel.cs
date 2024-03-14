using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCapturePatientResultModel
    {
        public ProcessCapturePatientResultModel(Guid processRowVersionId, Guid processPatientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientRowVersionId = processPatientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientRowVersionId { get; }
    }
}