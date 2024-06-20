using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserEntity>, IUserEntityRepository
    {
        public UserEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<bool> AnyByUserIdAndEntityIdAsync(long userId, long entityId, CancellationToken ct) =>

            Entities.AnyAsync(ue => ue.UserId == userId && ue.EntityId == entityId, ct);

        public async Task<IReadOnlyCollection<long>> GetAllEntityIdByUserIdAsync(long userId, CancellationToken ct) =>

            await Entities
                .Where(ue => ue.UserId == userId)
                .Select(ue => ue.EntityId)
                .ToListAsync(ct);
    }
}
