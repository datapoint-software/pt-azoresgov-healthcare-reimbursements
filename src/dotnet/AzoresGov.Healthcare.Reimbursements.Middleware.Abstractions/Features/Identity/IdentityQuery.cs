using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityQuery : Query<IdentityResult>
    {
        public IdentityQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
