using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsQueryHandler : IQueryHandler<MainProcessCreationFeatureOptionsQuery, MainProcessCreationFeatureOptionsResult>
    {
        private readonly IEntityRepository _entities;
        
        private readonly IUserRepository _users;

        public MainProcessCreationFeatureOptionsQueryHandler(IEntityRepository entities, IUserRepository users)
        {
            _entities = entities;
            _users = users;
        }

        public async Task<MainProcessCreationFeatureOptionsResult> HandleQueryAsync(MainProcessCreationFeatureOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var entityCount = await _entities.GetCountByUserIdAndEntityNatureAsync(
                user.Id,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

            if (entityCount is not 1)
                return new MainProcessCreationFeatureOptionsResult(null);

            var entity = await _entities.GetByUserIdAndEntityNatureAsync(
                user.Id,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

            Assert.Found(entity);

            var entityNodes = (await _entities.GetAllByIdAsync(
                entity.Node.Split('/', StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray(),
                ct))

                .ToDictionary(e => string.Join('/', e.Node.TrimEnd('/'), e.Id));

            return new MainProcessCreationFeatureOptionsResult(
                new MainProcessCreationFeatureOptionsResultEntitySelection(
                    entity.PublicId,
                    entityNodes.Values
                        .Select(e => new MainProcessCreationFeatureEntity(
                            e.PublicId,
                            e.RowVersionId,
                            entityNodes.TryGetValue(e.Node, out var node)
                                ? node.PublicId
                                : null,
                            e.Code,
                            e.Name,
                            e.Nature))
                        .Union([
                            new MainProcessCreationFeatureEntity(
                                entity.PublicId,
                                entity.RowVersionId,
                                entityNodes.TryGetValue(entity.Node, out var node)
                                    ? node.PublicId
                                    : null,
                                entity.Code,
                                entity.Name,
                                entity.Nature)
                        ])
                        .ToArray()));
        }
    }
}
