using AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    [Route("/api/features/sign-in")]
    public sealed class SignInFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public SignInFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<SignInFeatureOptionsModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<SignInFeatureOptionsQuery, SignInFeatureOptions>(
                new SignInFeatureOptionsQuery(),
                ct);

            return new SignInFeatureOptionsModel(
                result.PersistentSessionsEnabled);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public Task<SignInFeatureSignInResultModel> SignInAsync(
            [FromBody] SignInFeatureSignInModel model,
            CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
