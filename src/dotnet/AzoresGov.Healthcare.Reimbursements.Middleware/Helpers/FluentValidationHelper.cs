using FluentValidation;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class FluentValidationHelper
    {
        public static IRuleBuilderOptions<T, string> PatientNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>

            ruleBuilder.Must((filter) =>
            {
                if (string.IsNullOrEmpty(filter))
                    return true;

                if (filter.Length == 9 && long.TryParse(filter, out var _))
                    return true;

                return false;
            });
    }
}
