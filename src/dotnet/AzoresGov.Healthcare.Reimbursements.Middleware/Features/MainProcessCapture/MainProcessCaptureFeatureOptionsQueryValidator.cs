using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureOptionsQueryValidator : AbstractValidator<MainProcessCaptureFeatureOptionsQuery>
    {
        public MainProcessCaptureFeatureOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.ProcessId)
                .NotEmpty();
        }
    }
}
