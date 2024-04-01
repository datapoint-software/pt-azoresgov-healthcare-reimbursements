using AzoresGov.Healthcare.Reimbursements.Api.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.Identity
{
    [Route("/api/features/identity")]
    public sealed class IdentityFeatureController : Controller
    {
        [Authorize]
        [HttpPost("refresh")]
        public Task<IdentityFeatureRefreshResultModel> RefreshAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
