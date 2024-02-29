using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityPermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserEntityPermission>, IUserEntityPermissionRepository
    {
        public UserEntityPermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserEntityPermission?> GetByUserIdAndEntityIdAndPermissionIdAsync(long userId, long entityId, long permissionId, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(
                e => e.UserId == userId && 
                e.EntityId == entityId && 
                e.PermissionId == permissionId, 
                ct);
    }
}