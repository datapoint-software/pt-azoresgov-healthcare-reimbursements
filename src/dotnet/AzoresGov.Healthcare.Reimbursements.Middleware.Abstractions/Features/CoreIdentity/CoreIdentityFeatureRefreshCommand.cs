using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity
{
    public sealed class CoreIdentityFeatureRefreshCommand : Command<CoreIdentityFeatureRefreshResult>
    {
        public CoreIdentityFeatureRefreshCommand(Guid userId, Guid userSessionId)
        {
            UserId = userId;
            UserSessionId = userSessionId;
        }

        public Guid UserId { get; }

        public Guid UserSessionId { get; }
    }
}
