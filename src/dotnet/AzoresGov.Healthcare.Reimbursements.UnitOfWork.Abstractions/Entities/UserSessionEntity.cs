using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class UserSessionEntity : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public UserEntity User { get; set; } = default!;

        public string Agent { get; set; } = default!;

        public string NetworkAddress { get; set; } = default!;

        public DateTimeOffset Creation { get; set; } = default!;

        public DateTimeOffset? Expiration { get; set; } = default!;

        public DateTimeOffset LastSeen { get; set; } = default!;
    }
}
