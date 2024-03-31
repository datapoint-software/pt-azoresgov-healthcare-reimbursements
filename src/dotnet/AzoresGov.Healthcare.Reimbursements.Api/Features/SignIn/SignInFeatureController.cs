using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    [Route("/api/features/sign-in")]
    public sealed class SignInFeatureController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public Task<SignInFeatureOptionsModel> GetOptionsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
