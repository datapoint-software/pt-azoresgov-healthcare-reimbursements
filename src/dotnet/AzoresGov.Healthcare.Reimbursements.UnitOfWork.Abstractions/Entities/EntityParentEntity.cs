using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class EntityParentEntity : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public EntityEntity Entity { get; set; } = default!;

        public long EntityId { get; set; } = default!;

        public EntityEntity ParentEntity { get; set; } = default!;

        public long ParentEntityId { get; set; } = default!;

        public int Level { get; set; } = default!;
    }
}
