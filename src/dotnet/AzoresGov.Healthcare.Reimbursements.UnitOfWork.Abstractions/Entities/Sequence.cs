using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class Sequence : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int NextValue { get; set; } = default!;
    }
}
