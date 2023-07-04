using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInResult
    {
        public SignInResult(Guid userId, Guid userRowVersionId, Guid userSessionId, Guid userSessionRowVersionId, string userSessionSecret)
        {
            UserId = userId;
            UserRowVersionId = userRowVersionId;
            UserSessionId = userSessionId;
            UserSessionRowVersionId = userSessionRowVersionId;
            UserSessionSecret = userSessionSecret;
        }

        public Guid UserId { get; }

        public Guid UserRowVersionId { get; }

        public Guid UserSessionId { get; }

        public Guid UserSessionRowVersionId { get; }

        public string UserSessionSecret { get; }
    }
}