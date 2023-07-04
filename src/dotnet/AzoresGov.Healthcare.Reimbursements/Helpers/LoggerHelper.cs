using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class LoggerHelper
    {
        internal static ILogger CreateLogger(IConfiguration configuration, IWebHostEnvironment environment)
        {
            return LoggerFactory.Create(logger => logger.WithEnvironmentDefaults(configuration, environment))
                .CreateLogger(nameof(Program));
        }

        internal static ILoggingBuilder WithEnvironmentDefaults(this ILoggingBuilder logger, IConfiguration configuration, IWebHostEnvironment environment) => logger

            .AddConsole()
            .SetMinimumLevel(
                environment.IsProduction()
                    ? LogLevel.Warning
                    : LogLevel.Trace);
    }
}
