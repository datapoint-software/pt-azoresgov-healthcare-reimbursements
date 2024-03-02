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
    public sealed class EntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Entity>, IEntityRepository
    {
        public EntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<Entity>> GetAllByIdAsync(IReadOnlyCollection<long> id, CancellationToken ct) =>

            await Entities
                .Where(e => id.Contains(e.Id))
                .ToListAsync(ct);

        public async Task<IReadOnlyCollection<Entity>> GetAllByUserSearchCriteriaAsync(
            long userId,
            string? filter,
            IReadOnlyCollection<EntityNature> nature,
            int skip,
            int take,
            CancellationToken ct)
        {
            var queryable = GetQueryableByUserSearchCriteriaAsync(
                userId,
                filter,
                nature);

            return await queryable
                .OrderBy(e => e.Name)
                .Skip(skip)
                .Take(take)
                .ToListAsync(ct);
        }

        public async Task<int> GetCountByUserSearchCriteriaAsync(
            long userId,
            string? filter,
            IReadOnlyCollection<EntityNature> nature,
            CancellationToken ct)
        {
            var queryable = GetQueryableByUserSearchCriteriaAsync(
                userId,
                filter,
                nature);

            return await queryable.CountAsync(ct);
        }

        public Task<Entity?> GetParentEntityByEntityIdAsync(
            long entityId,
            int level,
            CancellationToken ct) =>

            UnitOfWork.EntityParentEntities
                .Where(epe => epe.EntityId == entityId)
                .Where(epe => epe.Level == level)
                .Select(epe => epe.Entity)
                .FirstOrDefaultAsync(ct);

        public async Task<Entity> GetSingleByUserIdAndNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> nature,
            CancellationToken ct) =>

            await UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId && nature.Contains(ue.Entity.Nature))
                .Select(ue => ue.Entity)
                .SingleAsync(ct);

        private IQueryable<Entity> GetQueryableByUserSearchCriteriaAsync(
            long userId,
            string? filter,
            IReadOnlyCollection<EntityNature> nature)
        {
            var queryable = UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Where(ue => nature.Contains(ue.Entity.Nature))
                .Select(ue => ue.Entity)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                var filterMatchExpression = $"%{filter.Replace(' ', '%')}%";

                queryable = queryable
                    .Where(e => EF.Functions.Like(e.Name, filterMatchExpression) ||
                                EF.Functions.Like(e.Code, filterMatchExpression));
            }

            return queryable;
        }
    }
}