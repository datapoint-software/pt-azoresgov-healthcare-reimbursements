using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public sealed class EntityRepository : EntityFrameworkCoreRepository<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext, EntityEntity>, IEntityRepository
    {
        public EntityRepository(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<EntityEntity>> GetAllByIdAsync(IReadOnlyCollection<long> id, CancellationToken ct) => await Entities

            .Where(e => id.Contains(e.Id))
            .ToListAsync(ct);

        public Task<EntityEntity?> GetByCodeAsync(string code, CancellationToken ct) =>

            Entities.FirstOrDefaultAsync(e => e.Code == code, ct);
    }
}
