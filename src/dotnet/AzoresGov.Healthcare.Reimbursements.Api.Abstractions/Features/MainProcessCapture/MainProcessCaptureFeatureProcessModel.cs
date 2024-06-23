using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureProcessModel
    {
        public MainProcessCaptureFeatureProcessModel(Guid id, Guid rowVersionId, Guid entityId, string number, DateTimeOffset creation)
        {
            Id = id;
            RowVersionId = rowVersionId;
            EntityId = entityId;
            Number = number;
            Creation = creation;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public Guid EntityId { get; }

        public string Number { get; }

        public DateTimeOffset Creation { get; }
    }
}
