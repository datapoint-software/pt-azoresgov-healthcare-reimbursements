using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserRoleRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<UserRole>> GetAllByUserIdAsync(long userId, CancellationToken ct) =>

            await Entities.Where(e => e.UserId == userId).ToListAsync(ct);
    }
}
