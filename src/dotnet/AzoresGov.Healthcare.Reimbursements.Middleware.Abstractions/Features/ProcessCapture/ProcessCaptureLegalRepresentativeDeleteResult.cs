using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeDeleteResult
    {
        public ProcessCaptureLegalRepresentativeDeleteResult(Guid processRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
    }
}