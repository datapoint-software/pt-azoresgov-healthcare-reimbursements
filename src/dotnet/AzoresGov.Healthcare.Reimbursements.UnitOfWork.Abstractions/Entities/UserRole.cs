using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class UserRole : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public User User { get; set; } = default!;

        public long UserId { get; } = default!;

        public Role Role { get; set; } = default!;

        public long RoleId { get; } = default!;

        public bool Granted { get; set; } = default!;
    }
}
