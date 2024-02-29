using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        Task<UserPermission?> GetByUserIdAndPermissionIdAsync(long userId, long permissionId, CancellationToken ct);
    }
}