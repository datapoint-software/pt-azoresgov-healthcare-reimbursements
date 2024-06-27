using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeaturePatientSubmitResultModel
    {
        public MainProcessCaptureFeaturePatientSubmitResultModel(Guid processRowVersionId, Guid patientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }
    }
}