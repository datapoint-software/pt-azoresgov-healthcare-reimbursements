using Microsoft.AspNetCore.Authorization;

namespace AzoresGov.Healthcare.Reimbursements.Api.Attributes
{
    public sealed class AdministrativeAttribute : AuthorizeAttribute
    {
        public AdministrativeAttribute() : base("Administrative")
        {
        }
    }
}
