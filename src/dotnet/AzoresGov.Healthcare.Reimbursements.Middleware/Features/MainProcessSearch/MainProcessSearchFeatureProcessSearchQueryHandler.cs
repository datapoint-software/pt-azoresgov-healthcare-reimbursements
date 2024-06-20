using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessSearchQueryHandler : IQueryHandler<MainProcessSearchFeatureProcessSearchQuery, MainProcessSearchFeatureProcessSearchResult>
    {
        private static MainProcessSearchFeatureProcessSearchResult EmptyResult = new MainProcessSearchFeatureProcessSearchResult(
            0,
            [],
            [],
            [],
            []);

        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessSearchFeatureProcessSearchQueryHandler(IEntityRepository entities, IPatientRepository patients, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessSearchFeatureProcessSearchResult> HandleQueryAsync(MainProcessSearchFeatureProcessSearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var userEntityIds = await _userEntities.GetAllEntityIdByUserIdAsync(
                user.Id,
                ct);

            if (userEntityIds.Count == 0)
                return EmptyResult;

            var processCount = await (
                string.IsNullOrEmpty(query.Filter) ? _processes.GetCountByEmptySearchCriteriaAsync(userEntityIds, ct) :
                query.UseFullSearchCriteria ? _processes.GetCountByFullSearchCriteriaAsync(userEntityIds, query.Filter, ct) :
                    _processes.GetCountByBasicSearchCriteriaAsync(userEntityIds, query.Filter, ct));

            if (processCount == 0)
                return EmptyResult;

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 25;

            var processes = await (
                string.IsNullOrEmpty(query.Filter) ? _processes.GetAllByEmptySearchCriteriaAsync(userEntityIds, skip, take, ct) :
                query.UseFullSearchCriteria ? _processes.GetAllByFullSearchCriteriaAsync(userEntityIds, query.Filter, skip, take, ct) :
                    _processes.GetAllByBasicSearchCriteriaAsync(userEntityIds, query.Filter, skip, take, ct));

            var patients = (await _patients.GetAllByPatientIdAsync(
                processes
                    .Select(p => p.PatientId)
                    .ToArray(),
                ct))
                
                .ToDictionary(p => p.Id);

            var entities = (await _entities.GetAllByIdAsync(
                patients.Values
                    .Select(p => p.EntityId)
                    .Union(processes.Select(p => p.EntityId))
                    .Distinct()
                    .ToArray(),
                ct))
                
                .ToDictionary(e => e.Id);

            entities = entities.Values.Union(
                await _entities.GetAllByIdAsync(
                    entities.Values
                        .SelectMany(e => e.Node.Split('/', StringSplitOptions.RemoveEmptyEntries))
                        .Select(long.Parse)
                        .Distinct()
                        .ToArray(),
                    ct))
                .ToDictionary(e => e.Id);

            var parentEntities = entities.Values
                .ToDictionary(e => string.Join('/', e.Node, e.Id));

            return new MainProcessSearchFeatureProcessSearchResult(
                processCount,
                processes
                    .Select(p => p.PublicId)
                    .ToArray(),
                entities.Values
                    .Select(e => new MainProcessSearchFeatureEntity(
                        e.PublicId,
                        e.RowVersionId,
                        parentEntities.TryGetValue(e.Node, out var parentEntity)
                            ? parentEntity.PublicId
                            : null,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                patients.Values
                    .Select(p => new MainProcessSearchFeaturePatient(
                        p.PublicId,
                        p.RowVersionId,
                        entities[ p.EntityId ].PublicId,
                        p.Number,
                        p.TaxNumber,
                        p.Name,
                        p.Death))
                    .ToArray(),
                processes
                    .Select(p => new MainProcessSearchFeatureProcess(
                        p.PublicId,
                        p.RowVersionId,
                        entities[ p.EntityId ].PublicId,
                        patients[ p.PatientId ].PublicId,
                        p.Number,
                        p.Status,
                        p.Creation))
                    .ToArray());
        }
    }
}
