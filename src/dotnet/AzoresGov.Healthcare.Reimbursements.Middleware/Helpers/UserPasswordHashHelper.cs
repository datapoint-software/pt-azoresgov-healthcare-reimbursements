using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class UserPasswordHashHelper
    {
        private const bool BCryptEnhancedEntropy = true;

        private const BCrypt.Net.HashType BCryptHashType = BCrypt.Net.HashType.SHA384;

        private const char BCryptMinorRevision = 'b';

        internal static string ComputePasswordHash(string password, int workFactor) =>

            BCrypt.Net.BCrypt.HashPassword(
                password,
                BCrypt.Net.BCrypt.GenerateSalt(
                    workFactor,
                    BCryptMinorRevision),
                BCryptEnhancedEntropy,
                BCryptHashType);

        internal static bool ValidatePassword(string password, string passwordHash) =>

            BCrypt.Net.BCrypt.Verify(
                password,
                passwordHash,
                BCryptEnhancedEntropy,
                BCryptHashType);

        internal static bool ValidatePasswordHash(string passwordHash, int workFactor) =>

            !BCrypt.Net.BCrypt.PasswordNeedsRehash(
                passwordHash,
                workFactor);
    }
}
