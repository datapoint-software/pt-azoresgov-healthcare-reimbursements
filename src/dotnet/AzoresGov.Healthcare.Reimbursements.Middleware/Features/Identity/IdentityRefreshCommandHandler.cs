using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Configuration;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshCommandHandler : ICommandHandler<IdentityRefreshCommand, IdentityRefreshResult>
    {
        private const string GenericUserMessage = "A sua sessão expirou.";

        private readonly IConfiguration _configuration;

        private readonly IUserSessionRepository _userSessions;

        public IdentityRefreshCommandHandler(IConfiguration configuration, IUserSessionRepository userSessions)
        {
            _configuration = configuration;
            _userSessions = userSessions;
        }

        public async Task<IdentityRefreshResult> HandleCommandAsync(IdentityRefreshCommand command, CancellationToken ct)
        {
            var userSession = await GetUserSessionAsync(
                command,
                ct);

            AssertUserSessionRowVersion(
                command,
                userSession);

            await AssertUserSessionExpirationAsync(
                command,
                userSession,
                ct);

            AssertUserSessionSecret(
                command,
                userSession);

            RefreshUserSession(
                userSession);

            return new IdentityRefreshResult(
                userSession.RowVersionId,
                userSession.Secret);
        }

        private async Task AssertUserSessionExpirationAsync(IdentityRefreshCommand command, UserSessionEntity userSession, CancellationToken ct)
        {
            var userSessionOptions = await _configuration.GetUserSessionOptionsAsync(ct);

            if (userSessionOptions.Expiration.HasValue)
            {
                var lastSeenMinimum = userSession.LastSeen.AddSeconds(
                    -userSessionOptions.Expiration.Value);

                if (userSession.LastSeen < lastSeenMinimum)
                {
                    throw new AuthenticationException("User session moment of last seen is bellow minimum.")
                        .WithCode("AAKWPU")
                        .WithUserMessage(GenericUserMessage);
                }
            }
        }

        private static void RefreshUserSession(UserSessionEntity userSession)
        {
            userSession.RowVersionId = Guid.NewGuid();
            userSession.Secret = UserSessionHelper.CreateSecret();
            userSession.LastSeen = DateTimeOffset.UtcNow;
        }

        private void AssertUserSessionSecret(IdentityRefreshCommand command, UserSessionEntity userSession)
        {
            if (command.UserSessionSecret == userSession.Secret)
                return;

            throw new AuthenticationException("User session secret mismatch.")
                .WithCode("QVGJUJ")
                .WithUserMessage(GenericUserMessage);
        }

        private void AssertUserSessionRowVersion(IdentityRefreshCommand command, UserSessionEntity userSession)
        {
            if (command.UserSessionRowVersionId == userSession.RowVersionId)
                return;

            throw new AuthenticationException("User session row version mismatch.")
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
                throw new AuthenticationException("A user session was not found matching the given identifier.")
                    .WithCode("UDTXAL")
                    .WithUserMessage(GenericUserMessage);
            }

            return userSession;
        }
    }
}
