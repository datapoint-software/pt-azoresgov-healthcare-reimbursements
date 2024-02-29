using System;
using System.Linq;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Api
{
    internal static class ClaimsPrincipalExtensions
    {
        internal static Guid GetId(this ClaimsPrincipal principal) =>

            Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        
        internal static Guid GetSessionId(this ClaimsPrincipal principal) =>

            Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
    }
}
