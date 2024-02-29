using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public interface IAuthorizationManager
    {
        Task<bool> IsUserPermissionGrantedAsync(User user, Permission permission, CancellationToken ct);
        
        Task<bool> IsUserEntityPermissionGrantedAsync(User user, Permission permission, CancellationToken ct);

        Task<bool> IsUserEntityPermissionGrantedAsync(User user, Entity entity, Permission permission, CancellationToken ct);

        Task<bool> IsProcessCreationPermissionGrantedAsync(User user, CancellationToken ct);

        Task<bool> IsProcessCreationPermissionGrantedAsync(User user, Entity entity, CancellationToken ct);
    }
}