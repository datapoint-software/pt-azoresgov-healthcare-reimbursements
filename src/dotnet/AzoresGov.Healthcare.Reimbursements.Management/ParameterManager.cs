using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    public sealed class ParameterManager : IParameterManager
    {
        private readonly IParameterRepository _parameters;

        public ParameterManager(IParameterRepository parameters)
        {
            _parameters = parameters;
        }

        public async Task<int> GetUserPasswordHashWorkFactorAsync(CancellationToken ct) =>

            (await GetValueOrDefaultByNameAsync<int?>("UserPasswordHashWorkFactor", ct)) ?? 14;

        public async Task<bool> GetPersistentSessionsEnabledAsync(CancellationToken ct) =>

            (await GetValueOrDefaultByNameAsync<bool?>("PersistentSessionsEnabled", ct)) ?? false;

        public async Task<int> GetSignInDelayInMillisecondsAsync(CancellationToken ct) =>

            (await GetValueOrDefaultByNameAsync<int?>("SignInDelayInMilliseconds", ct)) ?? 5000;

        public async Task<int> GetUserSessionExpirationInSecondsAsync(CancellationToken ct) =>

            (await GetValueOrDefaultByNameAsync<int?>("UserSessionExpirationInSeconds", ct)) ?? 900;

        private async Task<T?> GetValueOrDefaultByNameAsync<T>(string name, CancellationToken ct)
        {
            var parameter = await _parameters.GetByNameAsync(name, ct);

            if (parameter is null)
                return default;

            return JsonSerializer.Deserialize<T>(parameter.JsonValue);
        }
    }
}
