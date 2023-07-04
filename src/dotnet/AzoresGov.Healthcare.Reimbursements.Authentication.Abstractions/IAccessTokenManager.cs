using System;
using System.Security.Claims;

namespace AzoresGov.Healthcare.Reimbursements.Authentication
{
    public interface IAccessTokenManager
    {
        string CreateAccessToken(ClaimsPrincipal principal, DateTimeOffset issued, int expiration);
    }
}
