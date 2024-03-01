using AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    [Route("/api/features/sign-in")]
    public sealed class SignInController : Controller
    {
        private readonly IMediator _mediator;

        public SignInController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<SignInOptionsResultModel> GetSignInOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<SignInOptionsQuery, SignInOptionsResult>(
                new SignInOptionsQuery(),
                ct);

            return new SignInOptionsResultModel(
                new SignInOptionsMethodsResultModel(
                    result.Methods.Basic is null ? null :
                        new SignInOptionsMethodsBasicResultModel(
                            result.Methods.Basic.PersistentSessionsEnabled)));
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<SignInResultModel> SignInAsync([FromBody] SignInModel model, CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<SignInCommand, Middleware.Features.SignIn.SignInResult>(
                new SignInCommand(
                    HttpContext.Request.Headers.UserAgent!,
                    HttpContext.Connection.RemoteIpAddress!,
                    model.EmailAddress,
                    model.Password,
                    model.Persistent),
                ct);

            var claims = new Claim[ 4 + result.Roles.Count ];
            claims[ 0 ] = new Claim(ClaimTypes.Sid, result.Session.Id.ToString());
            claims[ 1 ] = new Claim(ClaimTypes.NameIdentifier, result.User.Id.ToString());
            claims[ 2 ] = new Claim(ClaimTypes.Name, result.User.Name);
            claims[ 3 ] = new Claim(ClaimTypes.Email, result.User.EmailAddress);

            var i = 3;
            foreach (var role in result.Roles)
                claims[ ++i ] = new Claim(ClaimTypes.Role, role.Id.ToString());

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        claims,
                        "Basic",
                        ClaimTypes.Name,
                        ClaimTypes.Role)),
                new AuthenticationProperties()
                {
                    ExpiresUtc = result.Session.Expiration
                });

            return new SignInResultModel(
                result.Roles
                    .Select(r => new SignInRoleResultModel(
                        r.Id,
                        r.RowVersionId,
                        r.Name))
                    .ToArray(),
                new SignInSessionResultModel(
                    result.Session.Id),
                new SignInUserResultModel(
                    result.User.Id,
                    result.User.Name,
                    result.User.EmailAddress));
        }
    }
}
