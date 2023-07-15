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

        public async Task<IEnumerable<UserEntityPermissionEntity>> GetAllByUserIdAndEntityIdAsync(long userId, IEnumerable<long> entityId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId && entityId.Contains(e.EntityId))
            .ToListAsync(ct);
    }
}
