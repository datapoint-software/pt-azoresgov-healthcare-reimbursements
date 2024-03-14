using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsPatientLegalRepresentativeResult
    {
        public ProcessCaptureOptionsPatientLegalRepresentativeResult(Guid? rowVersionId, string name, string taxNumber, string? emailAddress, string? faxNumber, string? mobileNumber, string? phoneNumber)
        {
            RowVersionId = rowVersionId;
            Name = name;
            TaxNumber = taxNumber;
            EmailAddress = emailAddress;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
        }

        public Guid? RowVersionId { get; }

        public string Name { get; }
        
        public string TaxNumber { get; }
        
        public string? EmailAddress { get; }
        
        public string? FaxNumber { get; }
        
        public string? MobileNumber { get; }
        
        public string? PhoneNumber { get; }
    }
}