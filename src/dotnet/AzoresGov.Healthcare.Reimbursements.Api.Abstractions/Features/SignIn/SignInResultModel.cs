using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInResultModel
    {
        public SignInResultModel(
            IReadOnlyCollection<SignInEntityResultModel> entities, 
            IReadOnlyCollection<SignInPermissionResultModel> permissions, 
            SignInUserResultModel user, 
            SignInUserSessionResultModel userSession)
        {
            Entities = entities;
            Permissions = permissions;
            User = user;
            UserSession = userSession;
        }

        public IReadOnlyCollection<SignInEntityResultModel> Entities { get; }

        public IReadOnlyCollection<SignInPermissionResultModel> Permissions { get; }

        public SignInUserResultModel User { get; }

        public SignInUserSessionResultModel UserSession { get; }
    }
}
