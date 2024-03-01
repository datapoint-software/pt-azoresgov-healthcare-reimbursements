using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInResultModel
    {
        public SignInResultModel(IReadOnlyCollection<SignInRoleResultModel> roles, SignInSessionResultModel session, SignInUserResultModel user)
        {
            Roles = roles;
            Session = session;
            User = user;
        }

        public IReadOnlyCollection<SignInRoleResultModel> Roles { get; }
        
        public SignInSessionResultModel Session { get; }

        public SignInUserResultModel User { get; }
    }
}
