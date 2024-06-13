using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchQueryHandler : IQueryHandler<MainProcessCreationPatientSelectionFeatureSearchQuery, MainProcessCreationPatientSelectionFeatureSearchResult>
    {
        private const string PatientSearchQueryModeOutOfBoundsExceptionMessage = "Patient search query mode is not supported.";

        private readonly IEntityRepository _entities;

        private readonly IPatientEntityRepository _patientEntities;

        private readonly IPatientRepository _patients;

        private readonly IUserRepository _users;

        private readonly IUserEntityRepository _userEntities;

        public MainProcessCreationPatientSelectionFeatureSearchQueryHandler(IEntityRepository entities, IPatientEntityRepository patientEntities, IPatientRepository patients, IUserRepository users, IUserEntityRepository userEntities)
        {
            _entities = entities;
            _patientEntities = patientEntities;
            _patients = patients;
            _users = users;
            _userEntities = userEntities;
        }

        public async Task<MainProcessCreationPatientSelectionFeatureSearchResult> HandleQueryAsync(MainProcessCreationPatientSelectionFeatureSearchQuery query, CancellationToken ct)
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

            var skip = query.Skip ?? 0;
            var take = query.Take ?? 25;

            var patients = await (query.Mode switch
            {
                MainProcessCreationPatientSelectionFeatureSearchMode.Full => _patients.GetAllByFullSearchCriteriaAsync(query.Filter, skip, take, ct),
                MainProcessCreationPatientSelectionFeatureSearchMode.PatientNumber => _patients.GetAllByPatientNumberSearchCriteriaAsync(query.Filter, skip, take, ct),
                _ => throw new InvalidOperationException(PatientSearchQueryModeOutOfBoundsExceptionMessage)
            });

            var patientCount = await (query.Mode switch
            {
                MainProcessCreationPatientSelectionFeatureSearchMode.Full => _patients.GetCountByFullSearchCriteriaAsync(query.Filter, ct),
                MainProcessCreationPatientSelectionFeatureSearchMode.PatientNumber => _patients.GetCountByPatientNumberSearchCriteriaAsync(query.Filter, ct),
                _ => throw new InvalidOperationException(PatientSearchQueryModeOutOfBoundsExceptionMessage)
            });

            var patientEntities = (
                await _patientEntities.GetAllByPatientIdAndEntityIdAsync(
                    patients.Select(p => p.Id).ToArray(),
                    [ entity.Id ],
                    ct))
                .GroupBy(e => e.PatientId);

            return new MainProcessCreationPatientSelectionFeatureSearchResult(
                patientCount,
                patients
                    .Select(p => p.PublicId)
                    .ToArray(),
                patients
                    .Select(p => new MainProcessCreationPatientSelectionFeatureSearchResultPatient(
                        id: p.PublicId,
                        rowVersionId: p.RowVersionId,
                        number: p.Number,
                        taxNumber: IfFull(query.Mode, p.TaxNumber),
                        name: p.Name,
                        birth: IfFull(query.Mode, p.Birth),
                        death: p.Death,
                        external: patientEntities.Any(g => g.Key == p.Id) is false,
                        faxNumber: IfFull(query.Mode, p.FaxNumber),
                        mobileNumber: IfFull(query.Mode, p.MobileNumber),
                        phoneNumber: IfFull(query.Mode, p.PhoneNumber),
                        emailAddress: IfFull(query.Mode, p.EmailAddress),
                        postalAddressArea: IfFull(query.Mode, p.PostalAddressArea),
                        postalAddressAreaCode: IfFull(query.Mode, p.PostalAddressAreaCode),
                        postalAddressLine1: IfFull(query.Mode, p.PostalAddressLine1),
                        postalAddressLine2: IfFull(query.Mode, p.PostalAddressLine2),
                        postalAddressLine3: IfFull(query.Mode, p.PostalAddressLine3)))
                    .ToArray());
        }

        private static TProperty? IfFull<TProperty>(MainProcessCreationPatientSelectionFeatureSearchMode mode, TProperty? value)
        {
            if (mode is not MainProcessCreationPatientSelectionFeatureSearchMode.Full)
                return default;

            return value;
        }
    }
}
