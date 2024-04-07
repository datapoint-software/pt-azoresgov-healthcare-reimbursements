using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationFeatureOptionsQueryHandler : IQueryHandler<ProcessCreationFeatureOptionsQuery, ProcessCreationFeatureOptionsResult>
    {
        private static readonly IReadOnlyCollection<EntityNature> EntityNatures = [ 
            EntityNature.HealthCenter, 
            EntityNature.Office 
        ];

        private readonly IEntityRepository _entities;

        private readonly IUserRepository _users;

        public ProcessCreationFeatureOptionsQueryHandler(IEntityRepository entities, IUserRepository users)
        {
            _entities = entities;
            _users = users;
        }

        public async Task<ProcessCreationFeatureOptionsResult> HandleQueryAsync(
            ProcessCreationFeatureOptionsQuery query, 
            CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var entityCount = await _entities.GetCountByUserIdAndEntityNatureAsync(
                user.Id,
                EntityNatures,
                ct);

            if (entityCount == 1)
            {
                var entity = await _entities.GetByUserIdAndEntityNaturesAsync(
                    user.Id,
                    EntityNatures,
                    ct);

                Assert.Found(entity);

                var parentEntity = await _entities.GetParentEntityByEntityIdAndParentEntityNatureAsync(
                    entity.Id,
                    EntityNature.Administrative,
                    ct);

                Assert.Found(parentEntity);

                return new ProcessCreationFeatureOptionsResult(
                    false,
                    [
                        new ProcessCreationFeatureOptionsResultEntity(
                            entity.PublicId,
                            entity.RowVersionId,
                            parentEntity.PublicId,
                            entity.Code,
                            entity.Name,
                            entity.Nature),
                        new ProcessCreationFeatureOptionsResultEntity(
                            parentEntity.PublicId,
                            parentEntity.RowVersionId,
                            null,
                            parentEntity.Code,
                            parentEntity.Name,
                            parentEntity.Nature)
                    ],
                    entity.PublicId);
            }

            return new ProcessCreationFeatureOptionsResult(
                true,
                null,
                null);
        }
    }
}
