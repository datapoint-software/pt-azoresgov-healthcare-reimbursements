using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class RolePermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, RolePermissionEntity>, IRolePermissionRepository
    {
        public RolePermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> AnyByRoleIdAndPermissionIdAsync(IEnumerable<long> roleId, long permissionId, CancellationToken ct) => await Entities

            .Where(e => roleId.Contains(e.RoleId) && e.PermissionId == permissionId)
            .AnyAsync(ct);

        public async Task<IEnumerable<RolePermissionEntity>> GetAllByRoleIdAsync(IEnumerable<long> roleId, CancellationToken ct) => await Entities

            .Where(e => roleId.Contains(e.RoleId))
            .ToListAsync(ct);
    }
}
