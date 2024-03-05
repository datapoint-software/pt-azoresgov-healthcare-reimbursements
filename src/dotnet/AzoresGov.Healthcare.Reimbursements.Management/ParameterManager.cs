using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public sealed class ParameterManager : IParameterManager
    {
        // Parameter names

        private const string BasicAuthenticationDelay = nameof(BasicAuthenticationDelay);

        private const string BasicAuthenticationEnabled = nameof(BasicAuthenticationEnabled);

        private const string BasicAuthenticationPersistentSessionsEnabled = nameof(BasicAuthenticationPersistentSessionsEnabled);

        private const string ProcessExpiration = nameof(ProcessExpiration);

        private const string UserPasswordHashWorkFactor = nameof(UserPasswordHashWorkFactor);

        private const string UserSessionExpiration = nameof(UserSessionExpiration);

        // Cache entry names

        private const string ParameterCacheEntryNamePrefix = "azoresgov-healthcare-reimbursements:parameters:";

        private readonly IDistributedCache _distributedCache;

        private readonly IParameterRepository _parameters;

        public ParameterManager(IDistributedCache distributedCache, IParameterRepository parameters)
        {
            _distributedCache = distributedCache;
            _parameters = parameters;
        }

        public async Task<int> GetBasicAuthenticationDelayInMillisecondsAsync(CancellationToken ct) =>

            await GetValueOrDefaultAsync<int?>(BasicAuthenticationDelay, ct) ?? 7500;

        public async Task<bool> GetBasicAuthenticationEnabledAsync(CancellationToken ct) =>

            await GetValueOrDefaultAsync<bool?>(BasicAuthenticationEnabled, ct) ?? true;

        public async Task<bool> GetBasicAuthenticationPersistentSessionsEnabledAsync(CancellationToken ct) =>

            await GetValueOrDefaultAsync<bool?>(BasicAuthenticationPersistentSessionsEnabled, ct) ?? false;

        public async Task<int> GetProcessExpirationInDaysAsync(CancellationToken ct) =>

            await GetValueOrDefaultAsync<int?>(ProcessExpiration, ct) ?? 7;

        public async Task<int> GetUserPasswordHashWorkFactorAsync(CancellationToken ct) =>

            await GetValueOrDefaultAsync<int?>(UserPasswordHashWorkFactor, ct) ?? 14;
        
        public async Task<int> GetUserSessionExpirationInSecondsAsync(CancellationToken ct) => 
            
            await GetValueOrDefaultAsync<int?>(UserSessionExpiration, ct) ?? 900;

        private async Task<TValue?> GetValueOrDefaultAsync<TValue>(string parameterName, CancellationToken ct)
        {
            var parameterCacheEntryName = $"{ParameterCacheEntryNamePrefix}{parameterName}";

            var buffer = await _distributedCache.GetAsync(parameterCacheEntryName, ct);

            if (buffer is not null)
                return JsonSerializer.Deserialize<TValue>(buffer);

            var parameter = await _parameters.GetByNameAsync(parameterName, ct);

            if (parameter is null)
            {
                await _distributedCache.SetAsync(
                    parameterCacheEntryName, 
                    JsonSerializer.SerializeToUtf8Bytes<TValue?>(default!),
                    ct);

                return default!;
            }

            var result = JsonSerializer.Deserialize<TValue?>(parameter.JsonValue);

            await _distributedCache.SetAsync(
                parameterCacheEntryName,
                JsonSerializer.SerializeToUtf8Bytes(result),
                ct);

            return result;
        }
    }
}
