using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        Task<IReadOnlyCollection<RolePermission>> GetAllByRoleIdAndPermissionIdAsync(IReadOnlyCollection<long> roleId, long permissionId, CancellationToken ct);
    }
}