using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationCommandValidator : AbstractValidator<ProcessCreationCommand>
    {
        public ProcessCreationCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.EntityId)
                .NotEmpty();

            RuleFor(c => c.EntityRowVersionId)
                .NotEmpty();

            RuleFor(c => c.PatientId)
                .NotEmpty();

            RuleFor(c => c.PatientRowVersionId)
                .NotEmpty();
        }
    }
}