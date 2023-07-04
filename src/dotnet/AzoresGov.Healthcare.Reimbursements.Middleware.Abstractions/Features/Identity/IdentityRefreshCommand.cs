using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshCommand : Command<IdentityRefreshResult>
    {
        public IdentityRefreshCommand(Guid userSessionId, Guid userSessionRowVersionId, string userSessionSecret)
        {
            UserSessionId = userSessionId;
            UserSessionRowVersionId = userSessionRowVersionId;
            UserSessionSecret = userSessionSecret;
        }

        public Guid UserSessionId { get; }

        public Guid UserSessionRowVersionId { get; }

        public string UserSessionSecret { get; }
    }
}
