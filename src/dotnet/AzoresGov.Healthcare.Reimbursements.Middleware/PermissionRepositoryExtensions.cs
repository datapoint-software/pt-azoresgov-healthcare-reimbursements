using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal static class PermissionRepositoryExtensions
    {
        private static readonly Guid ProcessCreation = new Guid("1d20e2d8-f067-4a25-a62a-278ef7d16950");
        
        private static async Task<Permission> GetByPublicIdOrThrowInvalidOperationExceptionAsync(this IPermissionRepository permissions, Guid publicId, CancellationToken ct)
        {
            var permission = await permissions.GetByPublicIdAsync(
                publicId,
                ct);

            if (permission is null)
            {
                throw new InvalidOperationException("Permission was not found.")
                    .WithErrorCode("PNTFND");
            }

            return permission;
        }

        internal static Task<Permission> GetProcessCreationOrThrowInvalidOperationExceptionAsync(this IPermissionRepository permissions, CancellationToken ct) =>

            GetByPublicIdOrThrowInvalidOperationExceptionAsync(
                permissions,
                ProcessCreation,
                ct);
    }
}