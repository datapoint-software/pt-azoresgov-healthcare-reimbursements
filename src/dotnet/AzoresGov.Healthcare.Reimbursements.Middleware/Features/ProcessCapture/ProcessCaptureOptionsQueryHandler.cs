using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsQueryHandler : IQueryHandler<ProcessCaptureOptionsQuery, ProcessCaptureOptionsResult>
    {
        private readonly IEntityRepository _entities;
        
        private readonly IPatientRepository _patients;
        
        private readonly IProcessRepository _processes;

        private readonly IProcessConfigurationRepository _processSettings;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureOptionsQueryHandler(IEntityRepository entities, IPatientRepository patients, IProcessRepository processes, IProcessConfigurationRepository processSettings, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _processes = processes;
            _processSettings = processSettings;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureOptionsResult> HandleQueryAsync(ProcessCaptureOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                query.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowExceptionAsync(
                query.ProcessId,
                ct);

            Assert.ProcessStatus(
                ProcessStatus.Capture,
                process.Status);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var configuration = await _processSettings.GetByProcessIdAsync(
                process.Id,
                ct);

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

            return new ProcessCaptureOptionsResult(
                configuration is null ? null :
                    new ProcessCaptureOptionsConfigurationResult(
                        configuration.MachadoJosephEnabled,
                        configuration.DocumentIssueDateBypassEnabled,
                        configuration.ReimbursementLimitBypassEnabled),
                new ProcessCaptureOptionsEntityResult(
                    entity.PublicId,
                    entity.RowVersionId,
                    entity.Code,
                    entity.Name,
                    entity.Nature),
                parentEntity is null
                    ? null
                    : new ProcessCaptureOptionsEntityResult(
                        parentEntity.PublicId,
                        parentEntity.RowVersionId,
                        parentEntity.Code,
                        parentEntity.Name,
                        parentEntity.Nature),
                new ProcessCaptureOptionsPatientResult(
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
                new ProcessCaptureOptionsProcessResult(
                    process.PublicId,
                    process.RowVersionId,
                    process.Number,
                    process.Status));
        }
    }
}