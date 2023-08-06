using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.LogicalAreas;
using Datapoint;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Managers
{
    /// <summary>
    /// Safely manages authorization checks for all users taking
    /// into account his entities, roles and permissions at all levels.
    /// </summary>
    public sealed class AuthorizationManager
    {
        private readonly IAuthorizationLogicalArea _authorization;

        private readonly IDistributedCache _distributedCache;

        private readonly ILogger<AuthorizationManager> _logger;

        /// <summary>
        /// Creates a new authorization manager.
        /// </summary>
        /// <param name="authorization">The authorization logical area.</param>
        /// <param name="distributedCache">The distributed cache.</param>
        /// <param name="logger">The logger.</param>
        public AuthorizationManager(IAuthorizationLogicalArea authorization, IDistributedCache distributedCache, ILogger<AuthorizationManager> logger)
        {
            _authorization = authorization;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        /// <summary>
        /// Checks if a permission has been granted to a user.
        /// 
        /// A permission is granted if it is assigned to the user
        /// directly or through a system-wide role.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant status.</returns>
        public Task<bool> AuthorizeAsync(PermissionEntity permission, UserEntity user, CancellationToken ct) => 
            
            AuthorizeAsync(
                permission.Id,
                user.Id,
                true,
                ct);

        /// <summary>
        /// Checks if a permission has been granted for a user
        /// based on a specific entity.
        /// 
        /// A permission is granted if it is assigned to the user directly,
        /// through a system-wide role, through an entity or one of his
        /// roles within the scope of that entity.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="user">The user.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant status.</returns>
        public Task<bool> AuthorizeAsync(PermissionEntity permission, UserEntity user, EntityEntity entity, CancellationToken ct) => 
            
            AuthorizeAsync(
                permission.Id,
                user.Id,
                entity.Id,
                true,
                ct);

        /// <summary>
        /// Checks if a permission has been granted to a user.
        /// 
        /// A permission is granted if it is assigned to the user
        /// directly or through a system-wide role.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant status.</returns>
        public async Task<bool> AuthorizeAsync(string permissionName, UserEntity user, CancellationToken ct)
        {
            var permissionId = await GetPermissionIdAsync(
                permissionName,
                ct);

            return await AuthorizeAsync(
                permissionId,
                user.Id,
                true,
                ct);
        }

        /// <summary>
        /// Checks if a permission has been granted to a user either
        /// at a user level or to any of the available entities.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant status.</returns>
        public Task<bool> AuthorizeWithAnyEntityAsync(PermissionEntity permission, UserEntity user, CancellationToken ct) => 
            
            AuthorizeWithAnyEntityAsync(
                permission.Id,
                user.Id,
                true,
                ct);

        /// <summary>
        /// Checks if a permission has been granted to a user either
        /// at a user level or to any of the available entities.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant status.</returns>
        public async Task<bool> AuthorizeWithAnyEntityAsync(string permissionName, UserEntity user, CancellationToken ct)
        {
            var permissionId = await GetPermissionIdAsync(permissionName, ct);

            return await AuthorizeWithAnyEntityAsync(
                permissionId,
                user.Id,
                true,
                ct);
        }

        /// <summary>
        /// Checks if a permission has been granted for a user
        /// based on a specific entity.
        /// 
        /// A permission is granted if it is assigned to the user directly,
        /// through a system-wide role, through an entity or one of his
        /// roles within the scope of that entity.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <param name="user">The user.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant status.</returns>
        public async Task<bool> AuthorizeAsync(string permissionName, UserEntity user, EntityEntity entity, CancellationToken ct)
        {
            var permissionId = await GetPermissionIdAsync(
                permissionName,
                ct);

            return await AuthorizeAsync(
                permissionId,
                user.Id,
                entity.Id,
                true,
                ct);
        }

        /// <summary>
        /// Gets the number of entities a user was granted a specific
        /// permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant count.</returns>
        public Task<int> CountUserEntityPermissionGrantsAsync(PermissionEntity permission, UserEntity user, CancellationToken ct) =>
            
            CountUserEntityPermissionGrantsAsync(
                permission.Id,
                user.Id,
                ct);

        /// <summary>
        /// Gets the number of entities a user was granted a specific
        /// permission.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the permission grant count.</returns>
        public async Task<int> CountUserEntityPermissionGrantsAsync(string permissionName, UserEntity user, CancellationToken ct)
        {
            var permissionId = await GetPermissionIdAsync(
                permissionName,
                ct);

            return await CountUserEntityPermissionGrantsAsync(
                permissionId,
                user.Id,
                ct);
        }

        /// <summary>
        /// Populates the distributed user authorization cache.
        /// 
        /// This method should be called at sign in or during identity
        /// refresh, ensuring cache is ready before any authorization checks
        /// take place.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task.</returns>
        public Task PopulateAsync(UserEntity user, CancellationToken ct) =>
            
            PopulateAsync(
                user.Id,
                ct);

        private async Task<bool> AuthorizeAsync(long permissionId, long userId, bool populate, CancellationToken ct)
        {
            // Check if the user permission grant has been
            // set at the user level.
            var userPermissionGrantCacheKey = CreateUserPermissionGrantCacheKey(
                permissionId,
                userId);

            var result = await ReadBooleanOrDefaultAsync(
                userPermissionGrantCacheKey,
                ct);

            if (result.HasValue)
                return result.Value;

            if (populate)
            {
                // It seems the authorization cache has been missed
                // and we should populate it before trying this again.
                _logger.LogWarning("An authorization distributed cache hit has been missed.");

                await PopulateAsync(userId, ct);

                return await AuthorizeAsync(
                    permissionId,
                    userId,
                    false,
                    ct);
            }

            // It seems the authorization cache has been missed after
            // being populated, which means something went very wrong.
            throw new InvalidOperationException("A permission is missing from the authorization distributed cache source.")
                .WithCode("DCAUTH");
        }

        private async Task<bool> AuthorizeAsync(long permissionId, long userId, long entityId, bool populate, CancellationToken ct)
        {
            // Check if the user permission grant has been
            // set at the entity level.
            var userEntityPermissionGrantCacheKey = CreateUserEntityPermissionGrantCacheKey(
                permissionId,
                userId,
                entityId);

            var result = await ReadBooleanOrDefaultAsync(
                userEntityPermissionGrantCacheKey,
                ct);

            if (result.HasValue)
                return result.Value;

            // Check if the permission grant has been set
            // at the user level.
            result = await ReadBooleanOrDefaultAsync(
                CreateUserPermissionGrantCacheKey(
                    permissionId,
                    userId),
                ct);

            if (result.HasValue)
            {
                // We can write this to cache to avoid having to
                // do two round-trips in the future.
                await WriteBooleanAsync(
                    userEntityPermissionGrantCacheKey,
                    result.Value,
                    ct);

                return result.Value;
            }

            if (populate)
            {
                // It seems the authorization cache has been missed
                // and we should populate it before trying this again.
                _logger.LogWarning("An authorization distributed cache hit has been missed.");

                await PopulateAsync(userId, ct);

                return await AuthorizeAsync(
                    permissionId,
                    userId,
                    entityId,
                    false,
                    ct);
            }

            // It seems the authorization cache has been missed after
            // being populated, which means something went very wrong.
            throw new InvalidOperationException("A permission is missing from the authorization distributed cache source.")
                .WithCode("DCAUTH");
        }

        private async Task<bool> AuthorizeWithAnyEntityAsync(long permissionId, long userId, bool populate, CancellationToken ct)
        {
            var result = await ReadBooleanOrDefaultAsync(
                CreateUserEntityPermissionGrantCacheKey(permissionId, userId),
                ct);

            if (result.HasValue)
                return result.Value;

            if (populate)
            {
                // It seems the authorization cache has been missed
                // and we should populate it before trying this again.
                _logger.LogWarning("An authorization distributed cache hit has been missed.");

                await PopulateAsync(userId, ct);

                return await AuthorizeWithAnyEntityAsync(
                    permissionId,
                    userId,
                    false,
                    ct);
            }

            // It seems the authorization cache has been missed after
            // being populated, which means something went very wrong.
            throw new InvalidOperationException("A permission is missing from the authorization distributed cache source.")
                .WithCode("DCAUTH");
        }

        private Task<int> CountUserEntityPermissionGrantsAsync(long permissionId, long userId, CancellationToken ct) => 
            
            _authorization.CountUserEntityPermissionGrantsAsync(
                permissionId,
                userId,
                ct);

        private async Task PopulateAsync(long userId, CancellationToken ct)
        {
            // Go through the user level grants and set
            // them accordingly.
            var userPermissionGrants = await _authorization.GetAllUserPermissionGrantsByUserIdAsync(
                userId,
                ct);

            foreach (var userPermissionGrant in userPermissionGrants)
            {
                await WriteBooleanAsync(
                    CreateUserPermissionGrantCacheKey(
                        userPermissionGrant.PermissionId,
                        userPermissionGrant.UserId),
                    userPermissionGrant.Granted,
                    ct);

                await WriteBooleanAsync(
                    CreateUserEntityPermissionGrantCacheKey(
                        userPermissionGrant.PermissionId,
                        userPermissionGrant.UserId),
                    userPermissionGrant.Granted,
                    ct);
            }

            // Go through the entity level grants, skipping any
            // permissions already set at user level.
            var userEntityPermissionGrants = await _authorization.GetAllUserEntityPermissionGrantsByUserIdExceptWhenPermissionIdAsync(
                userId,
                userPermissionGrants.Select(upg => upg.PermissionId),
                ct);

            foreach (var userEntityPermissionGrant in userEntityPermissionGrants)
            {
                await WriteBooleanAsync(
                    CreateUserEntityPermissionGrantCacheKey(
                        userEntityPermissionGrant.PermissionId,
                        userEntityPermissionGrant.UserId,
                        userEntityPermissionGrant.EntityId),
                    userEntityPermissionGrant.Granted,
                    ct);
            }

            // We will also cache any entity level grants so we can
            // later authorize accross any of the user entities.
            var userEntityPermissionTransversalGrants = userEntityPermissionGrants
                .Where(uepg => uepg.Granted)
                .Select(uepg => uepg.PermissionId)
                .Distinct();

            foreach (var permissionId in userEntityPermissionTransversalGrants)
            {
                await WriteBooleanAsync(
                    CreateUserEntityPermissionGrantCacheKey(
                        permissionId,
                        userId),
                    true,
                    ct);
            }

            var userEntityPermissionTransversalRevokes = userEntityPermissionGrants
                .Select(uepg => uepg.PermissionId)
                .Distinct()
                .Where(pid => userEntityPermissionTransversalGrants.Contains(pid) == false);

            foreach (var permissionId in userEntityPermissionTransversalRevokes)
            {
                await WriteBooleanAsync(
                    CreateUserEntityPermissionGrantCacheKey(
                        permissionId,
                        userId),
                    false,
                    ct);
            }
        }

        private async Task<long> GetPermissionIdAsync(string permissionName, CancellationToken ct)
        {
            // Attempt to get the permission identifier from
            // the distributed cache in order to prevent having
            // to hit the database.
            var permissionCacheKey = CreatePermissionCacheKey(permissionName);

            var result = await ReadInt64OrDefaultAsync(
                permissionCacheKey,
                ct);

            if (result.HasValue)
                return result.Value;

            // Save the permission identifier to cache to avoid
            // hitting the database in the future.
            result = await _authorization.GetPermissionIdAsync(
                permissionName,
                ct);

            await WriteInt64Async(
                permissionCacheKey,
                result.Value,
                ct);

            return result.Value;
        }

        #region Distributed cache

        private async Task<bool?> ReadBooleanOrDefaultAsync(string key, CancellationToken ct)
        {
            var buffer = await _distributedCache.GetAsync(key, ct);

            if (buffer == null)
                return null;

            return BitConverter.ToBoolean(buffer);
        }

        private async Task<long?> ReadInt64OrDefaultAsync(string key, CancellationToken ct)
        {
            var buffer = await _distributedCache.GetAsync(key, ct);

            if (buffer == null)
                return null;

            return BitConverter.ToInt64(buffer);
        }

        private Task WriteBooleanAsync(string key, bool value, CancellationToken ct) => 
            
            _distributedCache.SetAsync(
                key,
                BitConverter.GetBytes(value),
                DefaultDistributedCacheEntryOptions,
                ct);

        private Task WriteInt64Async(string key, long value, CancellationToken ct) => 
            
            _distributedCache.SetAsync(
                key,
                BitConverter.GetBytes(value),
                ct);

        #endregion

        #region Statics

        private static readonly DistributedCacheEntryOptions DefaultDistributedCacheEntryOptions = new ()
        {
            SlidingExpiration = TimeSpan.FromHours(1)
        };

        private static string CreatePermissionCacheKey(string permissionName) =>
            $"authorization:p:{permissionName}";

        private static string CreateUserEntityPermissionGrantCacheKey(long permissionId, long userId, long entityId) =>

            $"authorization:uep:{permissionId}:{userId}:{entityId}";

        private static string CreateUserEntityPermissionGrantCacheKey(long permissionId, long userId) =>

            $"authorization:uep:{permissionId}:{userId}:any";

        private static string CreateUserPermissionGrantCacheKey(long permissionId, long userId) =>

            $"authorization:ue:{permissionId}:{userId}";
        #endregion
    }
}
