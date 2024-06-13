using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.CoreIdentity
{
    [Route("/api/features/core-identity")]
    public sealed class CoreIdentityFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public CoreIdentityFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("refresh")]
        public async Task<CoreIdentityFeatureRefreshResultModel> RefreshAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<CoreIdentityFeatureRefreshCommand, CoreIdentityFeatureRefreshResult>(
                new CoreIdentityFeatureRefreshCommand(
                    User.GetId(),
                    User.GetSessionId()),
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

            return new CoreIdentityFeatureRefreshResultModel(
                result.User.Id,
                result.User.RowVersionId,
                result.User.Name,
                result.User.EmailAddress,
                result.UserSession.Expiration,
                result.UserRoles.Select(r => r.Nature).ToArray());
        }
    }
}
