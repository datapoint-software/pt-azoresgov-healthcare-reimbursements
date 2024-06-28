using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeRemoveResultModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeRemoveResultModel(Guid processRowVersionId, Guid patientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }
    }
}