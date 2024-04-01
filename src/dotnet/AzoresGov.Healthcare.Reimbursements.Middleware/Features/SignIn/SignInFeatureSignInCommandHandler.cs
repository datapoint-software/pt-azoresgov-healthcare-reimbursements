using AzoresGov.Healthcare.Reimbursements.Management;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInFeatureSignInCommandHandler : ICommandHandler<SignInFeatureSignInCommand, SignInFeatureSignInResult>
    {
        private const string GenericErrorMessage = "O endereço de correio eletrónico e/ou palavra-passe não corresponde a um perfil existente.";

        private readonly IParameterManager _parameters;

        private readonly IUserPasswordRepository _userPasswords;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public SignInFeatureSignInCommandHandler(IParameterManager parameters, IUserPasswordRepository userPasswords, IUserRepository users, IUserSessionRepository userSessions)
        {
            _parameters = parameters;
            _userPasswords = userPasswords;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<SignInFeatureSignInResult> HandleCommandAsync(
            SignInFeatureSignInCommand command, 
            CancellationToken ct)
        {
            var user = await _users.GetByEmailAddressAsync(
                command.EmailAddress,
                ct);

            if (user is null)
            {
                throw new BusinessException("A user was not found matching the given email address.")
                    .WithErrorMessage(GenericErrorMessage);
            }

            var userPassword = await _userPasswords.GetByUserIdAsync(
                user.Id,
                ct);

            if (userPassword is null)
            {
                throw new BusinessException("A user password was not found matching this user.")
                    .WithErrorMessage(GenericErrorMessage);
            }

            if (!UserPasswordHelper.VerifyPassword(command.Password, userPassword.Hash))
            {
                throw new BusinessException("User password hash mismatch.")
                    .WithErrorMessage(GenericErrorMessage);
            }

            var userPasswordHashWorkFactor = await _parameters.GetUserPasswordHashWorkFactorAsync(ct);

            if (!UserPasswordHelper.VerifyPasswordHash(userPassword.Hash, userPasswordHashWorkFactor))
                userPassword.Hash = UserPasswordHelper.ComputePasswordHash(command.Password, userPasswordHashWorkFactor);

            var userSessionExpiration = await GetUserSessionExpirationAsync(
                command,
                ct);

            var userSession = _userSessions.Add(new UserSession()
            {
                PublicId = Guid.NewGuid(),
                RowVersionId = Guid.NewGuid(),
                User = user,
                UserAgent = command.UserAgent,
                RemoteAddress = command.RemoteAddress.ToString(),
                Creation = command.Creation,
                Expiration = userSessionExpiration,
                LastSeen = command.Creation
            });

            return new SignInFeatureSignInResult(
                new SignInFeatureSignInResultUser(
                    user.PublicId,
                    user.RowVersionId,
                    user.Name,
                    user.EmailAddress),
                new SignInFeatureSignInResultUserSession(
                    userSession.PublicId,
                    userSession.RowVersionId,
                    userSession.Expiration));
        }

        private async Task<DateTimeOffset?> GetUserSessionExpirationAsync(SignInFeatureSignInCommand command, CancellationToken ct)
        {
            if (command.Persistent && await _parameters.GetPersistentSessionsEnabledAsync(ct))
                return null;

            var userSessionExpirationInSeconds = await _parameters.GetUserSessionExpirationInSecondsAsync(ct);

            return command.Creation.AddSeconds(userSessionExpirationInSeconds);
        }
    }
}
