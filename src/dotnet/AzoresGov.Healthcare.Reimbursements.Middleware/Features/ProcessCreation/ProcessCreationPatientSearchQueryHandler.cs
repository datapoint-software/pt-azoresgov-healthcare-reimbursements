using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationPatientSearchQueryHandler : IQueryHandler<ProcessCreationPatientSearchQuery, ProcessCreationPatientSearchResult>
    {
        private readonly IEntityRepository _entities;
        
        private readonly IPatientRepository _patients;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public ProcessCreationPatientSearchQueryHandler(IEntityRepository entities, IPatientRepository patients, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCreationPatientSearchResult> HandleQueryAsync(ProcessCreationPatientSearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            var entity = await _entities.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.EntityId,
                ct);

            Assert.EntityNature(
                [ EntityNature.HealthCenter, EntityNature.Office ],
                entity.Nature);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                entity.Id,
                ct);

            var patients = await _patients.GetAllByEntitySearchCriteriaAsync(
                entity.Id,
                query.Filter,
                query.Skip ?? 0,
                query.Take ?? 5,
                ct);

            var patientCount = await _patients.CountByEntitySearchCriteriaAsync(
                entity.Id,
                query.Filter,
                ct);

            return new ProcessCreationPatientSearchResult(
                patients.Select(e => e.PublicId).ToArray(),
                patients
                    .Select(e => new ProcessCreationPatientResult(
                        e.PublicId,
                        e.RowVersionId,
                        e.Name,
                        e.Birth,
                        e.Gender,
                        e.HealthNumber,
                        e.TaxNumber,
                        e.FaxNumber,
                        e.MobileNumber,
                        e.PhoneNumber,
                        e.Death))
                    .ToArray(),
                patientCount);
        }
    }
}