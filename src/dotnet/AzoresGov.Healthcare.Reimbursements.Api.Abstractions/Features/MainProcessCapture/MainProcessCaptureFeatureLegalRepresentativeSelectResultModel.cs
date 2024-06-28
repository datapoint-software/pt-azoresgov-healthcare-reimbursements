using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSelectResultModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeSelectResultModel(Guid processRowVersionId, Guid patientRowVersionId, MainProcessCaptureFeatureLegalRepresentativeModel? legalRepresentative)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            LegalRepresentative = legalRepresentative;
        }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public MainProcessCaptureFeatureLegalRepresentativeModel? LegalRepresentative { get; }
    }
}