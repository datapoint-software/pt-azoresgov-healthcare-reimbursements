using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public class ProcessCaptureOptionsPaymentResult
    {
        public ProcessCaptureOptionsPaymentResult(Guid? processPaymentConfigurationRowVersionId, Guid? processPaymentWireTransferConfigurationRowVersionId, PaymentMethod method, PaymentReceiver receiver, string? iban, string? swift)
        {
            ProcessPaymentConfigurationRowVersionId = processPaymentConfigurationRowVersionId;
            ProcessPaymentWireTransferConfigurationRowVersionId = processPaymentWireTransferConfigurationRowVersionId;
            Method = method;
            Receiver = receiver;
            Iban = iban;
            Swift = swift;
        }

        public Guid? ProcessPaymentConfigurationRowVersionId { get; }
        
        public Guid? ProcessPaymentWireTransferConfigurationRowVersionId { get; }
        
        public PaymentMethod Method { get; }
        
        public PaymentReceiver Receiver { get; }

        public string? Iban { get; }
        
        public string? Swift { get; }
    }
}