using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class ProcessPatientRepositoryHelper
    {
        internal static async Task<ProcessPatient> GetByProcessIdOrThrowExceptionAsync(
            this IProcessPatientRepository processPatients, 
            long processId, 
            CancellationToken ct)
        {
            var entity = await processPatients.GetByProcessIdAsync(processId, ct);

            if (entity is null)
            {
                throw new BusinessException("An entity was not found matching the internal identifier.")
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
            }

            return entity;
        }
        
        internal static async Task<ProcessPatient> GetByProcessIdOrThrowExceptionAsync(
            this IProcessPatientRepository processPatients, 
            long processId, 
            Guid rowVersionId,
            CancellationToken ct)
        {
            var entity = await GetByProcessIdOrThrowExceptionAsync(
                processPatients,
                processId,
                ct);

            if (!entity.RowVersionId.Equals(rowVersionId))
            {
                throw new ConcurrencyException("Row version identifier mismatch.")
                    .WithErrorMessage("Os registos associados a esta operação foram modificados entretanto.");
            }

            return entity;
        }
    }
}