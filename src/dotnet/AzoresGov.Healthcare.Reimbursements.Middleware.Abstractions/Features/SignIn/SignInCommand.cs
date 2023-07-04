using Datapoint.Mediator;
using System.Net;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommand : Command<SignInResult>
    {
        public SignInCommand(string userAgent, IPAddress networkAddress, string emailAddress, string password, bool persistent)
        {
            UserAgent = userAgent;
            NetworkAddress = networkAddress;
            EmailAddress = emailAddress;
            Password = password;
            Persistent = persistent;
        }

        public string UserAgent { get; }

        public IPAddress NetworkAddress { get; }

        public string EmailAddress { get; }

        public string Password { get; }

        public bool Persistent { get; }
    }
}
