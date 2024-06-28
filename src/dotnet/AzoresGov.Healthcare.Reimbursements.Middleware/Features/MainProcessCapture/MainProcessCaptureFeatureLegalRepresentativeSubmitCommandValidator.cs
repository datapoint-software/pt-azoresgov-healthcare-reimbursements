using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSubmitCommandValidator : AbstractValidator<MainProcessCaptureFeatureLegalRepresentativeSubmitCommand>
    {
        public MainProcessCaptureFeatureLegalRepresentativeSubmitCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.PatientRowVersionId)
                .NotEmpty();

            RuleFor(c => c.LegalRepresentativeId);

            RuleFor(c => c.LegalRepresentativeRowVersionId)
                .NotEmpty()
                .When(c => c.LegalRepresentativeId.HasValue);

            RuleFor(c => c.TaxNumber)
                .NotEmpty()
                .TaxNumber()
                .When(c => c.LegalRepresentativeId.HasValue is false);

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(256)
                .When(c => c.LegalRepresentativeId.HasValue is false);

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
