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
        Task<IReadOnlyCollection<Entity>> GetAllByIdAsync(
            IReadOnlyCollection<long> entityId,
            CancellationToken ct);

        Task<IReadOnlyCollection<Entity>> GetAllBySearchCriteriaAsync(
            long userId,
            string? filter,
            IReadOnlyCollection<EntityNature>? entityNatures,
            int? skip,
            int? take,
            CancellationToken ct);

        Task<Entity?> GetByUserIdAndEntityNaturesAsync(
            long userId,
            IReadOnlyCollection<EntityNature> entityNatures,
            CancellationToken ct);

        Task<int> GetCountBySearchCriteriaAsync(
            long userId,
            string? filter,
            IReadOnlyCollection<EntityNature>? entityNatures,
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
