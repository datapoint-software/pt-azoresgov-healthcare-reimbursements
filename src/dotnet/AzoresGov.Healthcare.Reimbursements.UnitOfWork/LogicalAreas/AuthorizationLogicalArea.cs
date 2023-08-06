using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities.Logical;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.UnitOfWork.LogicalAreas
{
    public sealed class AuthorizationLogicalArea : EntityFrameworkCoreLogicalArea<HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext>, IAuthorizationLogicalArea
    {
        public AuthorizationLogicalArea(HealthcareReimbursementsUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<int> CountUserEntityPermissionGrantsAsync(long permissionId, long userId, CancellationToken ct) =>

            Context.UserEntityPermissionGrants
                .Where(uepg => uepg.PermissionId == permissionId)
                .Where(uepg => uepg.UserId == userId)
                .Where(uepg => uepg.Granted)
                .CountAsync(ct);

        public async Task<IReadOnlyCollection<UserEntityPermissionGrantLogicalEntity>> GetAllUserEntityPermissionGrantsByUserIdAsync(long userId, CancellationToken ct) =>

            await Context.UserEntityPermissionGrants
                .Where(uepg => uepg.UserId == userId)
                .ToListAsync(ct);

        public async Task<IReadOnlyCollection<UserEntityPermissionGrantLogicalEntity>> GetAllUserEntityPermissionGrantsByUserIdExceptWhenPermissionIdAsync(long userId, IEnumerable<long> permissionId, CancellationToken ct) =>

            await Context.UserEntityPermissionGrants
                .Where(uepg => uepg.UserId == userId)
                .Where(uepg => permissionId.Contains(uepg.PermissionId) == false)
                .ToListAsync(ct);

        public async Task<IReadOnlyCollection<UserPermissionGrantLogicalEntity>> GetAllUserPermissionGrantsByUserIdAsync(long userId, CancellationToken ct) =>

            await Context.UserPermissionGrants
                .Where(upg => upg.UserId == userId)
                .ToListAsync(ct);

        public async Task<long> GetPermissionIdAsync(string permissionName, CancellationToken ct) =>

            await Context.Permissions
                .Where(p => p.Name == permissionName)
                .AsNoTracking()
                .Select(p => p.Id)
                .SingleAsync(ct);
    }
}
