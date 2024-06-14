using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationConfirmation
{
    public sealed class MainProcessCreationConfirmationFeatureConfirmCommandValidator : AbstractValidator<MainProcessCreationConfirmationFeatureConfirmCommand>
    {
        public MainProcessCreationConfirmationFeatureConfirmCommandValidator()
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
