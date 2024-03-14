using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationResult
    {
        public ProcessCaptureConfigurationResult(Guid processRowVersionId, Guid processConfigurationRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessConfigurationRowVersionId = processConfigurationRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessConfigurationRowVersionId { get; }
    }
}