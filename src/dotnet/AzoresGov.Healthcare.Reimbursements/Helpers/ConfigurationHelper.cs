using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class ConfigurationHelper
    {
        internal static string GetBaseAddress(this IConfiguration configuration)
        {
            var baseAddress = configuration.GetValue<string>("BaseAddress");

            if (string.IsNullOrEmpty(baseAddress))
                throw new InvalidOperationException("Application base address, on property 'BaseAddress', is not set.");

            return baseAddress;
        }
    }
}
