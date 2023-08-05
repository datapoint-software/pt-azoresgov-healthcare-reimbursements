using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, UserEntityEntity>, IUserEntityRepository
    {
        public UserEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<int> CountByUserIdAsync(long userId, CancellationToken ct) => Entities

            .CountAsync(e => e.UserId == userId, ct);

        public async Task<IReadOnlyCollection<UserEntityEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct) => await Entities

            .Where(e => e.UserId == userId)
            .ToListAsync(ct);
    }
}
