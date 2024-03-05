using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessPatientCapture
{
    public sealed class ProcessPatientCaptureOptionsQueryValidator : AbstractValidator<ProcessPatientCaptureOptionsQuery>
    {
        public ProcessPatientCaptureOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.ProcessId)
                .NotEmpty();
        }
    }
}