using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePatientCommandValidator : AbstractValidator<ProcessCapturePatientCommand>
    {
        public ProcessCapturePatientCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.PatientRowVersionId)
                .NotEmpty();

            RuleFor(c => c.AddressLine1)
                .NotEmpty()
                .MaximumLength(128);

            RuleFor(c => c.AddressLine2)
                .MaximumLength(128);

            RuleFor(c => c.AddressLine3)
                .MaximumLength(128);

            RuleFor(c => c.PostalCode)
                .NotEmpty()
                .MaximumLength(16);

            RuleFor(c => c.PostalCodeArea)
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(c => c.EmailAddress)
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(c => c.FaxNumber)
                .MaximumLength(16);

            RuleFor(c => c.MobileNumber)
                .MaximumLength(16);

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(16);
        }
    }
}