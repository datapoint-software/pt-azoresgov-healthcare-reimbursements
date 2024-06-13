using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.GenericSignIn;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.GenericSignIn
{
    [Route("/api/features/generic-sign-in")]
    public sealed class GenericSignInFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public GenericSignInFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<GenericSignInFeatureOptionsModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<GenericSignInFeatureOptionsQuery, GenericSignInFeatureOptions>(
                new GenericSignInFeatureOptionsQuery(),
                ct);

            return new GenericSignInFeatureOptionsModel(
                result.PersistentSessionsEnabled);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<GenericSignInFeatureSignInResultModel> SignInAsync(
            [FromBody] GenericSignInFeatureSignInModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<GenericSignInFeatureSignInCommand, GenericSignInFeatureSignInResult>(
                new GenericSignInFeatureSignInCommand(
                    HttpContext.Request.Headers.UserAgent.ToString()!,
                    HttpContext.Connection.RemoteIpAddress!,
                    model.EmailAddress,
                    model.Password,
                    model.Persistent),
                ct);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsPrincipalHelper.CreateUserClaimsPrincipal(
                    "Basic",
                    result.User.Id,
                    result.UserSession.Id,
                    result.User.Name,
                    result.User.EmailAddress,
                    result.UserRoles.Select(ur => ur.Nature).ToArray()),
                new AuthenticationProperties()
                {
                    ExpiresUtc = result.UserSession.Expiration
                });

            return new GenericSignInFeatureSignInResultModel(
                result.User.Id,
                result.User.RowVersionId,
                result.User.Name,
                result.User.EmailAddress,
                result.UserSession.Expiration,
                result.UserRoles.Select(e => e.Nature).ToArray());
        }
    }
}
