using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityQueryValidator : AbstractValidator<IdentityQuery>
    {
        public IdentityQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();
        }
    }
}
