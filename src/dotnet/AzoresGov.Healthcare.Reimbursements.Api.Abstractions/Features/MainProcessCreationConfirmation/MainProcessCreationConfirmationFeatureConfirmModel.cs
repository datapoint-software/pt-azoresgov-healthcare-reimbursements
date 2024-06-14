using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationConfirmation
{
    public sealed class MainProcessCreationConfirmationFeatureConfirmModel
    {
        public MainProcessCreationConfirmationFeatureConfirmModel(Guid entityId, Guid entityRowVersionId, Guid patientId, Guid patientRowVersionId)
        {
            EntityId = entityId;
            EntityRowVersionId = entityRowVersionId;
            PatientId = patientId;
            PatientRowVersionId = patientRowVersionId;
        }

        public Guid EntityId { get; }

        public Guid EntityRowVersionId { get; }

        public Guid PatientId { get; }

        public Guid PatientRowVersionId { get; }
    }
}
