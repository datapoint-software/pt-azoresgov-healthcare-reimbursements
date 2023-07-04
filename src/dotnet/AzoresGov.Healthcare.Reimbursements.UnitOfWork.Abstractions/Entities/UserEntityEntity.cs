using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class UserEntityEntity : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public UserEntity User { get; set;} = default!;

        public long UserId { get; set; } = default!;

        public EntityEntity Entity { get; set; } = default!;

        public long EntityId { get; set; } = default!;
    }
}
