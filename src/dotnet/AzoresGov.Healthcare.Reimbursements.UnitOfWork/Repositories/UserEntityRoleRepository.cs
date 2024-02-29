using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityRoleRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserEntityRole>, IUserEntityRoleRepository
    {
        public UserEntityRoleRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<UserEntityRole>> GetAllByUserIdAndEntityIdAsync(long userId, long entityId, CancellationToken ct) =>

            await Entities
                .Where(e => e.UserId == userId && e.EntityId == entityId)
                .ToListAsync(ct);
    }
}