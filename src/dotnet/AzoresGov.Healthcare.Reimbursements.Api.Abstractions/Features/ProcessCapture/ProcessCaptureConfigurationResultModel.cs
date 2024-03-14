using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationResultModel
    {
        public ProcessCaptureConfigurationResultModel(Guid processRowVersionId, Guid processConfigurationRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessConfigurationRowVersionId = processConfigurationRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessConfigurationRowVersionId { get; }
    }
}