using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentative
    {
        public MainProcessCaptureFeatureLegalRepresentative(Guid id, Guid rowVersionId, string taxNumber, string name, string? faxNumber, string? mobileNumber, string? phoneNumber, string? emailAddress, string postalAddressArea, string postalAddressAreaCode, string postalAddressLine1, string? postalAddressLine2, string? postalAddressLine3)
        {
            Id = id;
            RowVersionId = rowVersionId;
            TaxNumber = taxNumber;
            Name = name;
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

        public string TaxNumber { get; }

        public string Name { get; }

        public string? FaxNumber { get; }

        public string? MobileNumber { get; }

        public string? PhoneNumber { get; }

        public string? EmailAddress { get; }

        public string PostalAddressArea { get; }

        public string PostalAddressAreaCode { get; }

        public string PostalAddressLine1 { get; }

        public string? PostalAddressLine2 { get; }

        public string? PostalAddressLine3 { get; }
    }
}