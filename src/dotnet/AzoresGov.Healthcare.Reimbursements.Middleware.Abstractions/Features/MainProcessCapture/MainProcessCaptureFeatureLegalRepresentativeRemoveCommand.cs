using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeRemoveCommand : Command<MainProcessCaptureFeatureLegalRepresentativeRemoveResult>
    {
        public MainProcessCaptureFeatureLegalRepresentativeRemoveCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid patientRowVersionId, Guid legalRepresentativeRowVersionId)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            PatientRowVersionId = patientRowVersionId;
            LegalRepresentativeRowVersionId = legalRepresentativeRowVersionId;
        }

        public Guid UserId { get; }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientRowVersionId { get; }

        public Guid LegalRepresentativeRowVersionId { get; }
    }
}
