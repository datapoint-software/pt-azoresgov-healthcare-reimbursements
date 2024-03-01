using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IEntityParentEntityRepository : IRepository<EntityParentEntity>
    {
        Task<IReadOnlyDictionary<long, long>> GetParentEntityIdByEntityIdAsync(
            IReadOnlyCollection<long> entityId,
            int level,
            CancellationToken ct);
    }
}