using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public class ProcessCapturePaymentDeleteModel
    {
        public ProcessCapturePaymentDeleteModel(Guid processId, Guid processRowVersionId, Guid processPaymentConfigurationRowVersionId, Guid? processPaymentWireTransferConfigurationRowVersionId)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPaymentConfigurationRowVersionId = processPaymentConfigurationRowVersionId;
            ProcessPaymentWireTransferConfigurationRowVersionId = processPaymentWireTransferConfigurationRowVersionId;
        }

        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPaymentConfigurationRowVersionId { get; }
        
        public Guid? ProcessPaymentWireTransferConfigurationRowVersionId { get; }
    }
}