using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEntityRoleRepository : IRepository<UserEntityRole>
    {
        Task<IReadOnlyCollection<UserEntityRole>> GetAllByUserIdAndEntityIdAsync(long userId, long entityId, CancellationToken ct);
    }
}