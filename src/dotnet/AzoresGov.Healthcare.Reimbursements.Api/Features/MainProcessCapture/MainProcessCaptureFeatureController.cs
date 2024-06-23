﻿using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
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
    }
}
