using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchOptionsQueryValidator : AbstractValidator<ProcessSearchOptionsQuery>
    {
        public ProcessSearchOptionsQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}