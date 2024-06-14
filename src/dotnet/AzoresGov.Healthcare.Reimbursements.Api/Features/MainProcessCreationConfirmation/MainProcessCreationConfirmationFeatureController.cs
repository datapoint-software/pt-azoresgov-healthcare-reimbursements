using AzoresGov.Healthcare.Reimbursements.Api.Attributes;
using AzoresGov.Healthcare.Reimbursements.Api.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationConfirmation;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationConfirmation
{
    [Route("/api/features/main-process-creation-confirmation")]
    public sealed class MainProcessCreationConfirmationFeatureController : Controller
    {
        private readonly IMediator _mediator;

        public MainProcessCreationConfirmationFeatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Administrative]
        [HttpPost("confirm")]
        public async Task<MainProcessCreationConfirmationFeatureConfirmResultModel> ConfirmAsync(
            [FromBody] MainProcessCreationConfirmationFeatureConfirmModel model,
            CancellationToken ct)
        {
            var result = await _mediator.HandleCommandAsync<MainProcessCreationConfirmationFeatureConfirmCommand, MainProcessCreationConfirmationFeatureConfirmResult>(
                new MainProcessCreationConfirmationFeatureConfirmCommand(
                    User.GetId(),
                    model.EntityId,
                    model.EntityRowVersionId,
                    model.PatientId,
                    model.PatientRowVersionId),
                ct);

            return new MainProcessCreationConfirmationFeatureConfirmResultModel(
                result.ProcessId,
                result.ProcessRowVersionId);
        }
    }
}
