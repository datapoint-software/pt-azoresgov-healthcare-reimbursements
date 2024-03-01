using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    [Route("/api/features/process-creation")]
    public sealed class ProcessCreationController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessCreationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("administrative")]
        [HttpGet("entities")]
        public async Task<ProcessCreationEntitySearchResultModel> SearchEntitiesAsync([FromQuery] string? filter, [FromQuery] int? skip, [FromQuery] int? take, CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCreationEntitySearchQuery, ProcessCreationEntitySearchResult>(
                new ProcessCreationEntitySearchQuery(
                    User.GetId(),
                    filter,
                    skip,
                    take),
                ct);

            return new ProcessCreationEntitySearchResultModel(
                result.Entities
                    .Select(e => new ProcessCreationEntitySearchEntityResultModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                result.Matches,
                result.TotalMatchCount);
        }
    }
}