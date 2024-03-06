using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCaptureOptionsPatientResult
    {
        public ProcessCaptureOptionsPatientResult(Guid id, Guid rowVersionId, string name, DateTimeOffset? birth, Gender? gender, string healthNumber, string taxNumber, string addressLine1, string? addressLine2, string? addressLine3, string? postalCode, string? postalCodeArea, string? emailAddress, string? faxNumber, string? mobileNumber, string? phoneNumber, DateTimeOffset? death)
        {
            Id = id;
            RowVersionId = rowVersionId;
            Name = name;
            Birth = birth;
            Gender = gender;
            HealthNumber = healthNumber;
            TaxNumber = taxNumber;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            PostalCode = postalCode;
            PostalCodeArea = postalCodeArea;
            EmailAddress = emailAddress;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
            Death = death;
        }

        public Guid Id { get; }

        public Guid RowVersionId { get; }

        public string Name { get; }

        public DateTimeOffset? Birth { get; }

        public Gender? Gender { get; }

        public string HealthNumber { get; }

        public string TaxNumber { get; }

        public string AddressLine1 { get; }

        public string? AddressLine2 { get; }

        public string? AddressLine3 { get; }

        public string? PostalCode { get; }

        public string? PostalCodeArea { get; }

        public string? EmailAddress { get; }

        public string? FaxNumber { get; }

        public string? MobileNumber { get; }

        public string? PhoneNumber { get; }

        public DateTimeOffset? Death { get; }
    }
}