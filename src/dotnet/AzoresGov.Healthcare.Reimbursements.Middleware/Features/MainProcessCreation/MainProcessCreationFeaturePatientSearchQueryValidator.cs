using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeaturePatientSearchQueryValidator : AbstractValidator<MainProcessCreationFeaturePatientSearchQuery>
    {
        public MainProcessCreationFeaturePatientSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.EntityId)
                .NotEmpty();

            RuleFor(q => q.EntityRowVersionId)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .MinimumLength(3)
                .MaximumLength(128)
                .When(q => q.UseFullSearchCriteria);

            RuleFor(q => q.Filter)
                .PatientNumber()
                .When(q => q.UseFullSearchCriteria is false);

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(25);
        }
    }
}
