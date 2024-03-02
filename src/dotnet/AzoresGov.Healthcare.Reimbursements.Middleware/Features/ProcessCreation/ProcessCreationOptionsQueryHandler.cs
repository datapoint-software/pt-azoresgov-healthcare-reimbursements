using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsQueryHandler : IQueryHandler<ProcessCreationOptionsQuery, ProcessCreationOptionsResult>
    {
        private readonly IEntityRepository _entities;
        
        private readonly IUserRepository _users;

        private readonly IUserEntityRepository _userEntities;

        public ProcessCreationOptionsQueryHandler(IEntityRepository entities, IUserRepository users, IUserEntityRepository userEntities)
        {
            _entities = entities;
            _users = users;
            _userEntities = userEntities;
        }

        public async Task<ProcessCreationOptionsResult> HandleQueryAsync(ProcessCreationOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            var userEntityCount = await _userEntities.CountByUserIdAndEntityNatureAsync(
                user.Id,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                ct);

            Guid? entityId;
            List<ProcessCreationEntityResult>? entities;

            if (userEntityCount == 1)
            {
                var entity = await _entities.GetSingleByUserIdAndNatureAsync(
                    user.Id,
                    [ EntityNature.HealthCenter, EntityNature.Office ],
                    ct);

                var parentEntity = await _entities.GetParentEntityByEntityIdAsync(
                    entity.Id,
                    0,
                    ct);

                entities = new List<ProcessCreationEntityResult>(2)
                {
                    {
                        new ProcessCreationEntityResult(
                            entity.PublicId,
                            entity.RowVersionId,
                            parentEntity?.PublicId,
                            entity.Code,
                            entity.Name,
                            entity.Nature)
                    }
                };

                if (parentEntity is not null)
                {
                    entities.Add(new ProcessCreationEntityResult(
                        parentEntity.PublicId,
                        parentEntity.RowVersionId,
                        null,
                        parentEntity.Code,
                        parentEntity.Name,
                        parentEntity.Nature));
                }

                entityId = entity.PublicId;
            }
            else
            {
                entities = null;
                entityId = null;
            }

            return new ProcessCreationOptionsResult(
                entities,
                entityId);
        }
    }
}