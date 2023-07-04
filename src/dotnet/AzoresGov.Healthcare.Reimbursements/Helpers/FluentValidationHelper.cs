using FluentValidation.Resources;
using FluentValidation.Validators;
using System.Collections.Generic;
using System.Globalization;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class FluentValidationHelper
    {
        internal static string ResolveValidatorErrorCode(IPropertyValidator validator) =>

            // E.g.: NotEmptyValidator => notempty
            // See https://docs.fluentvalidation.net/en/latest/error-codes.html#errorcode-and-error-messages
            validator.Name.Replace("Validator", "").ToLower();

        internal sealed class LanguageManager : ILanguageManager
        {
            private static readonly Dictionary<string, string> Messages = new()
            {
                { "NotEmptyValidator", "Este campo é de preenchimento obrigatório." },
                { "NotNullValidator", "Este campo é de preenchimento obrigatório." },
                { "EmailValidator", "Este campo requere um endereço de correio eletrónico válido." },
                { "MaxLengthValidator", "Este campo é demasiado longo." }
            };

            public bool Enabled { get; set; }

            public CultureInfo Culture { get; set; } = new CultureInfo("pt-PT");

            public string GetString(string key, CultureInfo? culture)
            {
                if (!Messages.TryGetValue(key, out var message))
                    message = "Este campo não é válido.";

                return string.Format(culture, message);
            }
        }
    }
}
