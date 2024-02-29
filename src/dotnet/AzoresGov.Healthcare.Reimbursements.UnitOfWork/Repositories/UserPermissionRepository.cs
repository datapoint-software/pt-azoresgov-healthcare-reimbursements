using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserPermissionRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserPermission>, IUserPermissionRepository
    {
        public UserPermissionRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<UserPermission?> GetByUserIdAndPermissionIdAsync(long userId, long permissionId, CancellationToken ct) =>
            
            Entities.FirstOrDefaultAsync(e => e.UserId == userId && e.PermissionId == permissionId, ct);
    }
}