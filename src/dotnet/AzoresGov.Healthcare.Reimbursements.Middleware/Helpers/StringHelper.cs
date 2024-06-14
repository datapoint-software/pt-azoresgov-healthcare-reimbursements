using System.Globalization;
using System.Linq;
using System.Text;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class StringHelper
    {
        internal static string CreateLikePatternExpression(string expression)
        {
            var expressionFormD = expression
                .ToLower()
                .Normalize(NormalizationForm.FormD);

            var patternExpressionBuilder = new StringBuilder();

            patternExpressionBuilder.Append('*');

            foreach (var c in expressionFormD)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);

                if (uc is not UnicodeCategory.NonSpacingMark)
                    patternExpressionBuilder.Append(c);
            }

            patternExpressionBuilder.Append('*');

            return patternExpressionBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }
    }
}
