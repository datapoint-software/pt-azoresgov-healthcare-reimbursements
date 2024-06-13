using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    [Route("/api/features/main-process-creation")]
    public sealed class MainProcessCreationFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public MainProcessCreationFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("get-options")]
        public async Task<MainProcessCreationFeatureOptionsResultModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCreationFeatureOptionsQuery, MainProcessCreationFeatureOptionsResult>(
                new MainProcessCreationFeatureOptionsQuery(
                    User.GetId()),
                ct);

            return new MainProcessCreationFeatureOptionsResultModel(
                result.EntitySelectionEnabled,
                result.Entities?
                    .Select(e => new MainProcessCreationFeatureOptionsResultEntityModel(
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
