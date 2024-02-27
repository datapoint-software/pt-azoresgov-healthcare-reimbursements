using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal static class UserRepositoryExtensions
    {
        public static async Task<User> GetByUserSessionPublicIdOrThrowBusinessExceptionAsync(this IUserRepository users, Guid userSessionPublicId, CancellationToken ct)
        {
            var user = await users.GetByUserSessionPublicIdAsync(userSessionPublicId, ct);

            if (user is null)
            {
                throw new BusinessException("A user was not found matching the given user session identifier.")
                    .WithErrorCode("UNFMUS")
                    .WithErrorMessage("O sessão do utilizador não foi encontrada.");
            }

            return user;
        }
    }
}
