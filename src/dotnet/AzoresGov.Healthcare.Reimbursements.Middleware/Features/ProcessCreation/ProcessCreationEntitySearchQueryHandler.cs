using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Management;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Workers.Features.ProcessCreation;
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
        private readonly IProcessCreationEntitySearchWorker _worker;

        private readonly IEntityRepository _entities;

        private readonly IUserRepository _users;

        private readonly IPermissionRepository _permissions;

        public ProcessCreationEntitySearchQueryHandler(IProcessCreationEntitySearchWorker worker, IEntityRepository entities, IUserRepository users, IPermissionRepository permissions)
        {
            _worker = worker;
            _entities = entities;
            _users = users;
            _permissions = permissions;
        }

        public async Task<ProcessCreationEntitySearchResult> HandleQueryAsync(ProcessCreationEntitySearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            var permission = await _permissions.GetProcessCreationOrThrowInvalidOperationExceptionAsync(ct);

            var results = await _worker.SearchUserEntitiesWithPermissionGrantAsync(
                user.Id,
                permission.Id,
                [ EntityNature.HealthCenter, EntityNature.Office ],
                query.Filter,
                query.Skip ?? 0,
                query.Take ?? 5,
                ct);

            var parentEntities = (await _entities.GetAllByIdAsync(
                results.Select(r => r.ParentEntityId).ToArray(),
                ct))
                
                .ToDictionary(pe => pe.Id);
            
            return new ProcessCreationEntitySearchResult(
                results
                    .Select(r => new ProcessCreationEntitySearchEntityResult(
                        r.PublicId,
                        r.RowVersionId,
                        parentEntities[r.ParentEntityId].PublicId,
                        r.Code,
                        r.Name,
                        r.Nature))
                    .Union(parentEntities.Values.Select(pe => new ProcessCreationEntitySearchEntityResult(
                        pe.PublicId,
                        pe.RowVersionId,
                        null,
                        pe.Code,
                        pe.Name,
                        pe.Nature)))
                    .ToArray(),
                results.Select(r => r.PublicId).ToArray(),
                0);
        }
    }
}