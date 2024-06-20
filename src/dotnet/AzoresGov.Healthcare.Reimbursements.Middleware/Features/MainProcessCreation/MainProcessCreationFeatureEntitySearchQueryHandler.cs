using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureEntitySearchQueryHandler : IQueryHandler<MainProcessCreationFeatureEntitySearchQuery, MainProcessCreationFeatureEntitySearchResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IUserRepository _users;

        public MainProcessCreationFeatureEntitySearchQueryHandler(IEntityRepository entities, IUserRepository users)
        {
            _entities = entities;
            _users = users;
        }

        public async Task<MainProcessCreationFeatureEntitySearchResult> HandleQueryAsync(MainProcessCreationFeatureEntitySearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var totalMatchCount = await _entities.GetCountByUserSearchCriteriaAsync(
                user.Id,
                query.Filter,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

           if (totalMatchCount == 0)
                return new MainProcessCreationFeatureEntitySearchResult(0, [], []);

            var entities = await _entities.GetAllByUserSearchCriteriaAsync(
                user.Id,
                query.Filter,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                query.Skip ?? 0,
                query.Take ?? 25,
                ct);

            var parentEntities = (await _entities.GetAllByIdAsync(
                entities
                    .SelectMany(e => e.Node.Split('/', StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse))
                    .Distinct()
                    .ToArray(),
                ct))
                
                .ToDictionary(e => string.Join('/', e.Node.TrimEnd('/'), e.Id));

            return new MainProcessCreationFeatureEntitySearchResult(
                totalMatchCount,
                entities
                    .Select(e => e.PublicId)
                    .ToArray(),
                entities
                    .Select(e => new MainProcessCreationFeatureEntity(
                        e.PublicId,
                        e.RowVersionId,
                        parentEntities.TryGetValue(e.Node, out var parentEntity)
                            ? parentEntity.PublicId
                            : null,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .Union(
                        parentEntities.Values
                            .Select(e => new MainProcessCreationFeatureEntity(
                                e.PublicId,
                                e.RowVersionId,
                                parentEntities.TryGetValue(e.Node, out var parentEntity)
                                    ? parentEntity.PublicId
                                    : null,
                                e.Code,
                                e.Name,
                                e.Nature)))
                    .ToArray());

        }
    }
}
