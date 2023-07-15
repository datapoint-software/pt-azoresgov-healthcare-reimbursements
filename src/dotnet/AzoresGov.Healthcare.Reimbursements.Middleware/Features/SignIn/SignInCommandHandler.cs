using AzoresGov.Healthcare.Reimbursements.Configuration;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Configuration;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ClaimTypes = AzoresGov.Healthcare.Reimbursements.ClaimTypes;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommandHandler : ICommandHandler<SignInCommand, SignInResult>
    {
        private const string GenericUserMessage = "Este endereço de correio eletrónico e palavra-passe não correspondem a um utilizador existente.";

        private readonly IConfiguration _configuration;

        private readonly IEntityRepository _entities;

        private readonly IPermissionRepository _permissions;

        private readonly IRolePermissionRepository _rolePermissions;

        private readonly IUserAgentRepository _userAgents;

        private readonly IUserEmailAddressRepository _userEmailAddresses;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserEntityPermissionRepository _userEntityPermissions;

        private readonly IUserEntityRoleRepository _userEntityRoles;

        private readonly IUserPasswordRepository _userPasswords;

        private readonly IUserPermissionRepository _userPermissions;

        private readonly IUserRoleRepository _userRoles;

        private readonly IUserRepository _users;

        private readonly IUserSessionRepository _userSessions;

        public SignInCommandHandler(
            IConfiguration configuration, 
            IEntityRepository entities, 
            IPermissionRepository permissions, 
            IRolePermissionRepository rolePermissions, 
            IUserAgentRepository userAgents, 
            IUserEmailAddressRepository userEmailAddresses, 
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
            _userEmailAddresses = userEmailAddresses;
            _userEntities = userEntities;
            _userEntityPermissions = userEntityPermissions;
            _userEntityRoles = userEntityRoles;
            _userPasswords = userPasswords;
            _userPermissions = userPermissions;
            _userRoles = userRoles;
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

            var timeBasedBruteforcePreventionDelayTask = Task.Delay(7500);

            var handleSignInCommandTask = HandleSignInCommandAsync(
                command, 
                userSessionOptions,
                ct);

            await Task.WhenAll(
                timeBasedBruteforcePreventionDelayTask,
                handleSignInCommandTask);

            return handleSignInCommandTask.Result;
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
                userSession,
                userSessionOptions);

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

            return new SignInResult(
                entities
                    .Select(e => new SignInEntityResult(
                        e.Value.PublicId,
                        (userEntityPermissions[e.Key] ?? Array.Empty<UserEntityPermissionEntity>())
                            .Where(uep => !userPermissions.ContainsKey(uep.PermissionId))
                            .Where(uep => !userRoles.Any(ur => (rolePermissions[ur.RoleId]?.Any(rp => rp.PermissionId == uep.PermissionId) ?? false)))
                            .Where(uep => uep.Granted)
                            .Select(uep => new SignInPermissionResult(
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
                    .Select(pid => new SignInPermissionResult(
                        permissions[pid].PublicId,
                        permissions[pid].Name))
                    .ToArray(),
                new SignInUserResult(
                    user.PublicId,
                    user.RowVersionId,
                    user.Name),
                new SignInUserSessionResult(
                    userSession.PublicId,
                    userSession.RowVersionId,
                    userSessionExpiration));
        }

        private static DateTimeOffset? GetUserSessionExpiration(SignInCommand command, UserSessionEntity userSession, UserSessionOptions userSessionOptions)
        {
            if (command.Persistent && userSessionOptions.Expiration.HasValue)
                return command.Creation.AddSeconds(userSessionOptions.Expiration.Value);

            return null;
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
