using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AzoresGov.Healthcare.Reimbursements.Configuration.Helpers
{
    internal static class ParameterHelper
    {
        internal static bool TryGetValue<TValue>(this IReadOnlyCollection<ParameterEntity> parameters, string parameterName, out TValue value)
        {
            var parameter = parameters.FirstOrDefault(e => e.Name == parameterName);

            if (parameter == null)
            {
                value = default!;

                return false;
            }

            return TryGetValue(parameter, out value);
        }

        internal static bool TryGetValue<TValue>(this ParameterEntity? parameter, out TValue value)
        {
            if (!string.IsNullOrEmpty(parameter?.JsonValue) && parameter.JsonValue != "null")
            {
                try
                {
                    value = JsonSerializer.Deserialize<TValue>(parameter.JsonValue)!;
                    return true;
                }
                catch (Exception) { }
            }

            value = default!;

            return false;
        }
    }
}
