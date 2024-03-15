using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureWriteLegalRepresentativeResult
    {
        public ProcessCaptureWriteLegalRepresentativeResult(Guid processRowVersionId, Guid processPatientLegalRepresentativeRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientLegalRepresentativeRowVersionId = processPatientLegalRepresentativeRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientLegalRepresentativeRowVersionId { get; }
    }
}