using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchQueryHandler : IQueryHandler<ProcessSearchQuery, ProcessSearchResult>
    {
        private readonly IEntityRepository _entities;
        
        private readonly IUserEntityRepository _userEntities;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IProcessConfigurationRepository _processSettings;

        private readonly IUserRepository _users;

        public ProcessSearchQueryHandler(IEntityRepository entities, IUserEntityRepository userEntities, IPatientRepository patients, IProcessRepository processes, IProcessConfigurationRepository processSettings, IUserRepository users)
        {
            _entities = entities;
            _userEntities = userEntities;
            _patients = patients;
            _processes = processes;
            _processSettings = processSettings;
            _users = users;
        }

        public async Task<ProcessSearchResult> HandleQueryAsync(ProcessSearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            var entityId = await GetEntityIdAsync(
                query.EntityId,
                user.Id,
                ct);

            var processes = await _processes.GetAllBySearchCriteriaAsync(
                query.Filter,
                entityId,
                query.Status,
                query.Skip ?? 0,
                query.Take ?? 25,
                ct);

            var totalMatchCount = await _processes.CountBySearchCriteriaAsync(
                query.Filter,
                entityId,
                query.Status,
                ct);

            var entities = (await _entities.GetAllByIdAsync(
                processes.Select(p => p.EntityId).Distinct().ToArray(),
                ct))
                
                .ToDictionary(e => e.Id);
            
            var patients = (await _patients.GetAllByIdAsync(
                processes.Select(p => p.PatientId).Distinct().ToArray(),
                ct))
                
                .ToDictionary(p => p.Id);

            var settings = (await _processSettings.GetAllByProcessIdAsync(
                processes.Select(p => p.Id).ToArray(),
                ct))

                .ToDictionary(c => c.ProcessId);

            return new ProcessSearchResult(
                entities.Values
                    .Select(e => new ProcessSearchEntityResult(
                        e.PublicId,
                        e.Code,
                        e.Name))
                    .ToArray(),
                patients.Values
                    .Select(p => new ProcessSearchPatientResult(
                        p.PublicId,
                        p.Name,
                        p.Gender,
                        p.HealthNumber,
                        p.TaxNumber,
                        p.Birth,
                        p.Death))
                    .ToArray(),
                processes
                    .Select(p => new ProcessSearchProcessResult(
                        p.PublicId,
                        entities[ p.EntityId ]!.PublicId,
                        patients[ p.PatientId ]!.PublicId,
                        p.Number,
                        p.Status,
                        settings.TryGetValue(p.Id, out var configuration) && configuration.MachadoJosephEnabled,
                        configuration?.DocumentIssueDateBypassEnabled ?? false,
                        configuration?.ReimbursementLimitBypassEnabled ?? false,
                        p.Creation,
                        p.Expiration,
                        p.Touch
                    ))
                    .ToArray(),
                totalMatchCount);
        }

        private async Task<IReadOnlyCollection<long>> GetEntityIdAsync(Guid? entityPublicId, long userId, CancellationToken ct)
        {
            if (entityPublicId is null)
            {
                return await _userEntities.GetAllEntityIdByUserIdAndEntityNatureAsync(
                    userId,
                    [ EntityNature.HealthCenter, EntityNature.Office ],
                    ct);
            }

            var entity = await _entities.GetByPublicIdOrThrowBusinessExceptionAsync(
                entityPublicId.Value,
                ct);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                userId,
                entity.Id,
                ct);

            return [ entity.Id ];
        }
    }
}