using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchQueryValidator : AbstractValidator<ProcessSearchQuery>
    {
        public ProcessSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.EntityId);

            RuleFor(q => q.Filter)
                .MaximumLength(128);

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(25);
        }
    }
}