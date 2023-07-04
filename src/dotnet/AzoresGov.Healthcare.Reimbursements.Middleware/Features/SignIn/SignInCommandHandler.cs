using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Configuration;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

using ClaimTypes = AzoresGov.Healthcare.Reimbursements.ClaimTypes;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResult>
    {
        private const string GenericUserMessage = "Este endereço de correio eletrónico e palavra-passe não correspondem a um utilizador existente.";

        private readonly IConfiguration _configuration;

        private readonly IUserAgentRepository _userAgents;

        private readonly IUserEmailAddressRepository _userEmailAddresses;

        private readonly IUserPasswordRepository _userPasswords;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public SignInCommandHandler(
            IConfiguration configuration, 
            IUserAgentRepository userAgents, 
            IUserEmailAddressRepository userEmailAddresses, 
            IUserPasswordRepository userPasswords, 
            IUserRepository users, 
            IUserSessionRepository userSessions)
        {
            _configuration = configuration;
            _userAgents = userAgents;
            _userEmailAddresses = userEmailAddresses;
            _userPasswords = userPasswords;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<SignInResult> HandleCommandAsync(SignInCommand command, CancellationToken ct)
        {
            var userEmailAddress = await GetUserEmailAddressAsync(command, ct);

            var user = await GetUserAsync(
                userEmailAddress,
                ct);

            var userPassword = await GetLastUserPasswordAsync(
                user,
                ct);

            EnsureUserPasswordHashMatch(
                command,
                userPassword);

            await EnsureUserPasswordHashWorkFactorAsync(
                command,
                userPassword,
                ct);

            var userAgent = await GetOrCreateUserAgentAsync(
                command,
                ct);

            var userSession = CreateUserSession(
                command,
                user,
                userAgent);

            return new SignInResult(
                user.PublicId,
                user.RowVersionId,
                userSession.PublicId,
                userSession.RowVersionId,
                userSession.Secret);
        }

        private UserSessionEntity CreateUserSession(SignInCommand command, UserEntity user, UserAgentEntity userAgent)
        {
            var userSession = new UserSessionEntity()
            {
                PublicId = Guid.NewGuid(),
                RowVersionId = Guid.NewGuid(),
                User = user,
                UserAgent = userAgent,
                NetworkAddress = command.NetworkAddress.ToString(),
                Secret = UserSessionHelper.CreateSecret(),
                Start = command.Creation,
                LastSeen = command.Creation
            };

            _userSessions.Add(userSession);

            return userSession;
        }

        private async Task<UserAgentEntity> GetOrCreateUserAgentAsync(SignInCommand command, CancellationToken ct)
        {
            var userAgentHash = HashHelper.ComputeHash(command.UserAgent);

            var userAgent = await _userAgents.GetByHashAsync(userAgentHash, ct);

            if (userAgent is null)
            {
                userAgent = new UserAgentEntity()
                {
                    Hash = userAgentHash,
                    Signature = command.UserAgent
                };

                _userAgents.Add(userAgent);
            }

            return userAgent;
        }

        private async Task EnsureUserPasswordHashWorkFactorAsync(SignInCommand command, UserPasswordEntity userPassword, CancellationToken ct)
        {
            var userPasswordHashOptions = await _configuration.GetUserPasswordHashOptionsAsync(ct);

            if (!UserPasswordHashHelper.ValidatePasswordHash(userPassword.Hash, userPasswordHashOptions.WorkFactor))
                userPassword.Hash = UserPasswordHashHelper.ComputePasswordHash(command.Password, userPasswordHashOptions.WorkFactor);
        }

        private static void EnsureUserPasswordHashMatch(SignInCommand command, UserPasswordEntity userPassword)
        {
            if (!UserPasswordHashHelper.ValidatePassword(command.Password, userPassword.Hash))
            {
                throw new BusinessException("The given password does not match the existing user password hash.")
                    .WithCode("MGSGXT")
                    .WithUserMessage(GenericUserMessage);
            }
        }

        private async Task<UserPasswordEntity> GetLastUserPasswordAsync(UserEntity user, CancellationToken ct)
        {
            var userPassword = await _userPasswords.GetLastByUserIdAsync(
                user.Id, 
                ct);

            if (userPassword == null)
            {
                throw new BusinessException("A user password is not set for the matching user.")
                    .WithCode("QJZGXO")
                    .WithUserMessage(GenericUserMessage);
            }

            return userPassword;
        }

        private async Task<UserEntity> GetUserAsync(UserEmailAddressEntity userEmailAddress, CancellationToken ct)
        {
            var user = await _users.GetByUserEmailAddressIdAsync(
                userEmailAddress.Id, 
                ct);

            if (user == null)
            {
                throw new InvalidOperationException("A user was not found matching the given user email address identifier.")
                    .WithCode("IWQIWJ");
            }

            return user;
        }

        private async Task<UserEmailAddressEntity> GetUserEmailAddressAsync(SignInCommand command, CancellationToken ct)
        {
            var userEmailAddress = await _userEmailAddresses.GetWithUserByEmailAddressAsync(
                command.EmailAddress,
                ct);

            if (userEmailAddress == null)
            {
                throw new BusinessException("A user was not found matching the given email address.")
                    .WithCode("INLLAJ")
                    .WithUserMessage(GenericUserMessage);
            }

            return userEmailAddress;
        }
    }
}
