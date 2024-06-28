using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeRemoveModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeRemoveModel(Guid processId, Guid processRowVersionId, Guid patientRowVersionId, Guid legalRepresentativeRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            LegalRepresentativeRowVersionId = legalRepresentativeRowVersionId;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public Guid LegalRepresentativeRowVersionId { get; }
    }
}
