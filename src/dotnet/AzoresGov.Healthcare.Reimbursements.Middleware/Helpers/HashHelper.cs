using System;
using System.Security.Cryptography;
using System.Text;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class HashHelper
    {
        internal static string ComputeHash(string input) =>
            Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(input)));
    }
}
