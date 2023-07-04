using System;
using System.Security.Cryptography;
using System.Text;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class HashHelper
    {
        internal static string ComputeHash(string input)
        {
            using MD5 algorithm = MD5.Create();

            return Convert.ToHexString(
                algorithm.ComputeHash(
                    Encoding.ASCII.GetBytes(input)));
        }
    }
}
