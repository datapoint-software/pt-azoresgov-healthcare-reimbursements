using AzoresGov.Healthcare.Reimbursements.Configuration.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    public sealed class AuthenticationConfigurationHandler : IConfigurationHandler<AuthenticationOptions>
    {
        private readonly IParameterRepository _parameters;

        public AuthenticationConfigurationHandler(IParameterRepository parameters)
        {
            _parameters = parameters;
        }

        public async Task<AuthenticationOptions> GetOptionsAsync(CancellationToken ct)
        {
            var parameter = await _parameters.GetByNameAsync(ParameterNames.AuthenticationEnabled, ct);

            if (!parameter.TryGetValue<bool>(out var enabled))
                enabled = true;

            return new AuthenticationOptions(enabled);
        }
    }
}
