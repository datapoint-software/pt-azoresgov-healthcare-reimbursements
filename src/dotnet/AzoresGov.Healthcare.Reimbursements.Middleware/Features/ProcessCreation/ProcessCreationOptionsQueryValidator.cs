using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsQueryValidator : AbstractValidator<ProcessCreationOptionsQuery>
    {
        public ProcessCreationOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}
