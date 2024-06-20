using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureEntitySearchQueryValidator : AbstractValidator<MainProcessCreationFeatureEntitySearchQuery>
    {
        public MainProcessCreationFeatureEntitySearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(128);

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(25);
        }
    }
}
