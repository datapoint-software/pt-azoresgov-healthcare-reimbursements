using AzoresGov.Healthcare.Reimbursements.Configuration;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Configuration;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshCommandHandler : ICommandHandler<IdentityRefreshCommand, IdentityRefreshResult>
    {
        private const string GenericUserMessage = "A sua sessão expirou.";

        private readonly IConfiguration _configuration;

        private readonly IEntityRepository _entities;

        private readonly IPermissionRepository _permissions;

        private readonly IRolePermissionRepository _rolePermissions;

        private readonly IUserAgentRepository _userAgents;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserEntityPermissionRepository _userEntityPermissions;

        private readonly IUserEntityRoleRepository _userEntityRoles;

        private readonly IUserPasswordRepository _userPasswords;

        private readonly IUserPermissionRepository _userPermissions;

        private readonly IUserRoleRepository _userRoles;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public IdentityRefreshCommandHandler(
            IConfiguration configuration, 
            IEntityRepository entities, 
            IPermissionRepository permissions, 
            IRolePermissionRepository rolePermissions, 
            IUserAgentRepository userAgents, 
            IUserEntityRepository userEntities, 
            IUserEntityPermissionRepository userEntityPermissions, 
            IUserEntityRoleRepository userEntityRoles, 
            IUserPasswordRepository userPasswords, 
            IUserPermissionRepository userPermissions, 
            IUserRoleRepository userRoles, 
            IUserRepository users, 
            IUserSessionRepository userSessions)
        {
            _configuration = configuration;
            _entities = entities;
            _permissions = permissions;
            _rolePermissions = rolePermissions;
            _userAgents = userAgents;
            _userEntities = userEntities;
            _userEntityPermissions = userEntityPermissions;
            _userEntityRoles = userEntityRoles;
            _userPasswords = userPasswords;
            _userPermissions = userPermissions;
            _userRoles = userRoles;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<IdentityRefreshResult> HandleCommandAsync(IdentityRefreshCommand command, CancellationToken ct)
        {
            var userSessionOptions = await _configuration.GetUserSessionOptionsAsync(ct);

            AssertUserSessionOptions(
                command, 
                userSessionOptions);

            var userSession = await GetUserSessionAsync(
                command,
                ct);

            AssertUserSessionRowVersion(
                command,
                userSession);

            AssertUserSessionExpiration(
                userSession,
                userSessionOptions);

            RefreshUserSession(
                command,
                userSession);

            var userSessionExpiration = GetUserSessionExpiration(
                command,
                userSessionOptions);

            var user = await GetUserAsync(
                userSession,
                ct);

            var userRoles = await GetAllUserRolesAsync(
                user,
                ct);

            var userPermissions = await GetAllUserPermissionsAsync(
                user,
                ct);

            var entities = await GetAllEntitiesAsync(
                user,
                ct);

            var userEntityPermissions = await GetAllUserEntityPermissionsAsync(
                user,
                entities.Values,
                ct);

            var userEntityRoles = await GetAllUserEntityRolesAsync(
                user,
                entities.Values,
                ct);

            var rolePermissions = await GetAllRolePermissionsAsync(
                userRoles,
                userEntityRoles.SelectMany(e => e.Value),
                ct);

            var permissions = await GetAllPermissionsAsync(
                userPermissions.Values,
                userEntityPermissions,
                rolePermissions,
                ct);

            return new IdentityRefreshResult(
                entities
                    .Select(e => new IdentityRefreshEntityResult(
                        e.Value.PublicId,
                        (userEntityPermissions[e.Key] ?? Array.Empty<UserEntityPermissionEntity>())
                            .Where(uep => !userPermissions.ContainsKey(uep.PermissionId))
                            .Where(uep => !userRoles.Any(ur => (rolePermissions[ur.RoleId]?.Any(rp => rp.PermissionId == uep.PermissionId) ?? false)))
                            .Where(uep => uep.Granted)
                            .Select(uep => new IdentityRefreshPermissionResult(
                                permissions[uep.PermissionId].PublicId,
                                permissions[uep.PermissionId].Name))
                            .ToArray()))
                    .ToArray(),
                userPermissions.Values
                    .Where(up => up.Granted)
                    .Select(up => up.PermissionId)
                    .Union(userRoles
                        .SelectMany(ur => rolePermissions[ur.RoleId] ?? Array.Empty<RolePermissionEntity>())
                        .Where(rp => !userPermissions.ContainsKey(rp.PermissionId))
                        .Where(rp => rp.Granted)
                        .Select(rp => rp.PermissionId))
                    .Distinct()
                    .Select(pid => new IdentityRefreshPermissionResult(
                        permissions[pid].PublicId,
                        permissions[pid].Name))
                    .ToArray(),
                new IdentityRefreshUserResult(
                    user.PublicId,
                    user.RowVersionId,
                    user.Name),
                new IdentityRefreshUserSessionResult(
                    userSession.PublicId,
                    userSession.RowVersionId,
                    userSessionExpiration));
        }

        private static DateTimeOffset? GetUserSessionExpiration(IdentityRefreshCommand command, UserSessionOptions userSessionOptions)
        {
            if (command.Persistent && userSessionOptions.Expiration.HasValue)
                return command.Creation.AddSeconds(userSessionOptions.Expiration.Value);

            return null;
        }

        private static void AssertUserSessionOptions(IdentityRefreshCommand command, UserSessionOptions userSessionOptions)
        {
            if (command.Persistent && !userSessionOptions.Expiration.HasValue)
            {
                throw new BusinessException("Persistent user sessions feature is not enabled.")
                    .WithCode("K7HYTM")
                    .WithUserMessage("As sessões persistentes não foram ativadas pelo administrador do sistema.");
            }
        }

        private async Task<UserEntity> GetUserAsync(UserSessionEntity userSession, CancellationToken ct)
        {
            var user = await _users.GetByIdAsync(
                userSession.UserId,
                ct);

            if (user == null)
            {
                throw new BusinessException("A user was not found matching this session.")
                    .WithCode("K9HUXM")
                    .WithUserMessage(GenericUserMessage);
            }

            return user;
        }

        private async Task<IReadOnlyDictionary<long, PermissionEntity>> GetAllPermissionsAsync(IEnumerable<UserPermissionEntity> userPermissions, IReadOnlyDictionary<long, IEnumerable<UserEntityPermissionEntity>> userEntityPermissions, IReadOnlyDictionary<long, IEnumerable<RolePermissionEntity>> rolePermissions, CancellationToken ct)
        {
            var permissions = await _permissions.GetAllByIdAsync(
                userPermissions
                    .Select(e => e.PermissionId)
                    .Union(rolePermissions
                        .SelectMany(e => e.Value.Select(e => e.PermissionId)))
                    .Distinct(),
                ct);

            return permissions.ToDictionary(e => e.Id);
        }

        private async Task<IReadOnlyDictionary<long, UserPermissionEntity>> GetAllUserPermissionsAsync(UserEntity user, CancellationToken ct)
        {
            var userPermissions = await _userPermissions.GetAllByUserIdAsync(
                user.Id,
                ct);

            return userPermissions.ToDictionary(
                up => up.PermissionId);
        }

        private async Task<IReadOnlyDictionary<long, IEnumerable<RolePermissionEntity>>> GetAllRolePermissionsAsync(IEnumerable<UserRoleEntity> userRoles, IEnumerable<UserEntityRoleEntity> userEntityRoles, CancellationToken ct)
        {
            var rolePermissions = await _rolePermissions.GetAllByRoleIdAsync(
                userRoles.Select(e => e.RoleId)
                    .Union(userEntityRoles.Select(e => e.RoleId))
                    .Distinct(),
                ct);

            return rolePermissions
                .GroupBy(e => e.RoleId)
                .ToDictionary(e => e.Key, e => e.AsEnumerable());
        }

        private Task<IEnumerable<UserRoleEntity>> GetAllUserRolesAsync(UserEntity user, CancellationToken ct) =>

            _userRoles.GetAllByUserIdAsync(
                user.Id,
                ct);

        private async Task<IReadOnlyDictionary<long, IEnumerable<UserEntityRoleEntity>>> GetAllUserEntityRolesAsync(UserEntity user, IEnumerable<EntityEntity> values, CancellationToken ct)
        {
            var userEntityRoles = await _userEntityRoles.GetAllByUserIdAndEntityIdAsync(
                user.Id,
                values.Select(e => e.Id),
                ct);

            return userEntityRoles
                .GroupBy(e => e.EntityId)
                .ToDictionary(e => e.Key, e => e.AsEnumerable());
        }

        private async Task<IReadOnlyDictionary<long, IEnumerable<UserEntityPermissionEntity>>> GetAllUserEntityPermissionsAsync(UserEntity user, IEnumerable<EntityEntity> entities, CancellationToken ct)
        {
            var userEntityPermissions = await _userEntityPermissions.GetAllByUserIdAndEntityIdAsync(
                user.Id,
                entities.Select(e => e.Id),
                ct);

            return userEntityPermissions
                .GroupBy(e => e.EntityId)
                .ToDictionary(
                    e => e.Key,
                    e => e.AsEnumerable());
        }

        private async Task<IReadOnlyDictionary<long, EntityEntity>> GetAllEntitiesAsync(UserEntity user, CancellationToken ct)
        {
            var userEntities = await _userEntities.GetAllByUserIdAsync(
                user.Id,
                ct);

            var entities = await _entities.GetAllByIdAsync(
                userEntities.Select(ue => ue.EntityId).ToArray(),
                ct);

            return entities.ToDictionary(e => e.Id);
        }

        private void AssertUserSessionExpiration(UserSessionEntity userSession, UserSessionOptions userSessionOptions)
        {
            if (userSessionOptions.Expiration.HasValue)
            {
                var lastSeenMinimum = userSession.LastSeen.AddSeconds(
                    -userSessionOptions.Expiration.Value);

                if (userSession.LastSeen < lastSeenMinimum)
                {
                    throw new BusinessException("User session was last seen too long ago.")
                        .WithCode("AAKWPU")
                        .WithUserMessage(GenericUserMessage);
                }
            }
        }

        private static void RefreshUserSession(IdentityRefreshCommand command, UserSessionEntity userSession)
        {
            userSession.RowVersionId = Guid.NewGuid();
            userSession.LastSeen = command.Creation;
        }

        private void AssertUserSessionRowVersion(IdentityRefreshCommand command, UserSessionEntity userSession)
        {
            if (command.UserSessionRowVersionId == userSession.RowVersionId)
                return;

            throw new BusinessException("User session row version mismatch.")
                .WithCode("GQOPAQ")
                .WithUserMessage(GenericUserMessage);
        }

        private async Task<UserSessionEntity> GetUserSessionAsync(IdentityRefreshCommand command, CancellationToken ct)
        {
            var userSession = await _userSessions.GetByPublicIdAsync(
                command.UserSessionId,
                ct);

            if (userSession == null)
            {
                throw new BusinessException("A user session was not found matching the given identifier.")
                    .WithCode("UDTXAL")
                    .WithUserMessage(GenericUserMessage);
            }

            return userSession;
        }
    }
}
