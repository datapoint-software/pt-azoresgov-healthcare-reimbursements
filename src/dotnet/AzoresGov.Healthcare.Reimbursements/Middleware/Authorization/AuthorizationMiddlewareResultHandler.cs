using Datapoint;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Authorization
{
    internal sealed class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Challenged)
                throw new AuthenticationException("Authorization has been challanged.");

            if (authorizeResult.Forbidden)
                throw new AuthorizationException("Authorization middleware has forbidden access to the requested resource.");

            return next(context);
        }
    }
}
