using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class UserEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, UserEntity>, IUserEntityRepository
    {
        public UserEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<bool> AnyByUserIdAndEntityIdAsync(
            long userId,
            long entityId,
            CancellationToken ct) =>

            Entities.AnyAsync(e => e.UserId == userId && e.EntityId == entityId, ct);

        public Task<int> CountByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNature,
            CancellationToken ct) =>

            Entities.CountAsync(e => e.UserId == userId && 
                                     entityNature.Contains(e.Entity.Nature), 
                ct);

        public async Task<IReadOnlyCollection<UserEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct) =>

            await Entities
                .Where(e => e.UserId == userId)
                .ToListAsync(ct);

        public async Task<IReadOnlyCollection<long>> GetAllEntityIdByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> nature,
            CancellationToken ct) =>

            await Entities
                .Where(e => e.UserId == userId)
                .Where(e => nature.Contains(e.Entity.Nature))
                .Select(e => e.EntityId)
                .ToListAsync(ct);
    }
}