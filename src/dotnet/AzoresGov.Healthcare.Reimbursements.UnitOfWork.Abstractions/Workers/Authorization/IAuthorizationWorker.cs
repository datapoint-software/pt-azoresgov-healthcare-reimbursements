using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Authorization
{
    public interface IAuthorizationWorker
    {
        Task<bool> IsUserEntityPermissionGrantedAsync(long userId, long permissionId, CancellationToken ct);

        Task<bool> IsUserEntityPermissionGrantedAsync(long userId, long entityId, long permissionId, CancellationToken ct);

        Task<bool> IsUserPermissionGrantedAsync(long userId, long permissionId, CancellationToken ct);
    }
}