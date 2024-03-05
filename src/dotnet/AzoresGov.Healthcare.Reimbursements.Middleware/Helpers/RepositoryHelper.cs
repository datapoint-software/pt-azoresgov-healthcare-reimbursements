using Datapoint;
using Datapoint.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Helpers
{
    internal static class RepositoryHelper
    {
        internal static async Task<TEntity> GetByIdOrThrowExceptionAsync<TEntity>(
            this IRepository<TEntity> entities, 
            long id, 
            CancellationToken ct)
            where TEntity : class, IEntity
        {
            var entity = await entities.GetByIdAsync(id, ct);

            if (entity is null)
            {
                throw new BusinessException("An entity was not found matching the internal identifier.")
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
            }

            return entity;
        }
        
        internal static async Task<TEntity> GetByPublicIdOrThrowExceptionAsync<TEntity>(
            this IRepository<TEntity> entities, 
            Guid publicId, 
            CancellationToken ct)
            where TEntity : class, IEntity
        {
            var entity = await entities.GetByPublicIdAsync(publicId, ct);

            if (entity is null)
            {
                throw new BusinessException("An entity was not found matching the public identifier.")
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
            }

            return entity;
        }
        
        internal static async Task<TEntity> GetByPublicIdOrThrowExceptionAsync<TEntity>(
            this IRepository<TEntity> entities, 
            Guid publicId, 
            Guid rowVersionId,
            CancellationToken ct)
            where TEntity : class, IEntity
        {
            var entity = await GetByPublicIdOrThrowExceptionAsync(
                entities,
                publicId,
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