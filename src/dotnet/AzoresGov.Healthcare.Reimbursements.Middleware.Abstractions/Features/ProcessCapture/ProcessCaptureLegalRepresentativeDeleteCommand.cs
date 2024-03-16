using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeDeleteCommand : Command<ProcessCaptureLegalRepresentativeDeleteResult>
    {
        public ProcessCaptureLegalRepresentativeDeleteCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid processPatientLegalRepresentativeRowVersionId)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientLegalRepresentativeRowVersionId = processPatientLegalRepresentativeRowVersionId;
        }

        public Guid UserId { get; }

        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientLegalRepresentativeRowVersionId { get; }
    }
}