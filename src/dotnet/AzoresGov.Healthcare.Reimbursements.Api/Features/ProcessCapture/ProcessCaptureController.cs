using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    [Route("/api/features/process-capture/{processId:guid}")]
    public sealed class ProcessCaptureController : Controller
    {
        private readonly IMediator _mediator;

        public ProcessCaptureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize("administrative")]
        [HttpPost("delete-legal-representative")]
        public async Task<ProcessCaptureDeleteLegalRepresentativeResultModel> DeleteLegalRepresentativeAsync(
            [FromRoute] Guid processId,
            [FromBody] ProcessCaptureDeleteLegalRepresentativeModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureDeleteLegalRepresentativeCommand, ProcessCaptureDeleteLegalRepresentativeResult>(
                new ProcessCaptureDeleteLegalRepresentativeCommand(
                    User.GetId(),
                    processId,
                    model.ProcessRowVersionId,
                    model.ProcessPatientLegalRepresentativeRowVersionId),
                ct);

            return new ProcessCaptureDeleteLegalRepresentativeResultModel(
                result.ProcessRowVersionId);
        }

        [Authorize("administrative")]
        [HttpGet("")]
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
                result.Configuration is null ? null : 
                    new ProcessCaptureOptionsConfigurationResultModel(
                        result.Configuration.RowVersionId,
                        result.Configuration.MachadoJosephEnabled,
                        result.Configuration.DocumentIssueDateBypassEnabled,
                        result.Configuration.ReimbursementLimitBypassEnabled),
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
                result.PatientLegalRepresentative is null ? null :
                    new ProcessCaptureOptionsPatientLegalRepresentativeResultModel(
                        result.PatientLegalRepresentative.RowVersionId,
                        result.PatientLegalRepresentative.Name,
                        result.PatientLegalRepresentative.TaxNumber,
                        result.PatientLegalRepresentative.EmailAddress,
                        result.PatientLegalRepresentative.FaxNumber,
                        result.PatientLegalRepresentative.MobileNumber,
                        result.PatientLegalRepresentative.PhoneNumber),
                new ProcessCaptureOptionsProcessResultModel(
                    result.Process.Id,
                    result.Process.RowVersionId,
                    result.Process.Number,
                    result.Process.Status));
        }

        [Authorize("administrative")]
        [HttpPost("configuration")]
        public async Task<ProcessCaptureConfigurationResultModel> WriteConfigurationAsync(
            [FromRoute] Guid processId,
            [FromBody] ProcessCaptureConfigurationModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureConfigurationCommand, ProcessCaptureConfigurationResult>(
                new ProcessCaptureConfigurationCommand(
                    User.GetId(),
                    processId,
                    model.ProcessRowVersionId,
                    model.ProcessConfigurationRowVersionId,
                    model.MachadoJosephEnabled,
                    model.DocumentIssueDateBypassEnabled,
                    model.ReimbursementLimitBypassEnabled),
                ct);
            
            return new ProcessCaptureConfigurationResultModel(
                result.ProcessRowVersionId,
                result.ProcessConfigurationRowVersionId);
        }

        [Authorize("administrative")]
        [HttpPost("patient")]
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
                    model.ProcessPatientRowVersionId,
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
                result.ProcessRowVersionId,
                result.ProcessPatientRowVersionId);
        }

        [Authorize("administrative")]
        [HttpPost("write-legal-representative")]
        public async Task<ProcessCaptureWriteLegalRepresentativeResultModel> WriteLegalRepresentativeAsync(
            [FromRoute] Guid processId,
            [FromBody] ProcessCaptureWriteLegalRepresentativeModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureWriteLegalRepresentativeCommand, ProcessCaptureWriteLegalRepresentativeResult>(
                new ProcessCaptureWriteLegalRepresentativeCommand(
                    User.GetId(),
                    processId,
                    model.ProcessRowVersionId,
                    model.ProcessPatientLegalRepresentativeId,
                    model.Name,
                    model.TaxNumber,
                    model.EmailAddress,
                    model.FaxNumber,
                    model.MobileNumber,
                    model.PhoneNumber),
                ct);

            return new ProcessCaptureWriteLegalRepresentativeResultModel(
                result.ProcessRowVersionId,
                result.ProcessPatientLegalRepresentativeRowVersionId);
        }
    }
}