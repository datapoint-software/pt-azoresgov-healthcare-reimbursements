using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Environment
{
    [Route("/api/environment")]
    public sealed class EnvironmentFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public EnvironmentFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<EnvironmentResultModel> GetEnvironmentAsync(CancellationToken ct)
        {
            var environment = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

            var result = await _mediator.HandleQueryAsync<EnvironmentQuery, EnvironmentResult>(
                new EnvironmentQuery(),
                ct);

            return new EnvironmentResultModel(
                environment.IsProduction(),
                result.DebugSymbols,
                result.FileVersion,
                result.ProductVersion);
        }
    }
}
