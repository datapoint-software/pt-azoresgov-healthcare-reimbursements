using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeDeleteResultModel
    {
        public ProcessCaptureLegalRepresentativeDeleteResultModel(Guid processRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
    }
}