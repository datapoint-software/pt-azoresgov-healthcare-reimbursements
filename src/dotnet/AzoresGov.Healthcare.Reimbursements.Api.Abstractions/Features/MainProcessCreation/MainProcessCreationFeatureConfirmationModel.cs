using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureConfirmationModel
    {
        public MainProcessCreationFeatureConfirmationModel(Guid entityId, Guid entityRowVersionId, Guid patientId, Guid patientRowVersionId)
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
