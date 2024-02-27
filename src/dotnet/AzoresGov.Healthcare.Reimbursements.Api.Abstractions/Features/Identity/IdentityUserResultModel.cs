using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    public sealed class IdentityUserResultModel
    {
        public IdentityUserResultModel(Guid id, string name, string emailAddress)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
        }

        public Guid Id { get; }

        public string Name { get; }

        public string EmailAddress { get; }
    }
}