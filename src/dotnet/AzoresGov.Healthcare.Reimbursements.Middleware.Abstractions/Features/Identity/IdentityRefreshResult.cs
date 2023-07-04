using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshResult
    {
        public IdentityRefreshResult(Guid userSessionRowVersionId, string userSessionSecret)
        {
            UserSessionRowVersionId = userSessionRowVersionId;
            UserSessionSecret = userSessionSecret;
        }

        public Guid UserSessionRowVersionId { get; }

        public string UserSessionSecret { get; }
    }
}