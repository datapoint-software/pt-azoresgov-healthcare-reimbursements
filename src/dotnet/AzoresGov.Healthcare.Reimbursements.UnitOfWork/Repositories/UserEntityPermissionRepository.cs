using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityPermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserEntityPermissionEntity>, IUserEntityPermissionRepository
    {
        public UserEntityPermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<int> CountGrantedByUserIdAndPermissionIdAsync(long userId, long permissionId, CancellationToken ct) => Entities

            .CountAsync(e => e.UserId == userId && e.PermissionId == permissionId && e.Granted, ct);

        public async Task<IEnumerable<UserEntityPermissionEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId)
            .ToListAsync(ct);

        public async Task<IEnumerable<UserEntityPermissionEntity>> GetAllByUserIdAndEntityIdAsync(long userId, IEnumerable<long> entityId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId && entityId.Contains(e.EntityId))
            .ToListAsync(ct);

        public async Task<IEnumerable<UserEntityPermissionEntity>> GetAllByUserIdAndPermissionIdAsync(long userId, long permissionId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId && e.PermissionId == permissionId)
            .ToListAsync(ct);
    }
}
