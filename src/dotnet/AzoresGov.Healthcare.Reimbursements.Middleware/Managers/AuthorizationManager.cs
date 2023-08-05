using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.UnitOfWork;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Managers
{
    public sealed class AuthorizationManager
    {
        private static string CreateUserCacheKey(UserEntity user) =>
            $"authorization:u={user.Id}";

        private static string CreateUserPermissionCacheKey(PermissionEntity permission, UserEntity user) =>
            $"authorization:u={user.Id};pn={permission.Name}";

        private static string CreateUserEntityPermissionCacheKey(PermissionEntity permission, UserEntity user, EntityEntity entity) =>
            $"authorization:u={user.Id};e={entity.Id};pn={permission.Name}";

        private static string CreateUserEntityTransversalCacheKey(PermissionEntity permission, UserEntity user) =>
            $"authorization:u={user.Id};e=transversal;pn={permission.Name}";

        private static DistributedCacheEntryOptions DistributedCacheEntryOptions = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromHours(1)
        };

        private readonly IDistributedCache _distributedCache;

        private readonly ILogger<AuthorizationManager> _logger;

        private readonly IEntityRepository _entities;

        private readonly IPermissionRepository _permissions;

        private readonly IRolePermissionRepository _rolePermissions;

        private readonly IRoleRepository _roles;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserEntityPermissionRepository _userEntityPermissions;

        private readonly IUserEntityRoleRepository _userEntityRoles;

        private readonly IUserPermissionRepository _userPermissions;

        private readonly IUserRoleRepository _userRoles;

        public AuthorizationManager(
            IDistributedCache distributedCache, 
            ILogger<AuthorizationManager> logger, 
            IEntityRepository entities, 
            IPermissionRepository permissions, 
            IRolePermissionRepository rolePermissions, 
            IRoleRepository roles, 
            IUserEntityRepository userEntities, 
            IUserEntityPermissionRepository userEntityPermissions, 
            IUserEntityRoleRepository userEntityRoles, 
            IUserPermissionRepository userPermissions, 
            IUserRoleRepository userRoles)
        {
            _distributedCache = distributedCache;
            _logger = logger;
            _entities = entities;
            _permissions = permissions;
            _rolePermissions = rolePermissions;
            _roles = roles;
            _userEntities = userEntities;
            _userEntityPermissions = userEntityPermissions;
            _userEntityRoles = userEntityRoles;
            _userPermissions = userPermissions;
            _userRoles = userRoles;
        }

        public async Task<bool> AuthorizeAsync(PermissionEntity permission, UserEntity user, CancellationToken ct)
        {
            await EnsureUserPopulationAsync(user, ct);

            return await ReadBooleanAsync(
                CreateUserPermissionCacheKey(
                    permission,
                    user),
                ct);
        }

        public async Task<bool> AuthorizeAsync(PermissionEntity permission, UserEntity user, EntityEntity entity, CancellationToken ct)
        {
            await EnsureUserPopulationAsync(user, ct);

            return await ReadBooleanAsync(
                CreateUserEntityPermissionCacheKey(
                    permission,
                    user,
                    entity),
                ct);
        }

        public async Task<bool> AuthorizeWithAnyEntityAsync(PermissionEntity permission, UserEntity user, CancellationToken ct)
        {
            await EnsureUserPopulationAsync(user, ct);

            return await ReadBooleanAsync(
                CreateUserEntityTransversalCacheKey(
                    permission,
                    user),
                ct);
        }

        public Task RefreshAsync(UserEntity user, CancellationToken ct)
        {
            _logger.LogInformation(
                "User '{UserId}' authorization is being refreshed.",
                user.PublicId.ToString());

            return _distributedCache.RemoveAsync(
                CreateUserCacheKey(user),
                ct);
        }

        private async Task EnsureUserPopulationAsync(UserEntity user, CancellationToken ct)
        {
            var userCacheKey = CreateUserCacheKey(user);

            if (await ReadBooleanAsync(userCacheKey, ct))
                return;

            var entities = (await _entities.GetAllAsync(ct))
                .ToDictionary(e => e.Id);

            var permissions = (await _permissions.GetAllAsync(ct))
                .ToDictionary(p => p.Id);

            var roles = (await _roles.GetAllAsync(ct))
                .ToDictionary(r => r.Id);

            var rolePermissions = (await _rolePermissions.GetAllAsync(ct))
                .GroupBy(rp => rp.RoleId)
                .ToDictionary(g => g.Key, g => g.ToArray());

            var userEntities = await _userEntities.GetAllAsync(ct);

            var userEntityPermissions = (await _userEntityPermissions.GetAllByUserIdAsync(user.Id, ct));

            var userEntityRoles = await _userEntityRoles.GetAllByUserIdAsync(user.Id, ct);

            var userEntityRolePermissions = userEntityRoles
                .GroupBy(uer => uer.EntityId)
                .ToDictionary(g => g.Key, g => g
                    .SelectMany(uer => rolePermissions.GetValueOrDefault(uer.RoleId) ?? Array.Empty<RolePermissionEntity>())
                    .GroupBy(rp => rp.PermissionId)
                    .ToDictionary(g => g.Key, g => g.Min(rp => rp.Granted)));

            var userPermissions = await _userPermissions.GetAllByUserIdAsync(user.Id, ct);

            var userRoles = await _userRoles.GetAllByUserIdAsync(user.Id, ct);

            var userRolePermissions = userRoles
                .SelectMany(rp => rolePermissions.GetValueOrDefault(rp.RoleId) ?? Array.Empty<RolePermissionEntity>())
                .GroupBy(rp => rp.PermissionId)
                .ToDictionary(g => g.Key, g => g.Min(rp => rp.Granted));

            var skip = new HashSet<long>(permissions.Count);

            // Permissions can be granted directly to the user and will
            // apply regardless of those set at role and entity level.
            foreach (var userPermission in userPermissions)
            {
                if (!permissions.TryGetValue(userPermission.PermissionId, out var permission))
                    throw new IndexOutOfRangeException("User permission is referencing a permission that does not exist.");

                skip.Add(userPermission.PermissionId);

                if (userPermission.Granted)
                {
                    await GrantAsync(
                        permission,
                        user,
                        ct);
                }
            }

            // Permissions can be granted through system wide user roles
            // and will apply regardless of those set at an entity level.
            foreach (var userRolePermissionKeyValue in userRolePermissions)
            {
                if (skip.Contains(userRolePermissionKeyValue.Key))
                    continue;

                skip.Add(userRolePermissionKeyValue.Key);

                if (!permissions.TryGetValue(userRolePermissionKeyValue.Key, out var permission))
                    throw new IndexOutOfRangeException("User entity permission is referencing a permission that does not exist.");

                if (userRolePermissionKeyValue.Value)
                {
                    await GrantAsync(
                        permission,
                        user,
                        ct);
                }
            }

            // System wide permissions will also automatically
            // propagate to any sub sets.
            foreach (var permissionId in skip)
            {
                foreach (var entity in entities.Values)
                {
                    await GrantAsync(
                        permissions[permissionId]!,
                        user,
                        entity,
                        ct);
                }
            }

            // Permissions can be granted through entities and will
            // apply regardless of any roles assigned to the user.
            foreach (var userEntityPermission in userEntityPermissions)
            {
                if (skip.Contains(userEntityPermission.PermissionId))
                    continue;

                skip.Add(userEntityPermission.Id);

                if (!permissions.TryGetValue(userEntityPermission.PermissionId, out var permission))
                    throw new IndexOutOfRangeException("User entity permission is referencing a permission that does not exist.");

                if (!entities.TryGetValue(userEntityPermission.EntityId, out var entity))
                    throw new IndexOutOfRangeException("User entity permission is referencing an entity that does not exist.");

                await GrantAsync(
                    permission,
                    user,
                    entity,
                    ct);
            }

            // Permissions can be granted through user entity roles,
            // which is the final layer of authorization.
            foreach (var userEntityRolePermissionKeyValue in userEntityRolePermissions)
            {
                if (!entities.TryGetValue(userEntityRolePermissionKeyValue.Key, out var entity))
                    throw new IndexOutOfRangeException("User entity role permission is referencing an entity that does not exist.");

                foreach (var userEntityRolePermissionResolutionKeyValue in userEntityRolePermissionKeyValue.Value)
                {
                    if (skip.Contains(userEntityRolePermissionResolutionKeyValue.Key))
                        continue;

                    if (!permissions.TryGetValue(userEntityRolePermissionResolutionKeyValue.Key, out var permission))
                        throw new IndexOutOfRangeException("User entity role permission is referencing a permission that does not exist.");

                    if (userEntityRolePermissionResolutionKeyValue.Value)
                    {
                        await GrantAsync(
                            permission,
                            user,
                            entity,
                            ct);
                    }
                }
            }

            await WriteBooleanAsync(
                userCacheKey, 
                true, 
                ct);
        }

        private Task GrantAsync(PermissionEntity permission, UserEntity user, CancellationToken ct)
        {
            return WriteBooleanAsync(
                CreateUserPermissionCacheKey(
                    permission,
                    user),
                true,
                ct);
        }

        private async Task GrantAsync(PermissionEntity permission, UserEntity user, EntityEntity entity, CancellationToken ct)
        {
            await WriteBooleanAsync(
                CreateUserEntityPermissionCacheKey(
                    permission,
                    user,
                    entity),
                true,
                ct);

            await WriteBooleanAsync(
                CreateUserEntityTransversalCacheKey(
                    permission,
                    user),
                true,
                ct);
        }

        private async Task<bool> ReadBooleanAsync(string key, CancellationToken ct)
        {
            var buffer = await _distributedCache.GetAsync(key, ct);

            return buffer?.Length == 1 && buffer[0] == 1;
        }

        private async Task WriteBooleanAsync(string key, bool value, CancellationToken ct)
        {
            await _distributedCache.SetAsync(
                key,
                new byte[] { (byte)(value ? 1 : 0) },
                DistributedCacheEntryOptions,
                ct);
        }

    }
}
