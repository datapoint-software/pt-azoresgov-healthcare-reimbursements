using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeDeleteModel
    {
        public ProcessCaptureLegalRepresentativeDeleteModel(Guid processId, Guid processRowVersionId, Guid processPatientLegalRepresentativeRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPatientLegalRepresentativeRowVersionId = processPatientLegalRepresentativeRowVersionId;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPatientLegalRepresentativeRowVersionId { get; }
    }
}