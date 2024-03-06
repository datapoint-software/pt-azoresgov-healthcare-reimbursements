using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCapturePatientResultModel
    {
        public ProcessCapturePatientResultModel(Guid patientRowVersionId, Guid processRowVersionId)
        {
            PatientRowVersionId = patientRowVersionId;
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid PatientRowVersionId { get; }

        public Guid ProcessRowVersionId { get; }
    }
}