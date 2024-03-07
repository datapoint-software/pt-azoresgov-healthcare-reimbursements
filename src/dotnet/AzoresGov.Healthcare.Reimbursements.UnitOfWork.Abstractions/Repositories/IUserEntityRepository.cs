using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEntityRepository : IRepository<UserEntity>
    {
        Task<bool> AnyByUserIdAndEntityIdAsync(
            long userId,
            long entityId,
            CancellationToken ct);
        
        Task<int> CountByUserIdAndEntityNatureAsync(
            long userId, 
            IReadOnlyCollection<EntityNature> entityNature, 
            CancellationToken ct);
        
        Task<IReadOnlyCollection<UserEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct);

        Task<IReadOnlyCollection<long>> GetAllEntityIdByUserIdAndEntityNatureAsync(
            long userId,
            IReadOnlyCollection<EntityNature> nature,
            CancellationToken ct);
    }
}