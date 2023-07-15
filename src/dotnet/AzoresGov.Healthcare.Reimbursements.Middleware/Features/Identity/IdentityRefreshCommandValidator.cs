using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity
{
    public sealed class IdentityRefreshCommandValidator : AbstractValidator<IdentityRefreshCommand>
    {
        public IdentityRefreshCommandValidator()
        {
            RuleFor(e => e.UserSessionId)
                .NotEmpty();

            RuleFor(e => e.UserSessionRowVersionId)
                .NotEmpty();
        }
    }
}
