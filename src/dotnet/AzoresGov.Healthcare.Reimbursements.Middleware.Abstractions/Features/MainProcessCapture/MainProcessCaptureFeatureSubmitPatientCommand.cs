using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureSubmitPatientCommand : Command<MainProcessCaptureFeatureSubmitPatientResult>
    {
        public MainProcessCaptureFeatureSubmitPatientCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid patientId, Guid patientRowVersionId, string? faxNumber, string? mobileNumber, string? phoneNumber, string? emailAddress, string postalAddressArea, string postalAddressAreaCode, string postalAddressLine1, string? postalAddressLine2, string? postalAddressLine3)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            PatientId = patientId;
            PatientRowVersionId = patientRowVersionId;
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

        public Guid UserId { get; }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }

        public Guid PatientId { get; }

        public Guid PatientRowVersionId { get; }

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
