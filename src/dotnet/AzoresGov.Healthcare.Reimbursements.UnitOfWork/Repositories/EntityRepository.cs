using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class EntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Entity>, IEntityRepository
    {
        public EntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<Entity>> GetAllByIdAsync(
            IReadOnlyCollection<long> entityIds,
            CancellationToken ct)
        {
            return await Entities
                .Where(e => entityIds.Contains(e.Id))
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<Entity>> GetAllByUserIdAsync(
            long userId,
            CancellationToken ct)
        {
            return await UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Select(ue => ue.Entity)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<Entity>> GetAllByUserSearchCriteriaAsync(
            long userId,
            string filter,
            IReadOnlyCollection<EntityNature> entityNatures,
            int skip,
            int take,
            CancellationToken ct)
        {
            return await GetUserSearchCriteriaQueryable(userId, filter, entityNatures)
                .OrderBy(e => e.Name)
                    .ThenBy(e => e.Code)
                        .ThenBy(e => e.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        public Task<Entity?> GetByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct)
        {
            return UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Where(ue => entityNatures.Contains(ue.Entity.Nature))
                .Select(ue => ue.Entity)
                .FirstOrDefaultAsync(ct);
        }

        public Task<int> GetCountByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct)
        {
            return UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Where(ue => entityNatures.Contains(ue.Entity.Nature))
                .CountAsync(ct);
        }

        public Task<int> GetCountByUserSearchCriteriaAsync(
            long userId,
            string filter,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct)
        {
            return GetUserSearchCriteriaQueryable(userId, filter, entityNatures)
                .CountAsync(ct);
        }

        private IQueryable<Entity> GetUserSearchCriteriaQueryable(
            long userId,
            string filter,
            IReadOnlyCollection<EntityNature> entityNatures)
        {
            var filterExpression = $"%{
                string.Join('%', filter.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            }%";

            return UnitOfWork.UserEntities
                .Include(ue => ue.Entity)
                .Where(ue => ue.UserId == userId)
                .Where(ue => entityNatures.Contains(ue.Entity.Nature))
                .Where(ue => EF.Functions.Like(ue.Entity.Code, filterExpression) ||
                    EF.Functions.Like(ue.Entity.Name, filterExpression))
                .Select(ue => ue.Entity);
        }
    }
}
