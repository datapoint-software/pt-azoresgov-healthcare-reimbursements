using AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;

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
        public async Task<SignInFeatureSignInResultModel> SignInAsync(
            [FromBody] SignInFeatureSignInModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<SignInFeatureSignInCommand, SignInFeatureSignInResult>(
                new SignInFeatureSignInCommand(
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

            return new SignInFeatureSignInResultModel(
                result.User.Id,
                result.User.RowVersionId,
                result.User.Name,
                result.User.EmailAddress,
                result.UserSession.Expiration,
                result.UserRoles.Select(e => e.Nature).ToArray());
        }
    }
}
