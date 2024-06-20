using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessSearch;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessSearch
{
    [Route("/api/features/main-process-search")]
    public sealed class MainProcessSearchFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public MainProcessSearchFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("search-processes")]
        public async Task<MainProcessSearchFeatureProcessSearchResultModel> SearchProcessesAsync(
            [FromBody] MainProcessSearchFeatureProcessSearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessSearchFeatureProcessSearchQuery, MainProcessSearchFeatureProcessSearchResult>(
                new MainProcessSearchFeatureProcessSearchQuery(
                    User.GetId(),
                    model.Filter,
                    model.UseFullSearchCriteria,
                    model.Skip,
                    model.Take),
                ct);

            return new MainProcessSearchFeatureProcessSearchResultModel(
                result.TotalMatchCount,
                result.ProcessIds,
                result.Entities
                    .Select(e => new MainProcessSearchFeatureEntityModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                result.Patients
                    .Select(p => new MainProcessSearchFeaturePatientModel(
                        p.Id,
                        p.RowVersionId,
                        p.EntityId,
                        p.Number,
                        p.TaxNumber,
                        p.Name,
                        p.Death))
                    .ToArray(),
                result.Processes
                    .Select(p => new MainProcessSearchFeatureProcessModel(
                        p.Id,
                        p.RowVersionId,
                        p.EntityId,
                        p.PatientId,
                        p.Number,
                        p.Status,
                        p.Creation))
                    .ToArray());
        }
    }
}
