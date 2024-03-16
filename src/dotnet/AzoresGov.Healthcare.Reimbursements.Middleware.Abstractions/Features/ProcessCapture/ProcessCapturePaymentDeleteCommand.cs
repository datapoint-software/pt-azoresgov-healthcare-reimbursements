using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentDeleteCommand : Command<ProcessCapturePaymentDeleteResult>
    {
        public ProcessCapturePaymentDeleteCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid processPaymentConfigurationRowVersionId, Guid? processPaymentWireTransferConfigurationRowVersionId)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPaymentConfigurationRowVersionId = processPaymentConfigurationRowVersionId;
            ProcessPaymentWireTransferConfigurationRowVersionId = processPaymentWireTransferConfigurationRowVersionId;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid ProcessPaymentConfigurationRowVersionId { get; }
        
        public Guid? ProcessPaymentWireTransferConfigurationRowVersionId { get; }
    }
}