using AzoresGov.Healthcare.Reimbursements.Configuration.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    public sealed class UserSessionConfigurationHandler : IConfigurationHandler<UserSessionOptions>
    {
        private readonly IParameterRepository _parameters;

        public UserSessionConfigurationHandler(IParameterRepository parameters)
        {
            _parameters = parameters;
        }

        public async Task<UserSessionOptions> GetOptionsAsync(CancellationToken ct)
        {
            var parameter = await _parameters.GetByNameAsync(
                ParameterNames.UserSessionExpiration,
                ct);

            if (!parameter.TryGetValue<int?>(out var expiration))
                expiration = 43200;

            return new UserSessionOptions(expiration);
        }
    }
}
