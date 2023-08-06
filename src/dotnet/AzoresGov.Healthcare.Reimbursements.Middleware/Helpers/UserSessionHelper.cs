using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class UserSessionHelper
    {
        private static readonly Random Random = new ();

        internal static string CreateSecret()
        {
            var buffer = new byte[32];

            Random.NextBytes(buffer);

            return Convert.ToBase64String(buffer);
        }
    }
}
