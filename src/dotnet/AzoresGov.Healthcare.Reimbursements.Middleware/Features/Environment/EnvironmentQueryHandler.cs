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
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            if (string.IsNullOrEmpty(fileVersionInfo.FileVersion))
                throw new InvalidOperationException("Can not get the executing assembly file version.");

            if (string.IsNullOrEmpty(fileVersionInfo.ProductVersion))
                throw new InvalidOperationException("Can not get the executing assembly product version.");

            return Task.FromResult(
                new EnvironmentResult(
                    fileVersionInfo.IsDebug,
                    fileVersionInfo.FileVersion,
                    fileVersionInfo.ProductVersion));
        }
    }
}
