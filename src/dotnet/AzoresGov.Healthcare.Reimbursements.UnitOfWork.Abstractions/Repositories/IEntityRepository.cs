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
        Task<IReadOnlyCollection<Entity>> GetAllByIdAsync(IReadOnlyCollection<long> id, CancellationToken ct);

        Task<IReadOnlyCollection<Entity>> GetAllByUserIdAndNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> nature,
            CancellationToken ct);

        Task<IReadOnlyCollection<Entity>> GetAllByUserSearchCriteriaAsync(
            long userId, 
            string? filter, 
            IReadOnlyCollection<EntityNature> nature, 
            int skip, 
            int take, 
            CancellationToken ct);

        Task<IReadOnlyCollection<string>> GetAllParentEntityCodeByEntityIdAsync(long entityId, CancellationToken ct);

        Task<Entity?> GetParentEntityByEntityIdAsync(
            long entityId, 
            int level,
            CancellationToken ct);
        
        Task<int> GetCountByUserSearchCriteriaAsync(
            long userId, 
            string? filter, 
            IReadOnlyCollection<EntityNature> nature, 
            CancellationToken ct);

        Task<Entity> GetSingleByUserIdAndNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> nature,
            CancellationToken ct);
    }
}