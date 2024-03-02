using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal static class EntityRepositoryExtensions
    {
        internal static async Task<Entity> GetByPublicIdOrThrowBusinessExceptionAsync(this IEntityRepository entities, Guid entityPublicId, CancellationToken ct)
        {
            var entity = await entities.GetByPublicIdAsync(entityPublicId, ct);

            if (entity is null)
            {
                throw new BusinessException("An entity was not found matching the given identifier.")
                    .WithErrorCode("ENFMUP")
                    .WithErrorMessage("O perfil da entidade não foi encontrado.");
            }

            return entity;
        }
    }
}