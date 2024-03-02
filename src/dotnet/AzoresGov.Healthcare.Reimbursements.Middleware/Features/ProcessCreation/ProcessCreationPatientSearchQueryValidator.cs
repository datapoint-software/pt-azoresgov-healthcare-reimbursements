using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationPatientSearchQueryValidator : AbstractValidator<ProcessCreationPatientSearchQuery>
    {
        public ProcessCreationPatientSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.EntityId)
                .NotEmpty();

            RuleFor(q => q.Filter)
                .MaximumLength(128)
                .MinimumLength(3);

            RuleFor(q => q.Skip)
                .GreaterThanOrEqualTo(0);

            RuleFor(q => q.Take)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(15);
        }
    }
}