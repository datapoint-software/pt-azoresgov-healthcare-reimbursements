using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Management;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInOptionsQueryHandler : IQueryHandler<SignInOptionsQuery, SignInOptionsResult>
    {
        private readonly IParameterManager _parameters;

        public SignInOptionsQueryHandler(IParameterManager parameters)
        {
            _parameters = parameters;
        }

        public async Task<SignInOptionsResult> HandleQueryAsync(SignInOptionsQuery query, CancellationToken ct)
        {
            var basicAuthenticationEnabled = await _parameters.GetBasicAuthenticationEnabledAsync(ct);

            if (!basicAuthenticationEnabled)
                throw new InvalidOperationException("No authentication methods available.")
                    .WithErrorCode("NOAUTHM")
                    .WithErrorMessage("Os métodos de autenticação não estão disponíveis para este ambiente.");

            return new SignInOptionsResult(
                new SignInOptionsMethodsResult(
                    new SignInOptionsMethodsBasicResult(
                        await _parameters.GetBasicAuthenticationPersistentSessionsEnabledAsync(ct))));
        }
    }
}
