using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserPermissionEntity>, IUserPermissionRepository
    {
        public UserPermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<UserPermissionEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId)
            .ToListAsync(ct);

        public Task<UserPermissionEntity?> GetByUserIdAndPermissionIdAsync(long userId, long permissionId, CancellationToken ct) => Entities

            .FirstOrDefaultAsync(
                e => e.UserId == userId && e.PermissionId == permissionId,
                ct);
    }
}
