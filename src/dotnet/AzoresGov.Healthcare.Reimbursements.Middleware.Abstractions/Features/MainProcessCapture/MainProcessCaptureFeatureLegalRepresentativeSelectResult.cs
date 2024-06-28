using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSelectResult
    {
        public MainProcessCaptureFeatureLegalRepresentativeSelectResult(Guid processRowVersionId, Guid patientRowVersionId, MainProcessCaptureFeatureLegalRepresentative? legalRepresentative)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            LegalRepresentative = legalRepresentative;
        }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public MainProcessCaptureFeatureLegalRepresentative? LegalRepresentative { get; }
    }
}