using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationEntitySelection;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationEntitySelection
{
    [Route("/api/features/main-process-creation-entity-selection")]
    public sealed class MainProcessCreationEntitySelectionFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public MainProcessCreationEntitySelectionFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("search")]
        public async Task<MainProcessCreationEntitySelectionSearchResultModel> SearchAsync(
            [FromBody] MainProcessCreationEntitySelectionSearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCreationEntitySelectionSearchQuery, MainProcessCreationEntitySelectionSearchResult>(
                new MainProcessCreationEntitySelectionSearchQuery(
                    User.GetId(),
                    model.Filter,
                    model.Skip,
                    model.Take),
                ct);

            return new MainProcessCreationEntitySelectionSearchResultModel(
                result.TotalMatchCount,
                result.EntityIds,
                result.Entities
                    .Select(e => new MainProcessCreationEntitySelectionSearchResultEntityModel(
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
