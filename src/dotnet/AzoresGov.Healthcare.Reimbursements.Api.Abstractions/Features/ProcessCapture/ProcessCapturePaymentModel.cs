using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentModel
    {
        public ProcessCapturePaymentModel(Guid processId, Guid processRowVersionId, Guid? processPaymentConfigurationRowVersionId, Guid? processPaymentWireTransferConfigurationRowVersionId, PaymentMethod method, PaymentReceiver receiver, string? iban, string? swift)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessPaymentConfigurationRowVersionId = processPaymentConfigurationRowVersionId;
            ProcessPaymentWireTransferConfigurationRowVersionId = processPaymentWireTransferConfigurationRowVersionId;
            Method = method;
            Receiver = receiver;
            Iban = iban;
            Swift = swift;
        }

        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid? ProcessPaymentConfigurationRowVersionId { get; }
        
        public Guid? ProcessPaymentWireTransferConfigurationRowVersionId { get; }
        
        public PaymentMethod Method { get; }
        
        public PaymentReceiver Receiver { get; }

        public string? Iban { get; }
        
        public string? Swift { get; }
    }
}