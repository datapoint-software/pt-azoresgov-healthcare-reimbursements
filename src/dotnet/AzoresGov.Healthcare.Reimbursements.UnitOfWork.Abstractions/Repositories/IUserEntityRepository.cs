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
        Task<int> CountByUserIdAndEntityNatureAsync(
            long userId, 
            IReadOnlyCollection<EntityNature> entityNature, 
            CancellationToken ct);
        
        Task<IReadOnlyCollection<UserEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct);
    }
}