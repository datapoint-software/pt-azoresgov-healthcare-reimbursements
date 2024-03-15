using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureWriteLegalRepresentativeResultModel
    {
        public ProcessCaptureWriteLegalRepresentativeResultModel(Guid processRowVersionId, Guid processPatientLegalRepresentativeRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientLegalRepresentativeRowVersionId = processPatientLegalRepresentativeRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientLegalRepresentativeRowVersionId { get; }
    }
}