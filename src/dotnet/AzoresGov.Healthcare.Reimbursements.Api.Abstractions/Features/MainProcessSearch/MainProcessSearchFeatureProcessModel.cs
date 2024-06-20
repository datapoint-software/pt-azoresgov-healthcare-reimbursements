using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessModel
    {
        public MainProcessSearchFeatureProcessModel(Guid id, Guid rowVersionId, Guid entityId, Guid patientId, string number, ProcessStatus status, DateTimeOffset creation)
        {
            Id = id;
            RowVersionId = rowVersionId;
            EntityId = entityId;
            PatientId = patientId;
            Number = number;
            Status = status;
            Creation = creation;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public Guid EntityId { get; }

        public Guid PatientId { get; }

        public string Number { get; }

        public ProcessStatus Status { get; }

        public DateTimeOffset Creation { get; }
    }
}
