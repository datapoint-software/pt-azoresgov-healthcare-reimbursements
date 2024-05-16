using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreationEntitySelection
{
    public sealed class ProcessCreationEntitySelectionSearchQueryHandler : IQueryHandler<ProcessCreationEntitySelectionSearchQuery, ProcessCreationEntitySelectionSearchResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IEntityParentEntityRepository _entityParentEntities;

        private readonly IUserRepository _users;

        public ProcessCreationEntitySelectionSearchQueryHandler(IEntityRepository entities, IEntityParentEntityRepository entityParentEntities, IUserRepository users)
        {
            _entities = entities;
            _entityParentEntities = entityParentEntities;
            _users = users;
        }

        public async Task<ProcessCreationEntitySelectionSearchResult> HandleQueryAsync(ProcessCreationEntitySelectionSearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(query.UserId, ct);

            Assert.Found(user);

            var entities = await _entities.GetAllBySearchCriteriaAsync(
                user.Id,
                query.Filter,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                query.Skip ?? 0,
                query.Take ?? 25,
                ct);

            var entityParentEntities = (
                await _entityParentEntities.GetAllByEntityIdAndParentEntityNatureAsync(
                    entities
                        .Select(e => e.Id)
                        .ToArray(),
                    EntityNature.Administrative,
                    ct)
            )
                .ToDictionary(epe => epe.EntityId);

            var parentEntities = (
                await _entities.GetAllByIdAsync(
                    entityParentEntities
                        .Values
                        .Select(e => e.ParentEntityId)
                        .ToArray(),
                    ct)
            )
                .ToDictionary(e => e.Id);

            var totalMatchCount = await _entities.GetCountBySearchCriteriaAsync(
                user.Id,
                query.Filter,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

            return new ProcessCreationEntitySelectionSearchResult(
                totalMatchCount,
                entities
                    .Select(e => e.PublicId)
                    .ToArray(),
                entities
                    .Select(e => new ProcessCreationEntitySelectionSearchResultEntity(
                        e.PublicId,
                        e.RowVersionId,
                        entityParentEntities.TryGetValue(e.Id, out var entityParentEntity) && parentEntities.TryGetValue(entityParentEntity.ParentEntityId, out var parentEntity) 
                            ? parentEntity.PublicId
                            : null,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .Union(parentEntities.Values.Select(e => new ProcessCreationEntitySelectionSearchResultEntity(
                        e.PublicId,
                        e.RowVersionId,
                        null,
                        e.Code,
                        e.Name,
                        e.Nature)))
                    .ToArray());
                    
        }
    }
}
