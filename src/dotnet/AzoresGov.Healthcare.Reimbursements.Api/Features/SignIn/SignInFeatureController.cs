using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Authentication;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn;
using Datapoint.Mediator;
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
        private readonly IAccessTokenManager _accessTokenManager;

        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public SignInFeatureController(IAccessTokenManager accessTokenManager, IConfiguration configuration, IMediator mediator)
        {
            _accessTokenManager = accessTokenManager;
            _configuration = configuration;
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
            var command = new SignInCommand(
                HttpContext.Request.GetUserAgent(),
                HttpContext.Request.GetRemoteAddress(),
                model.EmailAddress,
                model.Password);

            var result = await _mediator.HandleCommandAsync<SignInCommand, SignInResult>(
                command,
                ct);

            var principal = ClaimsPrincipalHelper.CreateClaimsPrincipal(
                result.UserId,
                result.UserRowVersionId,
                result.UserSessionId,
                result.UserSessionRowVersionId);

            var accessTokenExpiration = _configuration.GetAccessTokenExpiration();

            var accessToken = _accessTokenManager.CreateAccessToken(
                principal,
                command.Creation,
                accessTokenExpiration);

            return new SignInResultModel(
                accessToken,
                accessTokenExpiration,
                result.UserSessionSecret);
        }

        [AllowAnonymous]
        [HttpGet]
        public object Test()
        {
            return new
            {
                IsAuthenticated = HttpContext.User.Identity?.IsAuthenticated ?? false,
                Claims = HttpContext.User.Claims.Select(e => new { e.Type, e.Value }).ToArray()
            };
        }
    }
}
