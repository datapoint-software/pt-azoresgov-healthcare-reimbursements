using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureSimulationQueryValidator : AbstractValidator<ProcessCaptureSimulationQuery>
    {
        public ProcessCaptureSimulationQueryValidator()
        {
            RuleFor(e => e.UserId)
                .NotEmpty();

            RuleFor(e => e.ProcessId)
                .NotEmpty();

            RuleFor(e => e.ProcessRowVersionId)
                .NotEmpty();
        }
    }
}
