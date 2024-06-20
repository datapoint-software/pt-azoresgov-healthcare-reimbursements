using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureConfirmationCommandValidator : AbstractValidator<MainProcessCreationFeatureConfirmationCommand>
    {
        public MainProcessCreationFeatureConfirmationCommandValidator()
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
