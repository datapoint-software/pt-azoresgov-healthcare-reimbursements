using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Logical
{
    public sealed class UserEntityPermissionGrantLogicalEntity
    {
        public long UserId { get; init; }

        public long EntityId { get; init; }

        public long PermissionId { get; init; }

        public bool Granted { get; init; }
    }
}
