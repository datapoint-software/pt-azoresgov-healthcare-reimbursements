using AzoresGov.Healthcare.Reimbursements.Enumerations;
using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class Process : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Entity Entity { get; set; } = default!;

        public long EntityId { get; } = default!;

        public Patient Patient { get; set; } = default!;

        public long PatientId { get; } = default!;

        public string Number { get; set; } = default!;

        public ProcessStatus Status { get; set; } = default!;

        public DateTimeOffset Creation { get; set; } = default!;
    }
}
