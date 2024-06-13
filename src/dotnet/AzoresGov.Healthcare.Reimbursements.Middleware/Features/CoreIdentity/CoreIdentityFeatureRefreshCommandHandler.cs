using AzoresGov.Healthcare.Reimbursements.Management;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity
{
    public sealed class CoreIdentityFeatureRefreshCommandHandler : ICommandHandler<CoreIdentityFeatureRefreshCommand, CoreIdentityFeatureRefreshResult>
    {
        private readonly IParameterManager _parameters;

        private readonly IUserRoleRepository _userRoles;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public CoreIdentityFeatureRefreshCommandHandler(IParameterManager parameters, IUserRoleRepository userRoles, IUserRepository users, IUserSessionRepository userSessions)
        {
            _parameters = parameters;
            _userRoles = userRoles;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<CoreIdentityFeatureRefreshResult> HandleCommandAsync(CoreIdentityFeatureRefreshCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                command.UserId,
                ct);

            Assert.Found(user);

            var userSession = await _userSessions.GetByPublicIdAsync(
                command.UserSessionId,
                ct);

            Assert.Found(userSession);

            if (userSession.Expiration is not null)
            {
                if (userSession.Expiration < command.Creation)
                {
                    throw new BusinessException("User session has expired.")
                        .WithErrorMessage("A sessão do utilizador já expirou.");
                }

                userSession.Expiration = command.Creation.AddSeconds(
                    await _parameters.GetUserSessionExpirationInSecondsAsync(ct));
            }

            var userRoles = await _userRoles.GetAllByUserIdAsync(
                user.Id,
                ct);

            userSession.RowVersionId = Guid.NewGuid();
            userSession.LastSeen = command.Creation;

            return new CoreIdentityFeatureRefreshResult(
                new CoreIdentityFeatureRefreshResultUser(
                    user.PublicId,
                    user.RowVersionId,
                    user.Name,
                    user.EmailAddress),
                new CoreIdentityFeatureRefreshResultUserSession(
                    userSession.PublicId,
                    userSession.RowVersionId,
                    userSession.Expiration),
                userRoles
                    .Select(e => new CoreIdentityFeatureRefreshResultUserRole(
                        e.PublicId,
                        e.RowVersionId,
                        e.Nature))
                    .ToArray());
        }
    }
}
