using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSelectCommand : Command<MainProcessCaptureFeatureLegalRepresentativeSelectResult>
    {
        public MainProcessCaptureFeatureLegalRepresentativeSelectCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid patientRowVersionId, string taxNumber)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            TaxNumber = taxNumber;
        }

        public Guid UserId { get; }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public string TaxNumber { get; }
    }
}
