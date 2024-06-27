using FluentValidation;
using System.Text.RegularExpressions;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class FluentValidationHelper
    {
        private static readonly Regex TaxNumberExpression = new(@"^(([A-Z]{2})\d+)|(\d{9})$", RegexOptions.Compiled);

        public static IRuleBuilderOptions<T, string> PatientNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>

            ruleBuilder.Must((filter) =>
            {
                if (string.IsNullOrEmpty(filter))
                    return true;

                if (filter.Length == 9 && long.TryParse(filter, out var _))
                    return true;

                return false;
            });
        public static IRuleBuilderOptions<T, string> TaxNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>

            ruleBuilder.Must((filter) =>
            {
                if (string.IsNullOrEmpty(filter))
                    return true;

                if (filter.Length > 2 && TaxNumberExpression.IsMatch(filter))
                    return true;

                return false;
            });
    }
}
