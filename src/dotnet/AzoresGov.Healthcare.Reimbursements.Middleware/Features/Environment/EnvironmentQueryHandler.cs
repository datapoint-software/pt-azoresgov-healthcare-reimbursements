using Datapoint.Mediator;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment
{
    public sealed class EnvironmentQueryHandler : IQueryHandler<EnvironmentQuery, EnvironmentResult>
    {
        public Task<EnvironmentResult> HandleQueryAsync(EnvironmentQuery query, CancellationToken ct)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);

            if (string.IsNullOrEmpty(fileVersionInfo.ProductVersion))
                throw new InvalidOperationException("Can not the product version number from the executing assembly.");

            return Task.FromResult(
                new EnvironmentResult(
                    fileVersionInfo.ProductVersion));
        }
    }
}
