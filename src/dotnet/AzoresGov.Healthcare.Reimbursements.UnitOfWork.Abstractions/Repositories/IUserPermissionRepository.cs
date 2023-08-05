using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermissionEntity>
    {
        Task<IEnumerable<UserPermissionEntity>> GetAllByUserIdAsync(long userId, CancellationToken ct);

        Task<UserPermissionEntity?> GetByUserIdAndPermissionIdAsync(long userId, long permissionId, CancellationToken ct);
    }
}
