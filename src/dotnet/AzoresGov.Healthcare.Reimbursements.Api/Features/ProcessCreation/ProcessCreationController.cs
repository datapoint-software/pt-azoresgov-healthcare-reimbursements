using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [HttpGet]
        public async Task<ProcessCreationOptionsResultModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCreationOptionsQuery, ProcessCreationOptionsResult>(
                new ProcessCreationOptionsQuery(
                    User.GetId()),
                ct);

            return new ProcessCreationOptionsResultModel(
                result.Entities?
                    .Select(e => new ProcessCreationEntityResultModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                result.EntityId);
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
                    .Select(e => new ProcessCreationEntityResultModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                result.EntityIds,
                result.TotalMatchCount);
        }

        [Authorize("administrative")]
        [HttpGet("entities/{entityId:guid}/patients")]
        public async Task<ProcessCreationPatientSearchResultModel> SearchPatientsAsync(
            [FromRoute] Guid entityId,
            [FromQuery] string? filter,
            [FromQuery] int? skip,
            [FromQuery] int? take,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCreationPatientSearchQuery, ProcessCreationPatientSearchResult>(
                new ProcessCreationPatientSearchQuery(
                    User.GetId(),
                    entityId,
                    filter,
                    skip,
                    take),
                ct);

            return new ProcessCreationPatientSearchResultModel(
                result.PatientIds,
                result.Patients
                    .Select(e => new ProcessCreationPatientResultModel(
                        e.Id,
                        e.RowVersionId,
                        e.Name,
                        e.Birth,
                        e.Gender,
                        e.HealthNumber,
                        e.TaxNumber,
                        e.FaxNumber,
                        e.MobileNumber,
                        e.PhoneNumber,
                        e.Death))
                    .ToArray(),
                result.TotalMatchCount);
        }
    }
}