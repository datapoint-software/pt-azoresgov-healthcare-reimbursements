using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityFeatureRefreshCommand : Command<IdentityFeatureRefreshResult>
    {
        public IdentityFeatureRefreshCommand(Guid userId, Guid userSessionId)
        {
            UserId = userId;
            UserSessionId = userSessionId;
        }

        public Guid UserId { get; }

        public Guid UserSessionId { get; }
    }
}
