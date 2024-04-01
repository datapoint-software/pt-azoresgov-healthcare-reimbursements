using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Api.Identity;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    [Route("/api/features/identity")]
    public sealed class IdentityFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public IdentityFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<IdentityFeatureRefreshResultModel> RefreshAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<IdentityFeatureRefreshCommand, IdentityFeatureRefreshResult>(
                new IdentityFeatureRefreshCommand(
                    User.GetId(),
                    User.GetSessionId()),
                ct);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new Claim[]
                        {
                            new (ClaimTypes.Sid, result.UserSession.Id.ToString()),
                            new (ClaimTypes.NameIdentifier, result.User.Id.ToString()),
                            new (ClaimTypes.Name, result.User.Name),
                            new (ClaimTypes.Email, result.User.EmailAddress)
                        },
                        "Basic",
                        ClaimTypes.Name,
                        ClaimTypes.Role)),
                new AuthenticationProperties()
                {
                    ExpiresUtc = result.UserSession.Expiration
                });

            return new IdentityFeatureRefreshResultModel(
                result.User.Id,
                result.User.RowVersionId,
                result.User.Name,
                result.User.EmailAddress,
                result.UserSession.Expiration);
        }
    }
}
