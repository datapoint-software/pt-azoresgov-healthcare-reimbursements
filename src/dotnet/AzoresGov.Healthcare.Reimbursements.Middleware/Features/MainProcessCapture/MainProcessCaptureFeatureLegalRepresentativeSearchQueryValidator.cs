using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSearchQueryValidator : AbstractValidator<MainProcessCaptureFeatureLegalRepresentativeSearchQuery>
    {
        public MainProcessCaptureFeatureLegalRepresentativeSearchQueryValidator()
        {
            RuleFor(q => q.UserId)
                .NotEmpty();

            RuleFor(q => q.TaxNumber)
                .NotEmpty()
                .TaxNumber();
        }
    }
}
