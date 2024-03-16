using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePaymentDeleteCommandValidator : AbstractValidator<ProcessCapturePaymentDeleteCommand>
    {
        public ProcessCapturePaymentDeleteCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPaymentConfigurationRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPaymentWireTransferConfigurationRowVersionId);
        }
    }
}