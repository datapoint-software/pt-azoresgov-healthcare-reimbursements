using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using Datapoint;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AzoresGov.Healthcare.Reimbursements.Helpers
{
    internal static class ContextHelper
    {
        private const string ConnectionString = "Server=127.0.0.1,1433; Database=HealthcareReimbursements; User Id=azoresgov-healthcare-reimbursements-app; Password=8cd4a9c3-a6a6-4e6b-abd1-d38063cb7be4; Encrypt=False";

        internal static HealthcareReimbursementsContext CreateContext(IConfiguration configuration, IWebHostEnvironment environment) =>

            new((new DbContextOptionsBuilder<HealthcareReimbursementsContext>())
                .WithEnvironmentDefaults(configuration, environment)
                .Options);

        internal static void Migrate(IWebHostEnvironment environment, ILogger logger, HealthcareReimbursementsContext context)
        {
            if (!environment.IsDevelopment())
                return;

            var database = context.Database.GetDbConnection().Database;

            if (string.IsNullOrEmpty(database))
                throw new InvalidOperationException("Can not determine the database name.");

            var pendingMigrationCount = context.Database
                .GetPendingMigrations()
                .Count();

            if (pendingMigrationCount > 0)
            {
                logger.LogWarning(
                    "A total of {PendingMigrationCount} pending migrations will now be applied.",
                    pendingMigrationCount);

                context.Database.Migrate();
            }
        }

        internal static DbContextOptionsBuilder<TContext> WithEnvironmentDefaults<TContext>(this DbContextOptionsBuilder<TContext> builder, IConfiguration configuration, IWebHostEnvironment environment) where TContext : DbContext
        {
            var connectionString = configuration.GetConnectionString("HealthcareReimbursements");

            if (string.IsNullOrEmpty(connectionString))
            {
                if (!environment.IsDevelopment())
                {
                    throw new InvalidOperationException("Connection string 'HealthcareReimbursements' is required and can not be empty.")
                        .WithCode("CSUJMH");
                }

                connectionString = ConnectionString;
            }

            if (environment.IsDevelopment())
            {
                builder.UseLoggerFactory(
                    LoggerFactory.Create(logger => logger
                        .WithEnvironmentDefaults(configuration, environment)));
            }

            return builder.UseSqlServer(connectionString, (provider) =>
            {
                provider.CommandTimeout(5);
                provider.EnableRetryOnFailure(3);
                provider.MigrationsAssembly(typeof(HealthcareReimbursementsContext).Assembly.FullName);
                provider.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        }
    }
}
