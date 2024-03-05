using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Identity;
using Datapoint.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    [Route("/api/features/identity")]
    public sealed class IdentityController : Controller
    {
        private readonly IMediator _mediator;

        public IdentityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IdentityResultModel> GetIdentityAsync(CancellationToken ct)
        {
            var result = await _mediator.HandleQueryAsync<IdentityQuery, IdentityResult>(
                new IdentityQuery(
                    User.GetId()),
                ct);

            return new IdentityResultModel(
                new IdentityUserResultModel(
                    result.User.Id,
                    result.User.Name,
                    result.User.EmailAddress));
        }
    }
}
