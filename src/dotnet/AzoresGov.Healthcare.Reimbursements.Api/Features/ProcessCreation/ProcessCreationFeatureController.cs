using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    [Route("/api/features/process-creation")]
    public sealed class ProcessCreationFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessCreationFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ProcessCreationOptionsResultModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCreationOptionsQuery, ProcessCreationOptionsResult>(
                new ProcessCreationOptionsQuery(
                    User.GetId()),
                ct);

            return new ProcessCreationOptionsResultModel(
                new ProcessCreationEntitySelectionOptionsResultModel(
                    result.EntitySelection.Enabled));
        }
    }
}
