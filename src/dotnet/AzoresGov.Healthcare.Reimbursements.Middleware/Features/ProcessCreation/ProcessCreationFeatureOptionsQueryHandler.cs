using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                return new ProcessCreationFeatureOptionsResult(
                    false,
                    [
                        new ProcessCreationFeatureOptionsResultEntity(
                            entity.PublicId,
                            entity.RowVersionId,
                            entity.Code,
                            entity.Name,
                            entity.Nature)
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
