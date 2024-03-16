using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentResult
    {
        public ProcessCapturePaymentResult(Guid processRowVersionId, Guid processPaymentConfigurationRowVersionId, Guid? processPaymentWireTransferConfigurationRowVersionId)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessPaymentConfigurationRowVersionId = processPaymentConfigurationRowVersionId;
            ProcessPaymentWireTransferConfigurationRowVersionId = processPaymentWireTransferConfigurationRowVersionId;
        }

        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPaymentConfigurationRowVersionId { get; }
        
        public Guid? ProcessPaymentWireTransferConfigurationRowVersionId { get; }
    }
}