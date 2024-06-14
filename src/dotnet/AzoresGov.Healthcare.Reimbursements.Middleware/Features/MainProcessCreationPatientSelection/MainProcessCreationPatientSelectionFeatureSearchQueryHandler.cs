using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            // We know for a fact all patient numbers are a numeric
            // string with 9 characters of length and if the filter does not
            // match that expectation we can safely skip the database queries.
            if (query.Mode is MainProcessCreationPatientSelectionFeatureSearchMode.PatientNumber)
            {
                if (query.Filter.Length != 9 || !long.TryParse(query.Filter, out var _))
                {
                    return new MainProcessCreationPatientSelectionFeatureSearchResult(
                        0,
                        Array.Empty<Guid>(),
                        Array.Empty<MainProcessCreationPatientSelectionFeatureSearchResultPatient>());
                }
            }

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

            var patternExpression = query.Mode is MainProcessCreationPatientSelectionFeatureSearchMode.Full
                ? RegexHelper.CreateFromLikePatternExpression(StringHelper.CreateLikePatternExpression(query.Filter))
                : null;

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
                        taxNumber: IfFullMatch(query.Mode, patternExpression, p.TaxNumber),
                        name: p.Name,
                        birth: IfFull(query.Mode, p.Birth),
                        death: p.Death,
                        external: patientEntities.Any(g => g.Key == p.Id) is false,
                        faxNumber: IfFullMatch(query.Mode, patternExpression, p.FaxNumber),
                        mobileNumber: IfFullMatch(query.Mode, patternExpression, p.MobileNumber),
                        phoneNumber: IfFullMatch(query.Mode, patternExpression, p.PhoneNumber),
                        emailAddress: IfFullMatch(query.Mode, patternExpression, p.EmailAddress),
                        postalAddressArea: IfFullMatch(query.Mode, patternExpression, p.PostalAddressArea),
                        postalAddressAreaCode: IfFullMatch(query.Mode, patternExpression, p.PostalAddressAreaCode),
                        postalAddressLine1: IfFullMatch(query.Mode, patternExpression, p.PostalAddressLine1),
                        postalAddressLine2: IfFullMatch(query.Mode, patternExpression, p.PostalAddressLine2),
                        postalAddressLine3: IfFullMatch(query.Mode, patternExpression, p.PostalAddressLine3)))
                    .ToArray());
        }

        private static TProperty? IfFull<TProperty>(MainProcessCreationPatientSelectionFeatureSearchMode mode, TProperty? value)
        {
            if (mode is not MainProcessCreationPatientSelectionFeatureSearchMode.Full)
                return default;

            return value;
        }

        private static string? IfFullMatch(MainProcessCreationPatientSelectionFeatureSearchMode mode, Regex? patternExpression, string? value)
        {
            if (mode is not MainProcessCreationPatientSelectionFeatureSearchMode.Full)
                return default;

            if (string.IsNullOrEmpty(value))
                return default;

            if (patternExpression is null)
                return default;

            if (!patternExpression.IsMatch(StringHelper.CreateLikePatternExpression(value)))
                return default;

            return value;
        }
    }
}
