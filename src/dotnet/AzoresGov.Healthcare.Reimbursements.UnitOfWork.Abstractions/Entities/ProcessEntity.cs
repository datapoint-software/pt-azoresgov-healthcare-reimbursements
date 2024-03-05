using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class ProcessEntity : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Process Process { get; set; } = default!;

        public long ProcessId { get; } = default!;

        public Entity Entity { get; set; } = default!;

        public long EntityId { get; } = default!;
    }
}