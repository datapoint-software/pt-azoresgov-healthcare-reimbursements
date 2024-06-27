using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureSubmitPatientResult
    {
        public MainProcessCaptureFeatureSubmitPatientResult(Guid processRowVersionId, Guid patientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid PatientRowVersionId { get; }
    }
}