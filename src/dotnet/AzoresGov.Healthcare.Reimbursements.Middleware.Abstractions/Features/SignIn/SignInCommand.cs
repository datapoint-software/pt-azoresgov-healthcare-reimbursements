using Datapoint.Mediator;
using System.Net;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommand : Command<SignInResult>
    {
        public SignInCommand(string userAgent, IPAddress userNetworkAddress, string emailAddress, string password, bool persistent)
        {
            UserAgent = userAgent;
            UserNetworkAddress = userNetworkAddress;
            EmailAddress = emailAddress;
            Password = password;
            Persistent = persistent;
        }

        public string UserAgent { get; }

        public IPAddress UserNetworkAddress { get; }

        public string EmailAddress { get; }

        public string Password { get; }

        public bool Persistent { get; }
    }
}
