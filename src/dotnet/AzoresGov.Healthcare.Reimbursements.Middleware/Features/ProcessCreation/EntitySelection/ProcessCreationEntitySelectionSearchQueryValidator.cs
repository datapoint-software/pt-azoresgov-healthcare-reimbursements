using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation.EntitySelection
{
    public sealed class ProcessCreationEntitySelectionSearchQueryValidator : AbstractValidator<ProcessCreationEntitySelectionSearchQuery>
    {
        public ProcessCreationEntitySelectionSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .MinimumLength(3)
                .MaximumLength(256)
                .NotEmpty();

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(25);
        }
    }
}
