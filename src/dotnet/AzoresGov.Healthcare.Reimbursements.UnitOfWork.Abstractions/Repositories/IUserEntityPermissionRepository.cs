using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserEntityPermissionRepository : IRepository<UserEntityPermission>
    {
        Task<UserEntityPermission?> GetByUserIdAndEntityIdAndPermissionIdAsync(long userId, long entityId, long permissionId, CancellationToken ct);
    }
}