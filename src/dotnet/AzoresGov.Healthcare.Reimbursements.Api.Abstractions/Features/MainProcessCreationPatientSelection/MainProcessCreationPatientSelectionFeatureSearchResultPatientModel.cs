using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchResultPatientModel
    {
        public MainProcessCreationPatientSelectionFeatureSearchResultPatientModel(Guid id, Guid rowVersionId, string number, string? taxNumber, string name, DateTimeOffset? birth, DateTimeOffset? death, bool external, string? faxNumber, string? mobileNumber, string? phoneNumber, string? emailAddress, string? postalAddressArea, string? postalAddressAreaCode, string? postalAddressLine1, string? postalAddressLine2, string? postalAddressLine3)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Number = number;
            TaxNumber = taxNumber;
            Name = name;
            Birth = birth;
            Death = death;
            External = external;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            PostalAddressArea = postalAddressArea;
            PostalAddressAreaCode = postalAddressAreaCode;
            PostalAddressLine1 = postalAddressLine1;
            PostalAddressLine2 = postalAddressLine2;
            PostalAddressLine3 = postalAddressLine3;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Number { get; }

        public string? TaxNumber { get; }

        public string Name { get; }

        public DateTimeOffset? Birth { get; }

        public DateTimeOffset? Death { get; }

        public bool External { get; }

        public string? FaxNumber { get; }

        public string? MobileNumber { get; }

        public string? PhoneNumber { get; }

        public string? EmailAddress { get; }

        public string? PostalAddressArea { get; }

        public string? PostalAddressAreaCode { get; }

        public string? PostalAddressLine1 { get; }

        public string? PostalAddressLine2 { get; }

        public string? PostalAddressLine3 { get; }
    }
}