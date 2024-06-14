using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class EntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, Entity>, IEntityRepository
    {
        public EntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<string>> GetAllParentEntityCodesByEntityIdWithOrderByLevelAsync(
            long entityId,
            CancellationToken ct)
        {
            return await UnitOfWork.EntityParentEntities
                .Where(epe => epe.EntityId == entityId)
                .OrderBy(epe => epe.Level)
                .Select(epe => epe.ParentEntity.Code)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyCollection<Entity>> GetAllByIdAsync(
            IReadOnlyCollection<long> entityId,
            CancellationToken ct) =>

            await Entities
                .Where(e => entityId.Contains(e.Id))
                .ToListAsync(ct);

        public async Task<IReadOnlyCollection<Entity>> GetAllBySearchCriteriaAsync(
            long userId, 
            string? filter, 
            IReadOnlyCollection<EntityNature>? entityNatures, 
            int? skip, 
            int? take, 
            CancellationToken ct)
        {
            var queryable = CreateQueryableBySearchCriteria(
                userId,
                filter,
                entityNatures);

            if (skip.HasValue)
                queryable = queryable.Skip(skip.Value);

            if (take.HasValue)
                queryable = queryable.Take(take.Value);

            return await queryable.ToListAsync(ct);
        }

        public Task<Entity?> GetByUserIdAndEntityNaturesAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct) =>

            UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Where(ue => entityNatures.Contains(ue.Entity.Nature))
                .Select(ue => ue.Entity)
                .FirstOrDefaultAsync(ct);
        public Task<int> GetCountBySearchCriteriaAsync(
            long userId, 
            string? filter, 
            IReadOnlyCollection<EntityNature>? entityNatures, 
            CancellationToken ct) =>

            CreateQueryableBySearchCriteria(
                userId, 
                filter, 
                entityNatures)
                .CountAsync(ct);
        
        public Task<int> GetCountByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct) =>

            UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Where(ue => entityNatures.Contains(ue.Entity.Nature))
                .CountAsync(ct);

        public Task<Entity?> GetParentEntityByEntityIdAndParentEntityNatureAsync(
            long entityId,
            EntityNature parentEntityNature,
            CancellationToken ct) =>

            UnitOfWork.EntityParentEntities
                .Where(epe => epe.EntityId == entityId)
                .Where(epe => epe.ParentEntity.Nature == parentEntityNature)
                .Select(epe => epe.ParentEntity)
                .FirstOrDefaultAsync(ct);

        private IQueryable<Entity> CreateQueryableBySearchCriteria(
            long userId,
            string? filter,
            IReadOnlyCollection<EntityNature>? entityNatures)
        {
            var queryable = UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId);

            if (!string.IsNullOrEmpty(filter))
            {
                var pattern = $"%{filter.Replace(' ', '%')}%";

                queryable = queryable.Where(ue =>
                    EF.Functions.Like(ue.Entity.Code, pattern) ||
                    EF.Functions.Like(ue.Entity.Name, pattern));
            }

            if (entityNatures is not null && entityNatures.Count > 0)
                queryable = queryable.Where(ue => entityNatures.Contains(ue.Entity.Nature));

            return queryable.Select(ue => ue.Entity);
        }
    }
}
