using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureDeleteLegalRepresentativeCommandValidator : AbstractValidator<ProcessCaptureDeleteLegalRepresentativeCommand>
    {
        public ProcessCaptureDeleteLegalRepresentativeCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPatientLegalRepresentativeRowVersionId)
                .NotEmpty();
        }
    }
}