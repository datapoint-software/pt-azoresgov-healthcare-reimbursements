using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Environment
{
    [Route("/api/features/environment")]
    public sealed class EnvironmentController : Controller
    {
        private readonly IMediator _mediator;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public EnvironmentController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<EnvironmentResultModel> GetEnvironmentAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<EnvironmentQuery, EnvironmentResult>(
                new EnvironmentQuery(),
                ct);

            return new EnvironmentResultModel(
                _webHostEnvironment.IsProduction() ? EnvironmentNature.Production :
                _webHostEnvironment.IsStaging() ? EnvironmentNature.Staging :
                    EnvironmentNature.Development,
                result.ProductVersion);
        }
    }
}
