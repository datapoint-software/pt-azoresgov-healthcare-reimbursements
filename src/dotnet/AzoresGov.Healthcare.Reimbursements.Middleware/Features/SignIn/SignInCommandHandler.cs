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

using ClaimTypes = AzoresGov.Healthcare.Reimbursements.ClaimTypes;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResult>
    {
        private const string GenericUserMessage = "Este endereço de correio eletrónico e palavra-passe não correspondem a um utilizador existente.";

        private readonly AuthorizationManager _authorization;

        private readonly IConfiguration _configuration;

        private readonly IEntityRepository _entities;

        private readonly IPermissionRepository _permissions;

        private readonly IUserAgentRepository _userAgents;

        private readonly IUserEmailAddressRepository _userEmailAddresses;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserPasswordRepository _userPasswords;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public SignInCommandHandler(
            AuthorizationManager authorization,
            IConfiguration configuration, 
            IEntityRepository entities, 
            IPermissionRepository permissions, 
            IUserAgentRepository userAgents, 
            IUserEmailAddressRepository userEmailAddresses, 
            IUserEntityRepository userEntities, 
            IUserPasswordRepository userPasswords, 
            IUserRepository users, 
            IUserSessionRepository userSessions)
        {
            _authorization = authorization;
            _configuration = configuration;
            _entities = entities;
            _permissions = permissions;
            _userAgents = userAgents;
            _userEmailAddresses = userEmailAddresses;
            _userEntities = userEntities;
            _userPasswords = userPasswords;
            _users = users;
            _userSessions = userSessions;
        }

        public async Task<SignInResult> HandleCommandAsync(SignInCommand command, CancellationToken ct)
        {
            var userSessionOptions = await _configuration.GetUserSessionOptionsAsync(ct);

            await AssertAuthenticationEnabledAsync(ct);

            AssertUserSessionOptions(
                command, 
                userSessionOptions);

            var timeBasedBruteforcePreventionDelayTask = Task.Delay(7500, ct);

            var handleSignInCommandTask = HandleSignInCommandAsync(
                command, 
                userSessionOptions,
                ct);

            await Task.WhenAll(
                timeBasedBruteforcePreventionDelayTask,
                handleSignInCommandTask);

            return handleSignInCommandTask.Result;
        }

        private async Task<SignInResult> HandleSignInCommandAsync(SignInCommand command, UserSessionOptions userSessionOptions, CancellationToken ct)
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

            var userSessionExpiration = GetUserSessionExpiration(
                command,
                userSessionOptions);

            await _authorization.PopulateAsync(user, ct);

            return await BuildResultAsync(
                user, 
                userSession, 
                userSessionExpiration, 
                ct);
        }

        private async Task<SignInResult> BuildResultAsync(UserEntity user, UserSessionEntity userSession, DateTimeOffset? userSessionExpiration, CancellationToken ct)
        {

            var userEntities = await _userEntities.GetAllByUserIdAsync(
                user.Id, 
                ct);

            var entities = await _entities.GetAllByIdAsync(
                userEntities.Select(ue => ue.EntityId),
                ct);

            var permissions = await _permissions.GetAllAsync(ct);

            var entityResults = new List<SignInEntityResult>();

            foreach (var entity in entities)
            {
                var entityPermissionResults = new List<SignInPermissionResult>();

                foreach (var permission in permissions)
                {
                    if (await _authorization.AuthorizeAsync(permission, user, entity, ct))
                    {
                        entityPermissionResults.Add(
                            new SignInPermissionResult(
                                permission.PublicId,
                                permission.Name));
                    }
                }

                entityResults.Add(
                    new SignInEntityResult(
                        entity.PublicId,
                        entityPermissionResults));
            }

            var permissionResults = new List<SignInPermissionResult>();

            foreach (var permission in permissions)
            {
                if (await _authorization.AuthorizeAsync(permission, user, ct))
                {
                    permissionResults.Add(
                        new SignInPermissionResult(
                            permission.PublicId,
                            permission.Name));
                }
            }

            return new SignInResult(
                entityResults,
                permissionResults,
                new SignInUserResult(
                    user.PublicId,
                    user.RowVersionId,
                    user.Name),
                new SignInUserSessionResult(
                    userSession.PublicId,
                    userSession.RowVersionId,
                    userSessionExpiration));
        }

        private static void AssertUserSessionOptions(SignInCommand command, UserSessionOptions userSessionOptions)
        {
            if (command.Persistent && !userSessionOptions.Expiration.HasValue)
            {
                throw new BusinessException("Persistent user sessions feature is not enabled.")
                    .WithCode("K7HYTM")
                    .WithUserMessage("As sessões persistentes não foram ativadas pelo administrador do sistema.");
            }
        }

        private static DateTimeOffset? GetUserSessionExpiration(SignInCommand command, UserSessionOptions userSessionOptions)
        {
            if (command.Persistent && userSessionOptions.Expiration.HasValue)
                return command.Creation.AddSeconds(userSessionOptions.Expiration.Value);

            return null;
        }

        private async Task AssertAuthenticationEnabledAsync(CancellationToken ct)
        {
            var authenticationOptions = await _configuration.GetAuthenticationOptionsAsync(ct);

            if (!authenticationOptions.Enabled)
            {
                throw new BusinessException("Authentication is not enabled for this environment.")
                    .WithCode("GYORHL")
                    .WithUserMessage("Este método de início de sessão não está disponível.");
            }
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
