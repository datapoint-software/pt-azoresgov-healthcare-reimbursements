using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePatientCommand : Command<ProcessCapturePatientResult>
    {
        public ProcessCapturePatientCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid? processPatientRowVersionId, string addressLine1, string? addressLine2, string? addressLine3, string? postalCode, string? postalCodeArea, string? emailAddress, string? faxNumber, string? mobileNumber, string? phoneNumber)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientRowVersionId = processPatientRowVersionId;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            AddressLine3 = addressLine3;
            PostalCode = postalCode;
            PostalCodeArea = postalCodeArea;
            EmailAddress = emailAddress;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid? ProcessPatientRowVersionId { get; }

        public string AddressLine1 { get; }

        public string? AddressLine2 { get; }

        public string? AddressLine3 { get; }

        public string? PostalCode { get; }

        public string? PostalCodeArea { get; }

        public string? EmailAddress { get; }

        public string? FaxNumber { get; }

        public string? MobileNumber { get; }

        public string? PhoneNumber { get; }
    }
}