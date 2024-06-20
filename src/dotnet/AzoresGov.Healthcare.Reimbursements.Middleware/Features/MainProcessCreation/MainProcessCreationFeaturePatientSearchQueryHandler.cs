using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeaturePatientSearchQueryHandler : IQueryHandler<MainProcessCreationFeaturePatientSearchQuery, MainProcessCreationFeaturePatientSearchResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;
        
        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessCreationFeaturePatientSearchQueryHandler(IEntityRepository entities, IPatientRepository patients, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessCreationFeaturePatientSearchResult> HandleQueryAsync(MainProcessCreationFeaturePatientSearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var entity = await _entities.GetByPublicIdAsync(
                query.EntityId,
                ct);

            Assert.Found(entity, query.EntityRowVersionId);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user,
                entity,
                ct);

            var userEntities = await _userEntities.GetAllEntityIdByUserIdAsync(
                user.Id,
                ct);

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 25;

            var patientCount = await (query.UseFullSearchCriteria
                ? _patients.GetCountByFullSearchCriteriaAsync(query.Filter, ct)
                : _patients.GetCountByPatientNumberCriteriaAsync(query.Filter, ct));

            if (patientCount is 0)
                return new MainProcessCreationFeaturePatientSearchResult(0, [], [], []);

            var patients = await (query.UseFullSearchCriteria
                ? _patients.GetAllByFullSearchCriteriaAsync(query.Filter, skip, take, ct)
                : _patients.GetAllByPatientNumberCriteriaAsync(query.Filter, ct));

            var entities = (await _entities.GetAllByIdAsync(
                patients.Select(e => e.EntityId).ToArray(),
                ct))
                
                .ToDictionary(e => e.Id);

            var parentEntities = (await _entities.GetAllByIdAsync(
                entities.Values
                    .SelectMany(e => e.Node.Split('/', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse))
                    .ToArray(),
                ct))

                .ToDictionary(e => string.Join('/', e.Node.TrimEnd('/'), e.Id));

            return new MainProcessCreationFeaturePatientSearchResult(
                patientCount,
                patients
                    .Select(p => p.PublicId)
                    .ToArray(),
                entities.Values
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
                    .ToArray(),
                patients
                    .Select(p => new MainProcessCreationFeaturePatient(
                        p.PublicId,
                        p.RowVersionId,
                        entities[ p.EntityId ].PublicId,
                        p.Number,
                        p.TaxNumber,
                        p.Name,
                        p.Birth,
                        p.Death,
                        p.FaxNumber,
                        p.MobileNumber,
                        p.PhoneNumber,
                        p.EmailAddress,
                        external: entity.Nature is not EntityNature.Office && p.EntityId != entity.Id))
                    .ToArray());
        }
    }
}
