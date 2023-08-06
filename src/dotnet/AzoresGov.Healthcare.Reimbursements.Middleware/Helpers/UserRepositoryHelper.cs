using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class UserRepositoryHelper
    {
        internal static async Task<UserEntity> GetByPublicIdOrThrowBusinessExceptionAsync(this IUserRepository users, Guid publicId, CancellationToken ct)
        {
            var user = await users.GetByPublicIdAsync(publicId, ct);

            return user ??

                throw new BusinessException("A user was not found matching the given public identifier.")
                    .WithUserMessage("O perfil do utilizador não foi encontrado.")
                    .WithCode("61HDOI");
        }
    }
}
