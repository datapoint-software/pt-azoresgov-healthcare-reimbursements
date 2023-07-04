using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.SignIn
{
    public sealed class SignInModel
    {
        public SignInModel(string emailAddress, string password)
        {
            EmailAddress = emailAddress;
            Password = password;
        }

        public string EmailAddress { get; }

        public string Password { get; }
    }
}
