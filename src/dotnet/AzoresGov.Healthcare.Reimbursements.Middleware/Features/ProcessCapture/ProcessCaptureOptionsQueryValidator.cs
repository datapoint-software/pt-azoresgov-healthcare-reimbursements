using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsQueryValidator : AbstractValidator<ProcessCaptureOptionsQuery>
    {
        public ProcessCaptureOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.ProcessId)
                .NotEmpty();
        }
    }
}