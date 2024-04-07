using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsQueryValidator : AbstractValidator<ProcessCreationFeatureOptionsQuery>
    {
        public ProcessCreationFeatureOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}
