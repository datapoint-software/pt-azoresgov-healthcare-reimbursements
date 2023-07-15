using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshCommand : Command<IdentityRefreshResult>
    {
        public IdentityRefreshCommand(Guid userSessionId, Guid userSessionRowVersionId, bool persistent)
        {
            UserSessionId = userSessionId;
            UserSessionRowVersionId = userSessionRowVersionId;
            Persistent = persistent;
        }

        public Guid UserSessionId { get; }

        public Guid UserSessionRowVersionId { get; }

        public bool Persistent { get; }
    }
}
