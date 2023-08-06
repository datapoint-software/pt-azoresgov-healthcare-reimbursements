using AzoresGov.Healthcare.Reimbursements.Middleware.Managers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using Datapoint;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class AuthorizationManagerHelper
    {
        internal static async Task AuthorizeOrThrowBusinessExceptionAsync(this AuthorizationManager authorization, string permissionName, UserEntity user, CancellationToken ct)
        {
            var result = await authorization.AuthorizeAsync(
                permissionName,
                user,
                ct);

            if (result == false)
            {
                throw new BusinessException("User has insufficient permissions to complete this operation.")
                    .WithUserMessage("O perfil do utilizador não tem permissões suficientes para executar esta operação.")
                    .WithCode("KJAXNM");
            }
        }
    }
}
