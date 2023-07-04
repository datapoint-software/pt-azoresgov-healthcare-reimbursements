using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEntityRoleRepository : IRepository<UserEntityRoleEntity>
    {
        Task<IReadOnlyCollection<long>> GetAllRoleIdByUserIdAndEntityIdAsync(long userId, IReadOnlyCollection<long> entityId, CancellationToken ct);
    }
}
