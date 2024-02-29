using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Authorization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public sealed class AuthorizationManager : IAuthorizationManager
    {
        private static readonly Guid ProcessCreation = new Guid("1d20e2d8-f067-4a25-a62a-278ef7d16950");
        
        private readonly IAuthorizationWorker _authorizationWorker;

        private readonly IPermissionRepository _permissions;

        public AuthorizationManager(IAuthorizationWorker authorizationWorker, IPermissionRepository permissions)
        {
            _authorizationWorker = authorizationWorker;
            _permissions = permissions;
        }

        public Task<bool> IsUserPermissionGrantedAsync(User user, Permission permission, CancellationToken ct) =>

            _authorizationWorker.IsUserPermissionGrantedAsync(
                user.Id,
                permission.Id,
                ct);

        public Task<bool> IsUserEntityPermissionGrantedAsync(User user, Permission permission, CancellationToken ct) =>

            _authorizationWorker.IsUserEntityPermissionGrantedAsync(
                user.Id,
                permission.Id,
                ct);

        public Task<bool> IsUserEntityPermissionGrantedAsync(User user, Entity entity, Permission permission, CancellationToken ct) =>

            _authorizationWorker.IsUserEntityPermissionGrantedAsync(
                user.Id,
                entity.Id,
                permission.Id,
                ct);

        public async Task<bool> IsProcessCreationPermissionGrantedAsync(User user, CancellationToken ct) =>

            await IsUserEntityPermissionGrantedAsync(
                user,
                await _permissions.GetByPublicIdOrThrowInvalidOperationException(
                    ProcessCreation,
                    ct),
                ct);

        public async Task<bool> IsProcessCreationPermissionGrantedAsync(User user, Entity entity, CancellationToken ct) =>

            await IsUserEntityPermissionGrantedAsync(
                user,
                entity,
                await _permissions.GetByPublicIdOrThrowInvalidOperationException(
                    ProcessCreation,
                    ct),
                ct);
    }
}