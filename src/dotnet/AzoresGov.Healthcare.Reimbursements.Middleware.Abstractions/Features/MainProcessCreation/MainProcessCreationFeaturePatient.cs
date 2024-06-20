using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeaturePatient
    {
        public MainProcessCreationFeaturePatient(Guid id, Guid rowVersionId, Guid entityId, string number, string? taxNumber, string name, DateTimeOffset? birth, DateTimeOffset? death, string? faxNumber, string? mobileNumber, string? phoneNumber, string? emailAddress, bool external)
        {
            Id = id;
            RowVersionId = rowVersionId;
            EntityId = entityId;
            Number = number;
            TaxNumber = taxNumber;
            Name = name;
            Birth = birth;
            Death = death;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            External = external;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public Guid EntityId { get; }

        public string Number { get; }

        public string? TaxNumber { get; }

        public string Name { get; }

        public DateTimeOffset? Birth { get; }

        public DateTimeOffset? Death { get; }

        public string? FaxNumber { get; }

        public string? MobileNumber { get; }

        public string? PhoneNumber { get; }

        public string? EmailAddress { get; }

        public bool External { get; }
    }
}