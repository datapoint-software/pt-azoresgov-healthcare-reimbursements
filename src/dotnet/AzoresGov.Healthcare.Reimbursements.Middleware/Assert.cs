using Datapoint;
using Datapoint.UnitOfWork;
using System;
using System.Diagnostics.CodeAnalysis;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal static class Assert
    {
        internal static void Found<TEntity>([NotNull] TEntity? entity) where TEntity : class, IEntity
        {
            if (entity is null)
                throw new InvalidOperationException("An entity was not found.")
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
        }

        internal static void Found<TEntity>([NotNull] TEntity? entity, [NotNull] Guid? rowVersionId) where TEntity : class, IEntity
        {
            if (entity is null)
            {
                throw new InvalidOperationException("Entity was not found.")
                    .WithErrorMessage("A informação necessária não foi encontrada.");
            }

            if (rowVersionId is null)
                throw new ConcurrencyException("Row version identifier is missing.");

            if (!entity.PublicId.Equals(rowVersionId))
            {
                throw new ConcurrencyException("Row version identifier mismatch.")
                    .WithErrorMessage("A informação foi alterada entretanto.");
            }
        }
    }
}
