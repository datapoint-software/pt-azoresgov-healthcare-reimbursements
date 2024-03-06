using AzoresGov.Healthcare.Reimbursements.Middleware.Features;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    [Route("/api/features/process-capture")]
    public sealed class ProcessCaptureController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessCaptureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("administrative")]
        [HttpGet("{processId:guid}")]
        public async Task<ProcessCaptureOptionsResultModel> GetOptionsAsync(
            [FromRoute] Guid processId,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<ProcessCaptureOptionsQuery, ProcessCaptureOptionsResult>(
                new ProcessCaptureOptionsQuery(
                    User.GetId(),
                    processId),
                ct);

            return new ProcessCaptureOptionsResultModel(
                new ProcessCaptureOptionsEntityResultModel(
                    result.Entity.Id,
                    result.Entity.RowVersionId,
                    result.Entity.Code,
                    result.Entity.Name,
                    result.Entity.Nature),
                result.ParentEntity is null
                    ? null
                    : new ProcessCaptureOptionsEntityResultModel(
                        result.ParentEntity.Id,
                        result.ParentEntity.RowVersionId,
                        result.ParentEntity.Code,
                        result.ParentEntity.Name,
                        result.ParentEntity.Nature),
                new ProcessCaptureOptionsPatientResultModel(
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
                new ProcessCaptureOptionsProcessResultModel(
                    result.Process.Id,
                    result.Process.RowVersionId,
                    result.Process.Number,
                    result.Process.Status));
        }

        [Authorize("administrative")]
        [HttpPost("{processId:guid}/patient")]
        public async Task<ProcessCapturePatientResultModel> WritePatientAsync(
            [FromRoute] Guid processId,
            [FromBody] ProcessCapturePatientModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCapturePatientCommand, ProcessCapturePatientResult>(
                new ProcessCapturePatientCommand(
                    User.GetId(),
                    processId,
                    model.ProcessRowVersionId,
                    model.PatientRowVersionId,
                    model.AddressLine1,
                    model.AddressLine2,
                    model.AddressLine3,
                    model.PostalCode,
                    model.PostalCodeArea,
                    model.EmailAddress,
                    model.FaxNumber,
                    model.MobileNumber,
                    model.PhoneNumber),
                ct);

            return new ProcessCapturePatientResultModel(
                result.PatientRowVersionId,
                result.ProcessRowVersionId);
        }
    }
}