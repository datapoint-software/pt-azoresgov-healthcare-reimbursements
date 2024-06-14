using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchQueryValidator : AbstractValidator<MainProcessCreationPatientSelectionFeatureSearchQuery>
    {
        public MainProcessCreationPatientSelectionFeatureSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.EntityId)
                .NotEmpty();

            RuleFor(q => q.EntityRowVersionId)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .MinimumLength(3)
                .MaximumLength(256)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .PatientNumber()
                .When(q => q.Mode is MainProcessCreationPatientSelectionFeatureSearchMode.PatientNumber);

            RuleFor(q => q.Mode)
                .IsInEnum()
                .NotEmpty();

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(25);
        }
    }
}
