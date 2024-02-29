using Datapoint;
using Datapoint.UnitOfWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Management
{
    internal static class RepositoryExtensions
    {
        internal static async Task<TEntity> GetByPublicIdOrThrowInvalidOperationException<TEntity>(this IRepository<TEntity> entities, Guid publicId, CancellationToken ct) where TEntity : class, IEntity
        {
            var entity = await entities.GetByPublicIdAsync(
                publicId, 
                ct);

            if (entity is null)
            {
                throw new InvalidOperationException("Entity was not found.")
                    .WithErrorCode("MGMENF");
            }

            return entity;
        }
    }
}