using Datapoint.UnitOfWork;
using System;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities
{
    public sealed class RolePermission : IEntity
    {
        public long Id { get; set; } = default!;

        public Guid PublicId { get; set; } = default!;

        public Guid RowVersionId { get; set; } = default!;

        public Role Role { get; set; } = default!;

        public long RoleId { get; } = default!;

        public Permission Permission { get; set; } = default!;

        public long PermissionId { get; } = default!;

        public bool Granted { get; set; } = default!;
    }
}
