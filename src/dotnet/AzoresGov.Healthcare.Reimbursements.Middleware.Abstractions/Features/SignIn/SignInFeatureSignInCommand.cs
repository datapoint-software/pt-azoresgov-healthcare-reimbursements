using Datapoint.Mediator;
using System.Net;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInFeatureSignInCommand : Command<SignInFeatureSignInResult>
    {
        public SignInFeatureSignInCommand(string userAgent, IPAddress remoteAddress, string emailAddress, string password, bool persistent)
        {
            UserAgent = userAgent;
            RemoteAddress = remoteAddress;
            EmailAddress = emailAddress;
            Password = password;
            Persistent = persistent;
        }

        public string UserAgent { get; }

        public IPAddress RemoteAddress { get; }

        public string EmailAddress { get; }

        public string Password { get; }

        public bool Persistent { get; }
    }
}
