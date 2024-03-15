using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureWriteLegalRepresentativeCommand : Command<ProcessCaptureWriteLegalRepresentativeResult>
    {
        public ProcessCaptureWriteLegalRepresentativeCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid? processPatientLegalRepresentativeId, string name, string taxNumber, string? emailAddress, string? faxNumber, string? mobileNumber, string? phoneNumber)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientLegalRepresentativeId = processPatientLegalRepresentativeId;
            Name = name;
            TaxNumber = taxNumber;
            EmailAddress = emailAddress;
            FaxNumber = faxNumber;
            MobileNumber = mobileNumber;
            PhoneNumber = phoneNumber;
        }

        public Guid UserId { get; }

        public Guid ProcessId { get; }
        
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