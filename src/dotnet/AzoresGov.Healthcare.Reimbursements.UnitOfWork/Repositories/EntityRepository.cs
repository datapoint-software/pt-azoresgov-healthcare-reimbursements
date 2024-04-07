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

        public Task<Entity?> GetByUserIdAndEntityNaturesAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct) =>

            UnitOfWork.UserEntities
                .Where(ue => ue.UserId == userId)
                .Where(ue => entityNatures.Contains(ue.Entity.Nature))
                .Select(ue => ue.Entity)
                .FirstOrDefaultAsync(ct);

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
    }
}
