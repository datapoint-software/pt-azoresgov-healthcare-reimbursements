using Datapoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Handlers
{
    internal sealed class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged)
            {
                throw new AuthenticationException("Authorization has been challanged.")
                    .WithCode("NV7XZZ");
            }

            if (authorizeResult.Forbidden)
            {
                throw new AuthorizationException("Authorization middleware has forbidden access to the requested resource.")
                    .WithCode("HMZKC7");
            }

            if (!authorizeResult.Succeeded)
            {
                throw new InvalidOperationException("Authorization middleware still failed despite not having challanged or forbidden access to the request.")
                    .WithCode("M0NNN0");
            }

            return next(context);
        }
    }
}
