using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel(Guid processRowVersionId, Guid patientRowVersionId, Guid? legalRepresentativeId, Guid legalRepresentativeRowVersionId)
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