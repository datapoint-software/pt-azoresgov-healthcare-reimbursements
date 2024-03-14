using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class ProcessConfigurationRepositoryHelper
    {
        internal static async Task<ProcessConfiguration> GetByProcessIdOrThrowBusinessExceptionAsync(
            this IProcessConfigurationRepository processConfiguration, 
            long processId, 
            CancellationToken ct)
        {
            var entity = await processConfiguration.GetByProcessIdAsync(processId, ct);

            if (entity is null)
            {
                throw new BusinessException("An entity was not found matching the internal identifier.")
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
            }

            return entity;
        }
    }
}