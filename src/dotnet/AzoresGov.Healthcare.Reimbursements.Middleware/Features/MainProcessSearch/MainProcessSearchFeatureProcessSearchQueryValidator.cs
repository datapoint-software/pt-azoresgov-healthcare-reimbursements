using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessSearchQueryValidator : AbstractValidator<MainProcessSearchFeatureProcessSearchQuery>
    {
        public MainProcessSearchFeatureProcessSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .MinimumLength(3)
                .MaximumLength(128)
                .When(q => !string.IsNullOrEmpty(q.Filter));

            RuleFor(q => q.UseFullSearchCriteria);

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(25);
        }
    }
}
