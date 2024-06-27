using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureSubmitPatientCommandValidator : AbstractValidator<MainProcessCaptureFeatureSubmitPatientCommand>
    {
        public MainProcessCaptureFeatureSubmitPatientCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.PatientId)
                .NotEmpty();

            RuleFor(c => c.PatientRowVersionId)
                .NotEmpty();

            RuleFor(c => c.FaxNumber)
                .MaximumLength(16);

            RuleFor(c => c.MobileNumber)
                .MaximumLength(16);

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(16);

            RuleFor(c => c.EmailAddress)
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(c => c.PostalAddressArea)
                .MaximumLength(64)
                .NotEmpty();

            RuleFor(c => c.PostalAddressAreaCode)
                .MaximumLength(16)
                .NotEmpty();

            RuleFor(c => c.PostalAddressLine1)
                .MaximumLength(256)
                .NotEmpty();

            RuleFor(c => c.PostalAddressLine2)
                .MaximumLength(256);

            RuleFor(c => c.PostalAddressLine3)
                .MaximumLength(256);
        }
    }
}
