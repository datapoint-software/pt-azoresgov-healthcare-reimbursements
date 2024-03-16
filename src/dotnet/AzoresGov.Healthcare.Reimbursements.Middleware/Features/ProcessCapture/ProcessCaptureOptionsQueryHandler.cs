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

        private readonly IPatientLegalRepresentativeRepository _patientLegalRepresentatives;
        
        private readonly IProcessRepository _processes;

        private readonly IProcessPatientLegalRepresentativeRepository _processPatientLegalRepresentatives;

        private readonly IProcessPatientRepository _processPatients;

        private readonly IProcessConfigurationRepository _processSettings;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureOptionsQueryHandler(IEntityRepository entities, IPatientRepository patients, IPatientLegalRepresentativeRepository patientLegalRepresentatives, IProcessRepository processes, IProcessPatientLegalRepresentativeRepository processPatientLegalRepresentatives, IProcessPatientRepository processPatients, IProcessConfigurationRepository processSettings, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _patientLegalRepresentatives = patientLegalRepresentatives;
            _processes = processes;
            _processPatientLegalRepresentatives = processPatientLegalRepresentatives;
            _processPatients = processPatients;
            _processSettings = processSettings;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureOptionsResult> HandleQueryAsync(ProcessCaptureOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                query.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowBusinessExceptionAsync(
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

            var patientLegalRepresentative = await _patientLegalRepresentatives.GetByPatientIdAsync(
                patient.Id,
                ct);

            var processPatient = await _processPatients.GetByProcessIdAsync(
                process.Id,
                ct);

            var processPatientLegalRepresentative = await _processPatientLegalRepresentatives.GetByProcessIdAsync(
                process.Id,
                ct);

            return new ProcessCaptureOptionsResult(
                configuration is null ? null :
                    new ProcessCaptureOptionsConfigurationResult(
                        configuration.RowVersionId,
                        configuration.MachadoJosephEnabled,
                        configuration.DocumentIssueDateBypassEnabled,
                        configuration.ReimbursementLimitBypassEnabled,
                        configuration.UnemploymentEnabled),
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
                    processPatient?.RowVersionId,
                    processPatient?.Name ?? patient.Name,
                    processPatient?.Birth ?? patient.Birth,
                    processPatient?.Gender ?? patient.Gender,
                    processPatient?.HealthNumber ?? patient.HealthNumber,
                    processPatient?.TaxNumber ?? patient.TaxNumber,
                    processPatient?.AddressLine1 ?? patient.AddressLine1,
                    processPatient?.AddressLine2 ?? patient.AddressLine2,
                    processPatient?.AddressLine3 ?? patient.AddressLine3,
                    processPatient?.PostalCode ?? patient.PostalCode,
                    processPatient?.PostalCodeArea ?? patient.PostalCodeArea,
                    processPatient?.EmailAddress ?? patient.EmailAddress,
                    processPatient?.FaxNumber ?? patient.FaxNumber,
                    processPatient?.MobileNumber ?? patient.MobileNumber,
                    processPatient?.PhoneNumber ?? patient.PhoneNumber,
                    processPatient?.Death ?? patient.Death),
                processPatientLegalRepresentative is not null ? 
                    new ProcessCaptureOptionsPatientLegalRepresentativeResult(
                        processPatientLegalRepresentative.RowVersionId,
                        processPatientLegalRepresentative.Name,
                        processPatientLegalRepresentative.TaxNumber,
                        processPatientLegalRepresentative.EmailAddress,
                        processPatientLegalRepresentative.FaxNumber,
                        processPatientLegalRepresentative.MobileNumber,
                        processPatientLegalRepresentative.PhoneNumber) :
                    patientLegalRepresentative is not null ?
                        new ProcessCaptureOptionsPatientLegalRepresentativeResult(
                            null,
                            patientLegalRepresentative.Name,
                            patientLegalRepresentative.TaxNumber,
                            patientLegalRepresentative.EmailAddress,
                            patientLegalRepresentative.FaxNumber,
                            patientLegalRepresentative.MobileNumber,
                            patientLegalRepresentative.PhoneNumber) :
                        null,
                new ProcessCaptureOptionsProcessResult(
                    process.PublicId,
                    process.RowVersionId,
                    process.Number,
                    process.Status));
        }
    }
}