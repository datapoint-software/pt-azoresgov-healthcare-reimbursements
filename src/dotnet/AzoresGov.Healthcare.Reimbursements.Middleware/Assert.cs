using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal static class Assert
    {
        internal static void ProcessStatus(ProcessStatus processStatusExpectation, ProcessStatus processStatus)
        {
            if (processStatus == processStatusExpectation)
                return;

            throw new BusinessException("Process status mismatch.")
                .WithErrorMessage("O estado do processo não é compatível com esta operação.");
        }

        internal static async Task UserEntityAccessAsync(
            IUserEntityRepository userEntities,
            long userId,
            long entityId,
            CancellationToken ct)
        {
            if (await userEntities.AnyByUserIdAndEntityIdAsync(userId, entityId, ct))
                return;

            throw new BusinessException("User is not allowed access to this entity.")
                .WithErrorMessage("O perfil do utilizador não tem acesso a esta entidade.");
        }
    }
}