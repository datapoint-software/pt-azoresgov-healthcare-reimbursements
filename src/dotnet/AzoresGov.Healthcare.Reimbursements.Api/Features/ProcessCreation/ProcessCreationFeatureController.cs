using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        [Administrative]
        [HttpGet]
        public async Task<ProcessCreationFeatureOptionsResultModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCreationFeatureOptionsQuery, ProcessCreationFeatureOptionsResult>(
                new ProcessCreationFeatureOptionsQuery(
                    User.GetId()),
                ct);

            return new ProcessCreationFeatureOptionsResultModel(
                result.EntitySelectionEnabled,
                result.Entities?
                    .Select(e => new ProcessCreationFeatureOptionsResultEntityModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                result.EntityId);
        }
    }
}
