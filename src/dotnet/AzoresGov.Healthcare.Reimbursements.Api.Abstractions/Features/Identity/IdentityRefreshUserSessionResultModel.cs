using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityRefreshUserSessionResultModel
    {
        public IdentityRefreshUserSessionResultModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}