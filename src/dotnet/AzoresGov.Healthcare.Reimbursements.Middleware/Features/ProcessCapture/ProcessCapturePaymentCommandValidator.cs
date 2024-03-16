using AzoresGov.Healthcare.Reimbursements.Enumerations;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentCommandValidator : AbstractValidator<ProcessCapturePaymentCommand>
    {
        public ProcessCapturePaymentCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPaymentConfigurationRowVersionId);

            RuleFor(c => c.ProcessPaymentWireTransferConfigurationRowVersionId);

            RuleFor(c => c.Method)
                .IsInEnum()
                .NotEmpty();

            RuleFor(c => c.Receiver)
                .IsInEnum()
                .NotEmpty();

            RuleFor(c => c.Iban)
                .MaximumLength(64)
                .NotEmpty()
                .When(c => c.Method == PaymentMethod.WireTransfer);

            RuleFor(c => c.Swift)
                .MaximumLength(16)
                .NotEmpty()
                .When(c => c.Method == PaymentMethod.WireTransfer);
        }
    }
}