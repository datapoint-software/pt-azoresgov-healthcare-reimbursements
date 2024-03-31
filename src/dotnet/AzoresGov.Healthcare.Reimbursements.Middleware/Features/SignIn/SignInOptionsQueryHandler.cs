using AzoresGov.Healthcare.Reimbursements.Management;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsQueryHandler : IQueryHandler<SignInFeatureOptionsQuery, SignInFeatureOptions>
    {
        private readonly IParameterManager _parameters;

        public SignInOptionsQueryHandler(IParameterManager parameters)
        {
            _parameters = parameters;
        }

        public async Task<SignInFeatureOptions> HandleQueryAsync(SignInFeatureOptionsQuery query, CancellationToken ct)
        {
            var persistentSessionsEnabled = await _parameters.GetPersistentSessionsEnabledAsync(ct);

            return new SignInFeatureOptions(
                persistentSessionsEnabled);
        }
    }
}
