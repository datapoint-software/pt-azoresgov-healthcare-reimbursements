using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    [Route("/api/features/main-process-capture")]
    public sealed class MainProcessCaptureFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public MainProcessCaptureFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("get-options")]
        public async Task<MainProcessCaptureFeatureOptionsResultModel> GetOptionsAsync(
            [FromBody] MainProcessCaptureFeatureOptionsModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCaptureFeatureOptionsQuery, MainProcessCaptureFeatureOptionsResult>(
                new MainProcessCaptureFeatureOptionsQuery(
                    User.GetId(),
                    model.ProcessId),
                ct);

            return new MainProcessCaptureFeatureOptionsResultModel(
                result.Entities
                    .Select(e => new MainProcessCaptureFeatureEntityModel(
                        e.Id,
                        e.RowVersionId,
                        e.ParentEntityId,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                new MainProcessCaptureFeaturePatientModel(
                    result.Patient.Id,
                    result.Patient.RowVersionId,
                    result.Patient.EntityId,
                    result.Patient.Number,
                    result.Patient.TaxNumber,
                    result.Patient.Name,
                    result.Patient.Birth,
                    result.Patient.Death,
                    result.Patient.FaxNumber,
                    result.Patient.MobileNumber,
                    result.Patient.PhoneNumber,
                    result.Patient.EmailAddress,
                    result.Patient.PostalAddressArea,
                    result.Patient.PostalAddressAreaCode,
                    result.Patient.PostalAddressLine1,
                    result.Patient.PostalAddressLine2,
                    result.Patient.PostalAddressLine3),
                new MainProcessCaptureFeatureProcessModel(
                    result.Process.Id,
                    result.Process.RowVersionId,
                    result.Process.EntityId,
                    result.Process.Number,
                    result.Process.Creation));
        }

        [Administrative]
        [HttpPost("search-legal-representative")]
        public async Task<MainProcessCaptureFeatureLegalRepresentativeSearchResultModel> SearchLegalRepresentativeAsync(
            [FromBody] MainProcessCaptureFeatureLegalRepresentativeSearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCaptureFeatureLegalRepresentativeSearchQuery, MainProcessCaptureFeatureLegalRepresentativeSearchResult>(
                new MainProcessCaptureFeatureLegalRepresentativeSearchQuery(
                    User.GetId(),
                    model.TaxNumber),
                ct);

            return new MainProcessCaptureFeatureLegalRepresentativeSearchResultModel(
                result.LegalRepresentative is null
                    ? null
                    : new MainProcessCaptureFeatureLegalRepresentativeModel(
                        result.LegalRepresentative.Id,
                        result.LegalRepresentative.RowVersionId,
                        result.LegalRepresentative.TaxNumber,
                        result.LegalRepresentative.Name,
                        result.LegalRepresentative.FaxNumber,
                        result.LegalRepresentative.MobileNumber,
                        result.LegalRepresentative.PhoneNumber,
                        result.LegalRepresentative.EmailAddress,
                        result.LegalRepresentative.PostalAddressArea,
                        result.LegalRepresentative.PostalAddressAreaCode,
                        result.LegalRepresentative.PostalAddressLine1,
                        result.LegalRepresentative.PostalAddressLine2,
                        result.LegalRepresentative.PostalAddressLine3));
        }

        [Administrative]
        [HttpPost("update-patient")]
        public async Task<MainProcessCaptureFeaturePatientSubmitResultModel> UpdatePatientAsync(
            [FromBody] MainProcessCaptureFeaturePatientSubmitModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<MainProcessCaptureFeaturePatientSubmitCommand, MainProcessCaptureFeaturePatientSubmitResult>(
                new MainProcessCaptureFeaturePatientSubmitCommand(
                    User.GetId(),
                    model.ProcessId,
                    model.ProcessRowVersionId,
                    model.PatientId,
                    model.PatientRowVersionId,
                    model.FaxNumber,
                    model.MobileNumber,
                    model.PhoneNumber,
                    model.EmailAddress,
                    model.PostalAddressArea,
                    model.PostalAddressAreaCode,
                    model.PostalAddressLine1,
                    model.PostalAddressLine2,
                    model.PostalAddressLine3),
                ct);

            return new MainProcessCaptureFeaturePatientSubmitResultModel(
                result.ProcessRowVersionId,
                result.PatientRowVersionId);
        }
    }
}
