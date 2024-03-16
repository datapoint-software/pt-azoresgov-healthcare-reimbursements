using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationCommandValidator : AbstractValidator<ProcessCaptureConfigurationCommand>
    {
        public ProcessCaptureConfigurationCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessConfigurationRowVersionId);

            RuleFor(c => c.MachadoJosephEnabled);

            RuleFor(c => c.DocumentIssueDateBypassEnabled);

            RuleFor(c => c.ReimbursementLimitBypassEnabled);

            RuleFor(c => c.UnemploymentEnabled);
        }
    }
}