using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSelectCommandValidator : AbstractValidator<MainProcessCaptureFeatureLegalRepresentativeSelectCommand>
    {
        public MainProcessCaptureFeatureLegalRepresentativeSelectCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.PatientRowVersionId)
                .NotEmpty();

            RuleFor(c => c.TaxNumber)
                .NotEmpty()
                .TaxNumber();
        }
    }
}
