using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IEntityParentEntityRepository : IRepository<EntityParentEntity>
    {
        Task<IReadOnlyCollection<EntityParentEntity>> GetAllByEntityIdAndParentEntityNatureAsync(
            long entityId,
            EntityNature parentEntityNature,
            CancellationToken ct);

        Task<IReadOnlyCollection<EntityParentEntity>> GetAllByEntityIdAndParentEntityNatureAsync(
            IReadOnlyCollection<long> entityId,
            EntityNature parentEntityNature,
            CancellationToken ct);
    }
}
