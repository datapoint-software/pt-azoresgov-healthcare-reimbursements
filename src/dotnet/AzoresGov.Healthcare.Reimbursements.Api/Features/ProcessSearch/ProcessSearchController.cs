using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessSearch
{
    [Route("/api/features/process-search")]
    public sealed class ProcessSearchController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessSearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("administrative")]
        [HttpGet]
        public async Task<ProcessSearchOptionsResultModel> GetOptionsAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessSearchOptionsQuery, ProcessSearchOptionsResult>(
                new ProcessSearchOptionsQuery(
                    User.GetId()),
                ct);

            return new ProcessSearchOptionsResultModel(
                result.Entities
                    .Select(e => new ProcessSearchOptionsEntityResultModel(
                        e.Id,
                        e.Name))
                    .ToArray());
        }

        [Authorize("administrative")]
        [HttpGet("search")]
        public async Task<ProcessSearchResultModel> SearchAsync(
            [FromQuery] Guid? entityId,
            [FromQuery] string? filter,
            [FromQuery] IReadOnlyCollection<ProcessStatus>? status,
            [FromQuery] int? skip,
            [FromQuery] int? take,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessSearchQuery, ProcessSearchResult>(
                new ProcessSearchQuery(
                    User.GetId(),
                    entityId,
                    filter,
                    status?.Count == 0 ? null : 
                        status,
                    skip,
                    take),
                ct);

            return new ProcessSearchResultModel(
                result.Entities
                    .Select(e => new ProcessSearchEntityResultModel(
                        e.Id,
                        e.Code,
                        e.Name))
                    .ToArray(),
                result.Patients
                    .Select(p => new ProcessSearchPatientResultModel(
                        p.Id,
                        p.Name,
                        p.Gender,
                        p.HealthNumber,
                        p.TaxNumber,
                        p.Birth,
                        p.Death))
                    .ToArray(),
                result.Processes
                    .Select(p => new ProcessSearchProcessResultModel(
                        p.Id,
                        p.EntityId,
                        p.PatientId,
                        p.Number,
                        p.Status,
                        p.MachadoJosephEnabled,
                        p.DocumentIssueDateBypassEnabled,
                        p.ReimbursementLimitBypassEnabled,
                        p.Creation,
                        p.Expiration,
                        p.Touch))
                    .ToArray(),
                result.TotalMatchCount);
        }
    }
}