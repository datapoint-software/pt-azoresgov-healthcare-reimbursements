using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureLegalRepresentativeCommandValidator : AbstractValidator<ProcessCaptureLegalRepresentativeCommand>
    {
        public ProcessCaptureLegalRepresentativeCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPatientLegalRepresentativeId);

            RuleFor(c => c.Name)
                .MaximumLength(128)
                .NotEmpty();
            
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