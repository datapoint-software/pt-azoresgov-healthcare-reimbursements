using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureFamilyIncomeStatementCommandValidator : AbstractValidator<ProcessCaptureFamilyIncomeStatementCommand>
    {
        public ProcessCaptureFamilyIncomeStatementCommandValidator()
        {
            RuleFor(c => c.UserId)
                .NotEmpty();

            RuleFor(c => c.ProcessId)
                .NotEmpty();

            RuleFor(c => c.ProcessRowVersionId)
                .NotEmpty();

            RuleFor(c => c.ProcessPatientFamilyIncomeStatementRowVersionId);

            RuleFor(c => c.Year)
                .InclusiveBetween(2023, 2024)
                .NotEmpty();

            RuleFor(c => c.AccessCode)
                .MaximumLength(16);

            RuleFor(c => c.FamilyMemberCount)
                .GreaterThanOrEqualTo(0);

            RuleFor(c => c.FamilyIncome)
                .InclusiveBetween(0, 99999999999);
        }
    }
}