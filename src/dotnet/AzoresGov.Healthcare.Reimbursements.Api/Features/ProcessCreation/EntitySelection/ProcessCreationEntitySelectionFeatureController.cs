using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation.EntitySelection;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation.EntitySelection
{
    [Route("/api/features/process-creation/entity-selection")]
    public sealed class ProcessCreationEntitySelectionFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessCreationEntitySelectionFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("search")]
        public async Task<ProcessCreationEntitySelectionSearchResultModel> SearchAsync(
            [FromBody] ProcessCreationEntitySelectionSearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCreationEntitySelectionSearchQuery, ProcessCreationEntitySelectionSearchResult>(
                new ProcessCreationEntitySelectionSearchQuery(
                    User.GetId(),
                    model.Filter,
                    model.Skip,
                    model.Take),
                ct);

            return new ProcessCreationEntitySelectionSearchResultModel(
                result.TotalMatchCount,
                result.EntityIds,
                result.Entities
                    .Select(e => new ProcessCreationEntitySelectionSearchResultEntityModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray());
        }
    }
}
