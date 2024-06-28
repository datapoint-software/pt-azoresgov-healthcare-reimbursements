using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSubmitResult
    {
        public MainProcessCaptureFeatureLegalRepresentativeSubmitResult(Guid processRowVersionId, Guid patientRowVersionId, Guid? legalRepresentativeId, Guid legalRepresentativeRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            LegalRepresentativeId = legalRepresentativeId;
            LegalRepresentativeRowVersionId = legalRepresentativeRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public Guid? LegalRepresentativeId { get; }

        public Guid LegalRepresentativeRowVersionId { get; }
    }
}