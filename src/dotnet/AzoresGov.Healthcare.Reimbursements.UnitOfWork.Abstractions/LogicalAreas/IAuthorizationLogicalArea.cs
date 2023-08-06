using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Logical;
using Datapoint.UnitOfWork;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.LogicalAreas
{
    public interface IAuthorizationLogicalArea : ILogicalArea
    {
        Task<int> CountUserEntityPermissionGrantsAsync(long permissionId, long userId, CancellationToken ct);

        Task<IReadOnlyCollection<UserPermissionGrantLogicalEntity>> GetAllUserPermissionGrantsByUserIdAsync(long userId, CancellationToken ct);

        Task<IReadOnlyCollection<UserEntityPermissionGrantLogicalEntity>> GetAllUserEntityPermissionGrantsByUserIdExceptWhenPermissionIdAsync(long userId, IEnumerable<long> permissionId, CancellationToken ct);

        Task<IReadOnlyCollection<UserEntityPermissionGrantLogicalEntity>> GetAllUserEntityPermissionGrantsByUserIdAsync(long userId, CancellationToken ct);

        Task<long> GetPermissionIdAsync(string permissionName, CancellationToken ct);
    }
}
