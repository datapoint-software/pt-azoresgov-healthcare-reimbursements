using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCapturePatientResult
    {
        public ProcessCapturePatientResult(Guid processRowVersionId, Guid patientRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid PatientRowVersionId { get; }
    }
}