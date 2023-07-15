using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEntityRoleRepository : IRepository<UserEntityRoleEntity>
    {
        Task<IEnumerable<UserEntityRoleEntity>> GetAllByUserIdAndEntityIdAsync(long userId, IEnumerable<long> entityId, CancellationToken ct);
    }
}
