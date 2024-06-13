using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationPatientSelection;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationPatientSelection
{
    [Route("/api/features/main-process-creation-patient-selection")]
    public sealed class MainProcessCreationPatientSelectionFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public MainProcessCreationPatientSelectionFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("search")]
        public async Task<MainProcessCreationPatientSelectionFeatureSearchResultModel> SearchAsync(
            [FromBody] MainProcessCreationPatientSelectionFeatureSearchModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<MainProcessCreationPatientSelectionFeatureSearchQuery, MainProcessCreationPatientSelectionFeatureSearchResult>(
                new MainProcessCreationPatientSelectionFeatureSearchQuery(
                    User.GetId(),
                    model.EntityId,
                    model.EntityRowVersionId,
                    model.Filter,
                    (MainProcessCreationPatientSelectionFeatureSearchMode) model.Mode,
                    model.Skip,
                    model.Take),
                ct);

            return new MainProcessCreationPatientSelectionFeatureSearchResultModel(
                result.TotalMatchCount,
                result.PatientIds,
                result.Patients
                    .Select(p => new MainProcessCreationPatientSelectionFeatureSearchResultPatientModel(
                        p.Id,
                        p.RowVersionId,
                        p.Number,
                        p.TaxNumber,
                        p.Name,
                        p.Birth,
                        p.Death,
                        p.External,
                        p.FaxNumber,
                        p.MobileNumber,
                        p.PhoneNumber,
                        p.EmailAddress,
                        p.PostalAddressArea,
                        p.PostalAddressAreaCode,
                        p.PostalAddressLine1,
                        p.PostalAddressLine2,
                        p.PostalAddressLine3))
                    .ToArray());
        }
    }
}
