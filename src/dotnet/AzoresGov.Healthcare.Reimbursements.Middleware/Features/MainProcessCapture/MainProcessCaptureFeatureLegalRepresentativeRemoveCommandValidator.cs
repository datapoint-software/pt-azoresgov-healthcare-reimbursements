using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeRemoveCommandValidator : AbstractValidator<MainProcessCaptureFeatureLegalRepresentativeRemoveCommand>
    {
        public MainProcessCaptureFeatureLegalRepresentativeRemoveCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.PatientRowVersionId)
                .NotEmpty();

            RuleFor(c => c.LegalRepresentativeRowVersionId)
                .NotEmpty();
        }
    }
}
