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
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                query.UserId,
                ct);

            var entity = await _entities.GetByPublicIdOrThrowExceptionAsync(
                query.EntityId,
                ct);

            await EnsureUserEntityAccessAsync(
                user,
                entity,
                ct);

            if (entity.Nature is not EntityNature.HealthCenter and not EntityNature.Office)
            {
                throw new BusinessException("Patient search can only be performed on health center and office entities.")
                    .WithErrorMessage("A pesquisa de utentes só é permitida em Centros de Saúde ou Lojas do Cidadão.");
            }

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

        private async Task EnsureUserEntityAccessAsync(User user, Entity entity, CancellationToken ct)
        {
            if (!await _userEntities.AnyByUserIdAndEntityIdAsync(user.Id, entity.Id, ct))
            {
                throw new AuthorizationException("User does not have access to this entity.")
                    .WithErrorCode("ILLUEA")
                    .WithErrorMessage("O perfil do utilizador não tem acesso a esta entidade.");
            }
        }
    }
}