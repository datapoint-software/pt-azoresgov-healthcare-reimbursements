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
    public sealed class EntityParentEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, EntityParentEntity>, IEntityParentEntityRepository
    {
        public EntityParentEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<EntityParentEntity>> GetAllByEntityIdAndParentEntityNatureAsync(
            long entityId,
            EntityNature parentEntityNature,
            CancellationToken ct) =>

            await Entities
                .Where(epe => epe.EntityId == entityId)
                .Where(epe => epe.ParentEntity.Nature == parentEntityNature)
                .ToListAsync(ct);
    }
}
