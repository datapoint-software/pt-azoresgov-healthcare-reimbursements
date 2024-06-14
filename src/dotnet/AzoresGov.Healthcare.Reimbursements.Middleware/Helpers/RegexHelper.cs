using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class RegexHelper
    {
        internal static Regex CreateFromLikePatternExpression(string patternExpression)
        {
            var tokens = patternExpression.Split('*')
                .Select(Regex.Escape);            

            return new Regex($"^{string.Join(".*", tokens)}$", RegexOptions.Compiled);
        }
    }
}
