using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessPatientCapture
{
    public sealed class ProcessPatientCaptureOptionsQueryHandler : IQueryHandler<ProcessPatientCaptureOptionsQuery, ProcessPatientCaptureOptionsResult>
    {
        private readonly IEntityRepository _entities;
        
        private readonly IPatientRepository _patients;
        
        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessPatientCaptureOptionsQueryHandler(IEntityRepository entities, IPatientRepository patients, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessPatientCaptureOptionsResult> HandleQueryAsync(ProcessPatientCaptureOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                query.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowExceptionAsync(
                query.ProcessId,
                ct);

            if (!await _userEntities.AnyByUserIdAndEntityIdAsync(user.Id, process.EntityId, ct))
            {
                throw new BusinessException("User does not have write access to this process.")
                    .WithErrorMessage("O perfil do utilizador não tem acesso de escrita a este processo.");
            }

            var entity = await _entities.GetByIdOrThrowExceptionAsync(
                process.EntityId,
                ct);

            var parentEntity = await _entities.GetParentEntityByEntityIdAsync(
                process.EntityId,
                0,
                ct);

            var patient = await _patients.GetByIdOrThrowExceptionAsync(
                process.PatientId,
                ct);

            return new ProcessPatientCaptureOptionsResult(
                new ProcessPatientCaptureOptionsEntityResult(
                    entity.PublicId,
                    entity.RowVersionId,
                    entity.Code,
                    entity.Name,
                    entity.Nature),
                parentEntity is null
                    ? null
                    : new ProcessPatientCaptureOptionsEntityResult(
                        parentEntity.PublicId,
                        parentEntity.RowVersionId,
                        parentEntity.Code,
                        parentEntity.Name,
                        parentEntity.Nature),
                new ProcessPatientCaptureOptionsPatientResult(
                    patient.PublicId,
                    patient.RowVersionId,
                    patient.Name,
                    patient.Birth,
                    patient.Gender,
                    patient.HealthNumber,
                    patient.TaxNumber,
                    patient.AddressLine1,
                    patient.AddressLine2,
                    patient.AddressLine3,
                    patient.PostalCode,
                    patient.PostalCodeArea,
                    patient.EmailAddress,
                    patient.FaxNumber,
                    patient.MobileNumber,
                    patient.PhoneNumber,
                    patient.Death),
                new ProcessPatientCaptureOptionsProcessResult(
                    process.PublicId,
                    process.RowVersionId,
                    process.Number,
                    process.Status));
        }
    }
}