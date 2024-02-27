using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class UserPermission : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public User User { get; set; } = default!;

        public Permission Permission { get; set; } = default!;

        public bool Granted { get; set; } = default!;
    }
}
