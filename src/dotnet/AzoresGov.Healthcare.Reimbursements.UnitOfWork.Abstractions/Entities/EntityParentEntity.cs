using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class EntityParentEntity : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Entity Entity { get; set; } = default!;

        public long EntityId { get; } = default!;

        public Entity ParentEntity { get; set; } = default!;

        public long ParentEntityId { get; } = default!;

        public int Level { get; set; } = default!;
    }
}
