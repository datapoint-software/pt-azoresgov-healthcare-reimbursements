using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.CoreIdentity
{
    public sealed class CoreIdentityFeatureRefreshCommandValidator : AbstractValidator<CoreIdentityFeatureRefreshCommand>
    {
        public CoreIdentityFeatureRefreshCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.UserSessionId)
                .NotEmpty();
        }
    }
}
