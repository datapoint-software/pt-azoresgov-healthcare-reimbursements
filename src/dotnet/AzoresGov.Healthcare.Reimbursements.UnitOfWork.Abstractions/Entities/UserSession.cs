using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class UserSession : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public User User { get; set; } = default!;

        public long UserId { get; } = default!;

        public string UserAgent { get; set; } = default!;

        public string RemoteAddress { get; set; } = default!;

        public DateTimeOffset Creation { get; set; } = default!;

        public DateTimeOffset? Expiration { get; set; } = default!;

        public DateTimeOffset LastSeen { get; set; } = default!;
    }
}
