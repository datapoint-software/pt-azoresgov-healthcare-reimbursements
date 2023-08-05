using AzoresGov.Healthcare.Reimbursements.Configuration;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Managers;
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

        private readonly AuthorizationManager _authorization;

        private readonly IConfiguration _configuration;

        private readonly IEntityRepository _entities;

        private readonly IPermissionRepository _permissions;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public IdentityRefreshCommandHandler(
            AuthorizationManager authorization,
            IConfiguration configuration, 
            IEntityRepository entities, 
            IPermissionRepository permissions,
            IUserEntityRepository userEntities, 
            IUserRepository users, 
            IUserSessionRepository userSessions)
        {
            _authorization = authorization;
            _configuration = configuration;
            _entities = entities;
            _permissions = permissions;
            _userEntities = userEntities;
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

            var user = await GetUserAsync(userSession, ct);

            var userSessionExpiration = GetUserSessionExpiration(
                command,
                userSessionOptions);

            return await BuildResultAsync(
                user,
                userSession,
                userSessionExpiration,
                ct);
        }

        private async Task<IdentityRefreshResult> BuildResultAsync(UserEntity user, UserSessionEntity userSession, DateTimeOffset? userSessionExpiration, CancellationToken ct)
        {

            var userEntities = await _userEntities.GetAllByUserIdAsync(
                user.Id,
                ct);

            var entities = await _entities.GetAllByIdAsync(
                userEntities.Select(ue => ue.EntityId),
                ct);

            var permissions = await _permissions.GetAllAsync(ct);

            var entityResults = new List<IdentityRefreshEntityResult>();

            foreach (var entity in entities)
            {
                var entityPermissionResults = new List<IdentityRefreshPermissionResult>();

                foreach (var permission in permissions)
                {
                    if (await _authorization.AuthorizeAsync(permission, user, entity, ct))
                    {
                        entityPermissionResults.Add(
                            new IdentityRefreshPermissionResult(
                                permission.PublicId,
                                permission.Name));
                    }
                }

                entityResults.Add(
                    new IdentityRefreshEntityResult(
                        entity.PublicId,
                        entityPermissionResults));
            }

            var permissionResults = new List<IdentityRefreshPermissionResult>();

            foreach (var permission in permissions)
            {
                if (await _authorization.AuthorizeAsync(permission, user, ct))
                {
                    permissionResults.Add(
                        new IdentityRefreshPermissionResult(
                            permission.PublicId,
                            permission.Name));
                }
            }

            return new IdentityRefreshResult(
                entityResults,
                permissionResults,
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
