using FluentValidation;
using FluentValidation.Results;
using System.Text;
using System.Text.RegularExpressions;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class FluentValidationHelper
    {
        private static readonly Regex IbanRegex = new Regex(@"^[A-Z]{2}\d{2}[A-Z0-9]+$", RegexOptions.Compiled);
        
        internal static IRuleBuilderOptions<T, string> Iban<T>(this IRuleBuilder<T, string> ruleBuilder) =>

            (IRuleBuilderOptions<T, string>) ruleBuilder.Custom((iban, context) =>
            {
                if (string.IsNullOrEmpty(iban))
                    return;

                if (iban.Length > 4 && IbanRegex.IsMatch(iban))
                {
                    iban = iban.ToUpper();

                    var inverse = iban[4..] + iban[0..4];

                    var strb = new StringBuilder();

                    foreach (var chr in inverse)
                    {
                        var cv = char.IsLetter(chr)
                            ? ((int) chr) - 55
                            : int.Parse(chr.ToString());

                        strb.Append(cv);
                    }

                    var str = strb.ToString();
                    var cm = int.Parse(str[..1]);

                    for (var i = 1; i < str.Length; ++i)
                    {
                        cm *= 10;
                        cm += int.Parse(str.Substring(i, 1));
                        cm %= 97;
                    }

                    if (cm == 1)
                        return;
                }
                
                context.AddFailure(new ValidationFailure(
                    context.PropertyPath,
                    "Este campo requere um número de identificação bancário válido.",
                    iban));
            });
    }
}