using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityRoleRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserEntityRoleEntity>, IUserEntityRoleRepository
    {
        public UserEntityRoleRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<long>> GetAllRoleIdByUserIdAndEntityIdAsync(long userId, IReadOnlyCollection<long> entityId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId && entityId.Contains(e.EntityId))
            .Select(e => e.RoleId)
            .Distinct()
            .ToListAsync(ct);
    }
}
