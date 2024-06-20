using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsQueryValidator : AbstractValidator<MainProcessCreationFeatureOptionsQuery>
    {
        public MainProcessCreationFeatureOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}
