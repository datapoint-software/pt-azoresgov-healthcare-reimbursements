using Datapoint.Mediator;
using System.Net;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn
{
    public sealed class SignInCommand : Command<SignInResult>
    {
        public SignInCommand(string userAgent, IPAddress networkAddress, string emailAddress, string password)
        {
            UserAgent = userAgent;
            NetworkAddress = networkAddress;
            EmailAddress = emailAddress;
            Password = password;
        }

        public string UserAgent { get; }

        public IPAddress NetworkAddress { get; }

        public string EmailAddress { get; }

        public string Password { get; }
    }
}
