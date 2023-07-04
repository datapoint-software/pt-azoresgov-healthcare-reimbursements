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

        public async Task<IReadOnlyCollection<RolePermissionEntity>> GetAllByRoleIdAsync(IReadOnlyCollection<long> rolesId, CancellationToken ct) => await Entities

            .Where(e => rolesId.Contains(e.RoleId))
            .ToListAsync(ct);
    }
}
