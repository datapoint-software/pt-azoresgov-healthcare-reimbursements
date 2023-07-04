using AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Authentication;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    [Route("/api/features/identity")]
    public sealed class IdentityFeatureController : Controller
    {
        private readonly IAccessTokenManager _accessTokenManager;

        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public IdentityFeatureController(IAccessTokenManager accessTokenManager, IConfiguration configuration, IMediator mediator)
        {
            _accessTokenManager = accessTokenManager;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("refresh")]
        public async Task<IdentityRefreshResultModel> RefreshAsync([FromBody] IdentityRefreshModel model, CancellationToken ct)
        {
            var command = new IdentityRefreshCommand(
                User.GetSessionId(),
                User.GetSessionRowVersionId(),
                model.RefreshToken);

            var result = await _mediator.HandleCommandAsync<IdentityRefreshCommand, IdentityRefreshResult>(
                command,
                ct);

            var principal = ClaimsPrincipalHelper.CreateClaimsPrincipal(
                User.GetId(),
                User.GetRowVersionId(),
                User.GetSessionId(),
                result.UserSessionRowVersionId);

            var accessTokenExpiration = _configuration.GetAccessTokenExpiration();

            var accessToken = _accessTokenManager.CreateAccessToken(
                principal,
                command.Creation,
                accessTokenExpiration);

            return new IdentityRefreshResultModel(
                accessToken,
                accessTokenExpiration,
                result.UserSessionSecret);
        }
    }
}
