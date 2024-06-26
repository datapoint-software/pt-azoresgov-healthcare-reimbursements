﻿using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.UnitOfWork;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

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
                    .WithErrorMessage("Os registos associados a esta operação não foram encontrados.");
            }

            if (rowVersionId is null)
                throw new ConcurrencyException("Row version identifier is missing.");

            if (!entity.RowVersionId.Equals(rowVersionId))
            {
                throw new ConcurrencyException("Row version identifier mismatch.")
                    .WithErrorMessage("Os registos associados a esta operação foram modificados por outro utilizador.");
            }
        }

        internal static async Task UserEntityAccessAsync(IUserEntityRepository userEntities, User user, Entity entity, CancellationToken ct)
        {
            var found = await userEntities.AnyByUserIdAndEntityIdAsync(
                user.Id,
                entity.Id,
                ct);

            if (found)
                return;

            throw new AuthorizationException("User does not have access to this entity.")
                .WithErrorMessage("O utilizador não está autorizado a aceder aos registos da entidade pretendida.");
        }
    }
}
