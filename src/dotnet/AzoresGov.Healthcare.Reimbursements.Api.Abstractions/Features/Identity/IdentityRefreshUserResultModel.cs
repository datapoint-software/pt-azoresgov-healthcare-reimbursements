using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityRefreshUserResultModel
    {
        public IdentityRefreshUserResultModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}