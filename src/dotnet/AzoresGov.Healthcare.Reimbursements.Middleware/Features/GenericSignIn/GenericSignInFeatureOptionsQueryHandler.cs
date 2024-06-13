using AzoresGov.Healthcare.Reimbursements.Management;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.GenericSignIn
{
    public sealed class GenericSignInFeatureOptionsQueryHandler : IQueryHandler<GenericSignInFeatureOptionsQuery, GenericSignInFeatureOptions>
    {
        private readonly IParameterManager _parameters;

        public GenericSignInFeatureOptionsQueryHandler(IParameterManager parameters)
        {
            _parameters = parameters;
        }

        public async Task<GenericSignInFeatureOptions> HandleQueryAsync(GenericSignInFeatureOptionsQuery query, CancellationToken ct)
        {
            var persistentSessionsEnabled = await _parameters.GetPersistentSessionsEnabledAsync(ct);

            return new GenericSignInFeatureOptions(
                persistentSessionsEnabled);
        }
    }
}
