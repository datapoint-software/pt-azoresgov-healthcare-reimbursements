using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsQueryValidator : AbstractValidator<MainProcessCreationFeatureOptionsQuery>
    {
        public ProcessCreationFeatureOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}
