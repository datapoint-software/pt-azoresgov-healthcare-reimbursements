using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Authentication;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using SignInResult = AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn.SignInResult;

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
        [HttpGet("options")]
        public async Task<SignInOptionsResultModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<SignInOptionsQuery, SignInOptionsResult>(
                new SignInOptionsQuery(),
                ct);

            return new SignInOptionsResultModel(
                new SignInAuthenticationOptionsResultModel(
                    result.Authentication.Enabled,
                    result.Authentication.PersistentEnabled));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<SignInResultModel> SignInAsync([FromBody] SignInModel model, CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<SignInCommand, SignInResult>(
                new SignInCommand(
                    HttpContext.Request.GetUserAgent(),
                    HttpContext.Request.GetRemoteAddress(),
                    model.EmailAddress,
                    model.Password,
                    model.Persistent),
                ct);

            var principal = ClaimsPrincipalHelper.CreateClaimsPrincipal(
                result.User.Id,
                result.User.RowVersionId,
                result.UserSession.Id,
                result.UserSession.RowVersionId,
                result.UserSession.Expiration);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties()
                {
                    IsPersistent = result.UserSession.Expiration.HasValue,
                    ExpiresUtc = result.UserSession.Expiration
                });

            return new SignInResultModel(
                result.Entities
                    .Select(e => new SignInEntityResultModel(
                        e.Id,
                        e.Permissions
                            .Select(ep => new SignInPermissionResultModel(
                                ep.Id,
                                ep.Name))
                            .ToArray()))
                    .ToArray(),
                result.Permissions
                    .Select(p => new SignInPermissionResultModel(
                        p.Id,
                        p.Name))
                    .ToArray(),
                new SignInUserResultModel(
                    result.User.Id,
                    result.User.Name),
                new SignInUserSessionResultModel(
                    result.UserSession.Id));
        }
    }
}
