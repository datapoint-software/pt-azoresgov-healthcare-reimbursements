using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class RolePermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<RolePermission>> GetAllByRoleIdAndPermissionIdAsync(IReadOnlyCollection<long> roleId, long permissionId, CancellationToken ct) =>

            await Entities
                .Where(e => roleId.Contains(e.RoleId))
                .Where(e => e.PermissionId == permissionId)
                .ToListAsync(ct);
    }
}