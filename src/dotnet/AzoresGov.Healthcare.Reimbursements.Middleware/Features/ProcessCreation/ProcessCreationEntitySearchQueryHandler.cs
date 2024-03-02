using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchQueryHandler : IQueryHandler<ProcessCreationEntitySearchQuery, ProcessCreationEntitySearchResult>
    {
        private readonly IEntityParentEntityRepository _entityParentEntities;
        
        private readonly IEntityRepository _entities;

        private readonly IUserRepository _users;

        public ProcessCreationEntitySearchQueryHandler(IEntityParentEntityRepository entityParentEntities, IEntityRepository entities, IUserRepository users)
        {
            _entityParentEntities = entityParentEntities;
            _entities = entities;
            _users = users;
        }

        public async Task<ProcessCreationEntitySearchResult> HandleQueryAsync(ProcessCreationEntitySearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            var entities = await _entities.GetAllByUserSearchCriteriaAsync(
                user.Id,
                query.Filter,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                query.Skip ?? 0,
                query.Take ?? 5,
                ct);

            var entitiesCount = await _entities.GetCountByUserSearchCriteriaAsync(
                user.Id,
                query.Filter,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

            var entityParentEntityIds = await _entityParentEntities.GetParentEntityIdByEntityIdAsync(
                entities.Select(e => e.Id).ToArray(),
                0,
                ct);

            var parentEntities = (await _entities.GetAllByIdAsync(
                entityParentEntityIds.Values.ToArray(),
                ct))
                
                .ToDictionary(pe => pe.Id);
            
            return new ProcessCreationEntitySearchResult(
                entities
                    .Select(e => new ProcessCreationEntityResult(
                        e.PublicId,
                        e.RowVersionId,
                        entityParentEntityIds.TryGetValue(e.Id, out var parentEntityId) 
                            ? parentEntities[parentEntityId].PublicId
                            : null,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .Union(parentEntities.Values.Select(e => new ProcessCreationEntityResult(
                        e.PublicId,
                        e.RowVersionId,
                        null,
                        e.Code,
                        e.Name,
                        e.Nature)))
                    .ToArray(),
                entities
                    .Select(e => e.PublicId)
                    .ToArray(),
                entitiesCount);
        }
    }
}