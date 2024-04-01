﻿using AzoresGov.Healthcare.Reimbursements.Management;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityFeatureRefreshCommandHandler : ICommandHandler<IdentityFeatureRefreshCommand, IdentityFeatureRefreshResult>
    {
        private readonly IParameterManager _parameters;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public IdentityFeatureRefreshCommandHandler(IParameterManager parameters, IUserRepository users, IUserSessionRepository userSessions)
        {
            _parameters = parameters;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<IdentityFeatureRefreshResult> HandleCommandAsync(IdentityFeatureRefreshCommand command, CancellationToken ct)
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

            userSession.RowVersionId = Guid.NewGuid();
            userSession.LastSeen = command.Creation;

            return new IdentityFeatureRefreshResult(
                new IdentityFeatureRefreshResultUser(
                    user.PublicId,
                    user.RowVersionId,
                    user.Name,
                    user.EmailAddress),
                new IdentityFeatureRefreshResultUserSession(
                    userSession.PublicId,
                    userSession.RowVersionId,
                    userSession.Expiration));
        }
    }
}
