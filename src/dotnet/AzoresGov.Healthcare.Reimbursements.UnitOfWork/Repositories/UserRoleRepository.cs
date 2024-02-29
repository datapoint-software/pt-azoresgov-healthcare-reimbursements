using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRoleRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<UserRole>> GetAllByUserIdAsync(long userId, CancellationToken ct) =>

            await Entities
                .Where(e => e.UserId == userId)
                .ToListAsync(ct);
    }
}