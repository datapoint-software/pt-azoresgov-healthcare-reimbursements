using AzoresGov.Healthcare.Reimbursements.Middleware.Features;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessPatientCapture
{
    [Route("/api/features/process-patient-capture")]
    public sealed class ProcessPatientCaptureController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessPatientCaptureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("administrative")]
        [HttpGet("{processId:guid}")]
        public async Task<ProcessPatientCaptureOptionsResultModel> GetOptionsAsync(
            [FromRoute] Guid processId,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessPatientCaptureOptionsQuery, ProcessPatientCaptureOptionsResult>(
                new ProcessPatientCaptureOptionsQuery(
                    User.GetId(),
                    processId),
                ct);

            return new ProcessPatientCaptureOptionsResultModel(
                new ProcessPatientCaptureOptionsEntityResultModel(
                    result.Entity.Id,
                    result.Entity.RowVersionId,
                    result.Entity.Code,
                    result.Entity.Name,
                    result.Entity.Nature),
                result.ParentEntity is null
                    ? null
                    : new ProcessPatientCaptureOptionsEntityResultModel(
                        result.ParentEntity.Id,
                        result.ParentEntity.RowVersionId,
                        result.ParentEntity.Code,
                        result.ParentEntity.Name,
                        result.ParentEntity.Nature),
                new ProcessPatientCaptureOptionsPatientResultModel(
                    result.Patient.Id,
                    result.Patient.RowVersionId,
                    result.Patient.Name,
                    result.Patient.Birth,
                    result.Patient.Gender,
                    result.Patient.HealthNumber,
                    result.Patient.TaxNumber,
                    result.Patient.AddressLine1,
                    result.Patient.AddressLine2,
                    result.Patient.AddressLine3,
                    result.Patient.PostalCode,
                    result.Patient.PostalCodeArea,
                    result.Patient.EmailAddress,
                    result.Patient.FaxNumber,
                    result.Patient.MobileNumber,
                    result.Patient.PhoneNumber,
                    result.Patient.Death),
                new ProcessPatientCaptureOptionsProcessResultModel(
                    result.Process.Id,
                    result.Process.RowVersionId,
                    result.Process.Number,
                    result.Process.Status));
        }
    }
}