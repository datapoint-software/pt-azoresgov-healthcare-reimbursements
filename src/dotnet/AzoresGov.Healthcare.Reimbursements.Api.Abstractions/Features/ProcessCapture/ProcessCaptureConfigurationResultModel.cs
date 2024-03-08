using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationResultModel
    {
        public ProcessCaptureConfigurationResultModel(Guid rowVersionId)
        {
            RowVersionId = rowVersionId;
        }

        public Guid RowVersionId { get; }
    }
}