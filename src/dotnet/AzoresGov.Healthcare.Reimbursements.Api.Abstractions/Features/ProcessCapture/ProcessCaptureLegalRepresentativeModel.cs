using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeModel
    {
        public ProcessCaptureLegalRepresentativeModel(Guid processRowVersionId, Guid? processPatientLegalRepresentativeId, string name, string taxNumber, string? emailAddress, string? faxNumber, string? mobileNumber, string? phoneNumber)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientLegalRepresentativeId = processPatientLegalRepresentativeId;
            Name = name;
            TaxNumber = taxNumber;
            EmailAddress = emailAddress;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid? ProcessPatientLegalRepresentativeId { get; }
        
        public string Name { get; }
        
        public string TaxNumber { get; }
        
        public string? EmailAddress { get; }
        
        public string? FaxNumber { get; }
        
        public string? MobileNumber { get; }
        
        public string? PhoneNumber { get; }
    }
}