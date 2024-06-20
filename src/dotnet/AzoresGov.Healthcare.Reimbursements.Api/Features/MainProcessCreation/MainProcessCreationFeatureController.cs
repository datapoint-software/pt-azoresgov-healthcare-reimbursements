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
        public async Task<MainProcessCreationFeatureOptionsResultModel> GetOptionsAsync(
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCreationFeatureOptionsQuery, MainProcessCreationFeatureOptionsResult>(
                new MainProcessCreationFeatureOptionsQuery(
                    User.GetId()),
                ct);

            return new MainProcessCreationFeatureOptionsResultModel(
                result.EntitySelection is null
                    ? null
                    : new MainProcessCreationFeatureOptionsResultEntitySelectionModel(
                        result.EntitySelection.EntityId,
                        result.EntitySelection.Entities
                            .Select(e => new MainProcessCreationFeatureEntityModel(
                                e.Id,
                                e.RowVersionId,
                                e.ParentEntityId,
                                e.Code,
                                e.Name,
                                e.Nature))
                            .ToArray()));
        }

        [Administrative]
        [HttpPost("search-entities")]
        public async Task<MainProcessCreationFeatureEntitySearchResultModel> SearchEntitiesAsync(
            [FromBody] MainProcessCreationFeatureEntitySearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCreationFeatureEntitySearchQuery, MainProcessCreationFeatureEntitySearchResult>(
                new MainProcessCreationFeatureEntitySearchQuery(
                    User.GetId(),
                    model.Filter,
                    model.Skip,
                    model.Take),
                ct);

            return new MainProcessCreationFeatureEntitySearchResultModel(
                result.TotalMatchCount,
                result.EntityIds,
                result.Entities
                    .Select(e => new MainProcessCreationFeatureEntityModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray());
        }

        [Administrative]
        [HttpPost("search-patients")]
        public async Task<MainProcessCreationFeaturePatientSearchResultModel> SearchPatientsAsync(
            [FromBody] MainProcessCreationFeaturePatientSearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCreationFeaturePatientSearchQuery, MainProcessCreationFeaturePatientSearchResult>(
                new MainProcessCreationFeaturePatientSearchQuery(
                    User.GetId(),
                    model.EntityId,
                    model.EntityRowVersionId,
                    model.Filter,
                    model.UseFullSearchCriteria,
                    model.Skip,
                    model.Take),
                ct);

            return new MainProcessCreationFeaturePatientSearchResultModel(
                result.TotalMatchCount,
                result.PatientIds,
                result.Entities
                    .Select(e => new MainProcessCreationFeatureEntityModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                result.Patients
                    .Select(p => new MainProcessCreationFeaturePatientModel(
                        p.Id,
                        p.RowVersionId,
                        p.EntityId,
                        p.Number,
                        p.TaxNumber,
                        p.Name,
                        p.Birth,
                        p.Death,
                        p.FaxNumber,
                        p.MobileNumber,
                        p.PhoneNumber,
                        p.EmailAddress,
                        p.External))
                    .ToArray());
        }

        [Administrative]
        [HttpPost("confirm")]
        public async Task<MainProcessCreationFeatureConfirmationResultModel> ConfirmAsync(
            [FromBody] MainProcessCreationFeatureConfirmationModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<MainProcessCreationFeatureConfirmationCommand, MainProcessCreationFeatureConfirmationResult>(
                new MainProcessCreationFeatureConfirmationCommand(
                    User.GetId(),
                    model.EntityId,
                    model.EntityRowVersionId,
                    model.PatientId,
                    model.PatientRowVersionId),
                ct);

            return new MainProcessCreationFeatureConfirmationResultModel(
                new MainProcessCreationFeatureProcessModel(
                    result.Process.Id,
                    result.Process.RowVersionId,
                    result.Process.Number,
                    result.Process.Status,
                    result.Process.Creation));
        }
    }
}
