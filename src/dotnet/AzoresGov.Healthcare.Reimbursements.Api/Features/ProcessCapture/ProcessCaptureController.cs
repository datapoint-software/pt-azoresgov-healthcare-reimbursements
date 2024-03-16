using AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture;
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
        [HttpPost("delete-legal-representative")]
        public async Task<ProcessCaptureLegalRepresentativeDeleteResultModel> DeleteLegalRepresentativeAsync(
            [FromBody] ProcessCaptureLegalRepresentativeDeleteModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureLegalRepresentativeDeleteCommand, ProcessCaptureLegalRepresentativeDeleteResult>(
                new ProcessCaptureLegalRepresentativeDeleteCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.ProcessPatientLegalRepresentativeRowVersionId),
                ct);

            return new ProcessCaptureLegalRepresentativeDeleteResultModel(
                result.ProcessRowVersionId);
        }

        [Authorize("administrative")]
        [HttpPost("delete-family-income-statement")]
        public async Task<ProcessCaptureFamilyIncomeStatementDeleteResultModel> DeleteFamilyIncomeStatementAsync(
            [FromBody] ProcessCaptureFamilyIncomeStatementDeleteModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureFamilyIncomeStatementDeleteCommand, ProcessCaptureFamilyIncomeStatementDeleteResult>(
                new ProcessCaptureFamilyIncomeStatementDeleteCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.ProcessPatientFamilyIncomeStatementRowVersionId),
                ct);

            return new ProcessCaptureFamilyIncomeStatementDeleteResultModel(
                result.ProcessRowVersionId);
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
                result.Configuration is null ? null : 
                    new ProcessCaptureOptionsConfigurationResultModel(
                        result.Configuration.RowVersionId,
                        result.Configuration.MachadoJosephEnabled,
                        result.Configuration.DocumentIssueDateBypassEnabled,
                        result.Configuration.ReimbursementLimitBypassEnabled,
                        result.Configuration.UnemploymentEnabled),
                new ProcessCaptureOptionsEntityResultModel(
                    result.Entity.Id,
                    result.Entity.RowVersionId,
                    result.Entity.Code,
                    result.Entity.Name,
                    result.Entity.Nature),
                result.FamilyIncomeStatement is null
                    ? null
                    : new ProcessCaptureOptionsFamilyIncomeStatementResultModel(
                        result.FamilyIncomeStatement.RowVersionId,
                        result.FamilyIncomeStatement.Year,
                        result.FamilyIncomeStatement.AccessCode,
                        result.FamilyIncomeStatement.FamilyMemberCount,
                        result.FamilyIncomeStatement.FamilyIncome),
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
                result.Payment is null ? null :
                    new ProcessCaptureOptionsPaymentResultModel(
                        result.Payment.ProcessPaymentConfigurationRowVersionId,
                        result.Payment.ProcessPaymentWireTransferConfigurationRowVersionId,
                        result.Payment.Method,
                        result.Payment.Receiver,
                        result.Payment.Iban,
                        result.Payment.Swift),
                new ProcessCaptureOptionsProcessResultModel(
                    result.Process.Id,
                    result.Process.RowVersionId,
                    result.Process.Number,
                    result.Process.Status));
        }

        [Authorize("administrative")]
        [HttpPost("write-configuration")]
        public async Task<ProcessCaptureConfigurationResultModel> WriteConfigurationAsync(
            [FromBody] ProcessCaptureConfigurationModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureConfigurationCommand, ProcessCaptureConfigurationResult>(
                new ProcessCaptureConfigurationCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.ProcessConfigurationRowVersionId,
                    model.MachadoJosephEnabled,
                    model.DocumentIssueDateBypassEnabled,
                    model.ReimbursementLimitBypassEnabled,
                    model.UnemploymentEnabled),
                ct);
            
            return new ProcessCaptureConfigurationResultModel(
                result.ProcessRowVersionId,
                result.ProcessConfigurationRowVersionId);
        }

        [Authorize("administrative")]
        [HttpPost("write-patient")]
        public async Task<ProcessCapturePatientResultModel> WritePatientAsync(
            [FromBody] ProcessCapturePatientModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCapturePatientCommand, ProcessCapturePatientResult>(
                new ProcessCapturePatientCommand(
                    User.GetId(),
                    model.ProcessId,
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
        [HttpPost("write-family-income-statement")]
        public async Task<ProcessCaptureFamilyIncomeStatementResultModel> WriteFamilyIncomeStatementAsync(
            [FromBody] ProcessCaptureFamilyIncomeStatementModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureFamilyIncomeStatementCommand, ProcessCaptureFamilyIncomeStatementResult>(
                new ProcessCaptureFamilyIncomeStatementCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.ProcessPatientFamilyIncomeStatementRowVersionId,
                    model.Year,
                    model.AccessCode,
                    model.FamilyMemberCount,
                    model.FamilyIncome),
                ct);

            return new ProcessCaptureFamilyIncomeStatementResultModel(
                result.ProcessRowVersionId,
                result.ProcessPatientFamilyIncomeStatementRowVersionId);
        }

        [Authorize("administrative")]
        [HttpPost("write-legal-representative")]
        public async Task<ProcessCaptureLegalRepresentativeResultModel> WriteLegalRepresentativeAsync(
            [FromRoute] Guid processId,
            [FromBody] ProcessCaptureLegalRepresentativeModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCaptureLegalRepresentativeCommand, ProcessCaptureLegalRepresentativeResult>(
                new ProcessCaptureLegalRepresentativeCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.ProcessPatientLegalRepresentativeId,
                    model.Name,
                    model.TaxNumber,
                    model.EmailAddress,
                    model.FaxNumber,
                    model.MobileNumber,
                    model.PhoneNumber),
                ct);

            return new ProcessCaptureLegalRepresentativeResultModel(
                result.ProcessRowVersionId,
                result.ProcessPatientLegalRepresentativeRowVersionId);
        }

        [Authorize("administrative")]
        [HttpPost("write-payment")]
        public async Task<ProcessCapturePaymentResultModel> WritePaymentAsync(
            [FromBody] ProcessCapturePaymentModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<ProcessCapturePaymentCommand, ProcessCapturePaymentResult>(
                new ProcessCapturePaymentCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.ProcessPaymentConfigurationRowVersionId,
                    model.ProcessPaymentWireTransferConfigurationRowVersionId,
                    model.Method,
                    model.Receiver,
                    model.Iban,
                    model.Swift),
                ct);

            return new ProcessCapturePaymentResultModel(
                result.ProcessRowVersionId,
                result.ProcessPaymentConfigurationRowVersionId,
                result.ProcessPaymentWireTransferConfigurationRowVersionId);
        }
    }
}