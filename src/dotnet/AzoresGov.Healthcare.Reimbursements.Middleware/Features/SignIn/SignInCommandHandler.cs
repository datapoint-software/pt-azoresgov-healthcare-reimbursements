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
    public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResult>
    {
        private const string GenericCredentialsErrorCode = "ATHCRD";

        private const string GenericCredentialsErrorMessage = "O endereço de correio electrónico e/ou a palavra-passe não correspondem a um perfil existente.";

        private readonly IParameterManager _parameters;

        private readonly IUserPasswordRepository _userPasswords;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public SignInCommandHandler(IParameterManager parameters, IUserPasswordRepository userPasswords, IUserRepository users, IUserSessionRepository userSessions)
        {
            _parameters = parameters;
            _userPasswords = userPasswords;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<SignInResult> HandleCommandAsync(SignInCommand command, CancellationToken ct)
        {
            await EnsureBasicAuthenticationEnabledAsync(ct);

            // WARNING 
            //
            // In order to prevent time based brute force exploit attempts, we'll make
            // sure this command always takes the exact same amount of time regardless
            // of wether or not a matching user was found.
            //
            // The delay, in milliseconds, must be adjusted according to the hosts
            // processing capacity and the defined password hash work factor: to keep it
            // simple, it should always be greater than the amount of time it takes
            // to match, compare and rehash the user password.
            //
            // TODO <joao.pl.lopes>
            // 
            // We need to measure the time it takes both tasks to complete and,
            // if `delay` takes less time than `handler`, we must log a warning
            // message to make the system administrator aware of this.
            var delay = Task.Delay(await _parameters.GetBasicAuthenticationDelayAsync(ct), ct);
            var handler = HandleCommandWithoutDelayAsync(command, ct);

            await Task.WhenAll(delay, handler);

            return handler.Result;
        }

        private async Task<SignInResult> HandleCommandWithoutDelayAsync(SignInCommand command, CancellationToken ct)
        {
            // Get the user.
            var user = await _users.GetByEmailAddressAsync(
                command.EmailAddress,
                ct);

            if (user == null)
            {
                throw new BusinessException("A user was not found matching this email address.")
                    .WithErrorCode(GenericCredentialsErrorCode)
                    .WithErrorMessage(GenericCredentialsErrorMessage);
            }

            // Get and verify the user password.
            var userPassword = await _userPasswords.GetByUserIdAsync(
                user.Id,
                ct);

            if (userPassword is null)
            {
                throw new BusinessException("A password has not been set for the matching user.")
                    .WithErrorCode(GenericCredentialsErrorCode)
                    .WithErrorMessage(GenericCredentialsErrorMessage);
            }

            if (!UserPasswordHelper.VerifyPassword(command.Password, userPassword.Hash))
            {
                throw new BusinessException("User password hash mismatch.")
                    .WithErrorCode(GenericCredentialsErrorCode)
                    .WithErrorMessage(GenericCredentialsErrorMessage);
            }

            // Ensure the user password hash work factor is up to date.
            var userPasswordHashWorkFactor = await _parameters.GetUserPasswordHashWorkFactorAsync(ct);

            if (!UserPasswordHelper.VerifyPasswordHash(userPassword.Hash, userPasswordHashWorkFactor))
                userPassword.Hash = UserPasswordHelper.ComputePasswordHash(command.Password, userPasswordHashWorkFactor);

            // Calculate the user session expiration.
            var expiration = await GetUserSessionExpirationAsync(
                command,
                ct);

            // Create the user session.
            var userSession = _userSessions.Add(new UserSessionEntity()
            {
                PublicId = Guid.NewGuid(),
                User = user,
                Agent = command.UserAgent,
                NetworkAddress = command.UserNetworkAddress.ToString(),
                Creation = command.Creation,
                LastSeen = command.Creation,
                Expiration = expiration
            });

            return new SignInResult(
                new SignInSessionResult(
                    userSession.PublicId,
                    userSession.Expiration),
                new SignInUserResult(
                    user.PublicId,
                    user.Name,
                    user.EmailAddress));
        }

        private async Task<DateTimeOffset?> GetUserSessionExpirationAsync(SignInCommand command, CancellationToken ct)
        {
            if (command.Persistent)
            {
                if (!await _parameters.GetBasicAuthenticationPersistentSessionsEnabledAsync(ct))
                {
                    throw new BusinessException("Basic authentication persistent sessions are not enabled.")
                        .WithErrorCode("ATHPSD")
                        .WithErrorMessage("A persistência de sessões foi desativada para este ambiente.");
                }

                return null;
            }

            return command.Creation.AddSeconds(
                await _parameters.GetUserSessionExpirationAsync(ct));
        }

        private async Task EnsureBasicAuthenticationEnabledAsync(CancellationToken ct)
        {
            if (!await _parameters.GetBasicAuthenticationEnabledAsync(ct))
            {
                throw new BusinessException("Basic authentication is not enabled.")
                    .WithErrorCode("ATHMNE")
                    .WithErrorMessage("Este método de autenticação não está disponível.");
            }
        }
    }
}
