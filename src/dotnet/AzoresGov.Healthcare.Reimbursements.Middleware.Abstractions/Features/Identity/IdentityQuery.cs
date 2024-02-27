using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityQuery : Query<IdentityResult>
    {
        public IdentityQuery(Guid userSessionId)
        {
            UserSessionId = userSessionId;
        }

        public Guid UserSessionId { get; }
    }
}
