using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeaturePatientSubmitResult
    {
        public MainProcessCaptureFeaturePatientSubmitResult(Guid processRowVersionId, Guid patientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid PatientRowVersionId { get; }
    }
}