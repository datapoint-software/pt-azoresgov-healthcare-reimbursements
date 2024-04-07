using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IEntityRepository : IRepository<Entity>
    {
        Task<Entity?> GetByUserIdAndEntityNaturesAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct);

        Task<int> GetCountByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct);

        Task<Entity?> GetParentEntityByEntityIdAndParentEntityNatureAsync(
            long entityId,
            EntityNature parentEntityNature,
            CancellationToken ct);
    }
}
