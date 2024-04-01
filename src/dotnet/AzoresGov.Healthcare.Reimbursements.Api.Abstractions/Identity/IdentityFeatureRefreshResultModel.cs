using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Identity
{
    public sealed class IdentityFeatureRefreshResultModel
    {
        public IdentityFeatureRefreshResultModel(Guid id, Guid rowVersionId, string name, string emailAddress, DateTimeOffset? expiration)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Name = name;
            EmailAddress = emailAddress;
            Expiration = expiration;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Name { get; }

        public string EmailAddress { get; }

        public DateTimeOffset? Expiration { get; }
    }
}
