using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInModel
    {
        public SignInModel(string emailAddress, string password, bool persistent)
        {
            EmailAddress = emailAddress;
            Password = password;
            Persistent = persistent;
        }

        public string EmailAddress { get; }

        public string Password { get; }

        public bool Persistent { get; }
    }
}
