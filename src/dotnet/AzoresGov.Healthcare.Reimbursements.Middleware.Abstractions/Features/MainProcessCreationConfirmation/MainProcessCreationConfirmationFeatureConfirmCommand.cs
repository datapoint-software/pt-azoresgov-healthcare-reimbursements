using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationConfirmation
{
    public sealed class MainProcessCreationConfirmationFeatureConfirmCommand : Command<MainProcessCreationConfirmationFeatureConfirmResult>
    {
        public MainProcessCreationConfirmationFeatureConfirmCommand(Guid userId, Guid entityId, Guid entityRowVersionId, Guid patientId, Guid patientRowVersionId)
        {
            UserId = userId;
            EntityId = entityId;
            EntityRowVersionId = entityRowVersionId;
            PatientId = patientId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid UserId { get; }

        public Guid EntityId { get; }

        public Guid EntityRowVersionId { get; }

        public Guid PatientId { get; }

        public Guid PatientRowVersionId { get; }
    }
}
