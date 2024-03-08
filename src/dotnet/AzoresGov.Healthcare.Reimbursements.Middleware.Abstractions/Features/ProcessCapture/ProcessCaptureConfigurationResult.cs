using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCaptureConfigurationResult
    {
        public ProcessCaptureConfigurationResult(Guid rowVersionId)
        {
            RowVersionId = rowVersionId;
        }
        
        public Guid RowVersionId { get; }
    }
}