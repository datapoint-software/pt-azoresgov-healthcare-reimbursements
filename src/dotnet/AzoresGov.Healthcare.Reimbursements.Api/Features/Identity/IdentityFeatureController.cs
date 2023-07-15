using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Authentication;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
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

        [HttpPost("refresh")]
        public async Task<IdentityRefreshResultModel> RefreshAsync(CancellationToken ct)
        {
            var command = new IdentityRefreshCommand(
                User.GetSessionId(),
                User.GetSessionRowVersionId(),
                User.GetSessionPersistent());

            var result = await _mediator.HandleCommandAsync<IdentityRefreshCommand, IdentityRefreshResult>(
                command,
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

            return new IdentityRefreshResultModel(
                result.Entities
                    .Select(e => new IdentityRefreshEntityResultModel(
                        e.Id,
                        e.Permissions
                            .Select(ep => new IdentityRefreshPermissionResultModel(
                                ep.Id,
                                ep.Name))
                            .ToArray()))
                    .ToArray(),
                result.Permissions
                    .Select(p => new IdentityRefreshPermissionResultModel(
                        p.Id,
                        p.Name))
                    .ToArray(),
                new IdentityRefreshUserResultModel(
                    result.User.Id,
                    result.User.Name),
                new IdentityRefreshUserSessionResultModel(
                    result.UserSession.Id));
        }
    }
}
