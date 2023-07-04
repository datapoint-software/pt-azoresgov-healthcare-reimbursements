using AzoresGov.Healthcare.Reimbursements.Configuration.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Configuration
{
    public sealed class UserPasswordHashConfigurationHandler : IConfigurationHandler<UserPasswordHashOptions>
    {
        private readonly IParameterRepository _parameters;

        public UserPasswordHashConfigurationHandler(IParameterRepository parameters)
        {
            _parameters = parameters;
        }

        public async Task<UserPasswordHashOptions> GetOptionsAsync(CancellationToken ct)
        {
            var parameter = await _parameters.GetByNameAsync(ParameterNames.UserPasswordHashWorkFactor, ct);

            if (!parameter.TryGetValue<int>(out var workFactor))
                workFactor = 14;

            return new UserPasswordHashOptions(workFactor);
        }
    }
}
