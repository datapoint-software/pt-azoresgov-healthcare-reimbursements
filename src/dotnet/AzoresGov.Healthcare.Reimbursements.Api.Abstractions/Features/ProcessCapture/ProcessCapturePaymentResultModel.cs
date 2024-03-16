using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentResultModel
    {
        public ProcessCapturePaymentResultModel(Guid processRowVersionId, Guid processPaymentConfigurationRowVersionId, Guid? processPaymentWireTransferConfigurationRowVersionId)
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