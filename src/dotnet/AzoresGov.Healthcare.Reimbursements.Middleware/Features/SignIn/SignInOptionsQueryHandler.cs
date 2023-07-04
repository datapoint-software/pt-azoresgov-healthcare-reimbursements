using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using Datapoint.Configuration;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsQueryHandler : IQueryHandler<SignInOptionsQuery, SignInOptionsResult>
    {
        private readonly IConfiguration _configuration;

        public SignInOptionsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<SignInOptionsResult> HandleQueryAsync(SignInOptionsQuery query, CancellationToken ct)
        {
            var authenticationOptions = await _configuration.GetAuthenticationOptionsAsync(ct);

            var userSessionOptions = await _configuration.GetUserSessionOptionsAsync(ct);

            return new SignInOptionsResult(
                new SignInAuthenticationOptionsResult(
                    enabled: authenticationOptions.Enabled,
                    persistentEnabled: userSessionOptions is null));
        }
    }
}
