using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSelectModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeSelectModel(Guid processId, Guid processRowVersionId, Guid patientRowVersionId, string taxNumber)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            TaxNumber = taxNumber;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public string TaxNumber { get; }
    }
}
