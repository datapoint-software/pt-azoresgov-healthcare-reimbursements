using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class PermissionRepositoryHelper
    {
        internal static async Task<PermissionEntity> GetByNameOrThrowInvalidOperationExceptionAsync(this IPermissionRepository permissions, string permissionName, CancellationToken ct)
        {
            var permission = await permissions.GetByNameAsync(permissionName, ct);

            if (permission == null)
            {
                throw new InvalidOperationException("A permission was not found matching the given name.")
                    .WithCode("GOIQXN");
            }

            return permission;
        }
    }
}
