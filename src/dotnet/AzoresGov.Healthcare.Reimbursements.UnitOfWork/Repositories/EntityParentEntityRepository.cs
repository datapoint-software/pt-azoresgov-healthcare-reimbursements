using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class EntityParentEntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, EntityParentEntity>, IEntityParentEntityRepository
    {
        public EntityParentEntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IReadOnlyCollection<EntityParentEntity>> GetAllByEntityIdAndLevelAsync(IReadOnlyCollection<long> entityId, int level, CancellationToken ct) =>

            await Entities
                .Where(e => entityId.Contains(e.EntityId))
                .Where(e => e.Level == level)
                .ToListAsync(ct);
    }
}