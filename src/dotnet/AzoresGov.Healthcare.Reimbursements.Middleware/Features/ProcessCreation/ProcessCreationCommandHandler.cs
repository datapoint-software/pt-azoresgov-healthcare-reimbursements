using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Management;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationCommandHandler : ICommandHandler<ProcessCreationCommand, ProcessCreationResult>
    {
        private readonly IParameterManager _parameterManager;
        
        private readonly IEntityRepository _entities;

        private readonly IPatientEntityRepository _patientEntities;

        private readonly IPatientLegalRepresentativeRepository _patientLegalRepresentatives;
        
        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IProcessPatientLegalRepresentativeRepository _processPatientLegalRepresentatives;

        private readonly IProcessPatientRepository _processPatients;

        private readonly IProcessConfigurationRepository _processSettings;

        private readonly ISequenceRepository _sequences;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCreationCommandHandler(IParameterManager parameterManager, IEntityRepository entities, IPatientEntityRepository patientEntities, IPatientLegalRepresentativeRepository patientLegalRepresentatives, IPatientRepository patients, IProcessRepository processes, IProcessPatientLegalRepresentativeRepository processPatientLegalRepresentatives, IProcessPatientRepository processPatients, IProcessConfigurationRepository processSettings, ISequenceRepository sequences, IUserEntityRepository userEntities, IUserRepository users)
        {
            _parameterManager = parameterManager;
            _entities = entities;
            _patientEntities = patientEntities;
            _patientLegalRepresentatives = patientLegalRepresentatives;
            _patients = patients;
            _processes = processes;
            _processPatientLegalRepresentatives = processPatientLegalRepresentatives;
            _processPatients = processPatients;
            _processSettings = processSettings;
            _sequences = sequences;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCreationResult> HandleCommandAsync(ProcessCreationCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                command.UserId,
                ct);

            var entity = await _entities.GetByPublicIdOrThrowBusinessExceptionAsync(
                command.EntityId,
                ct);

            Assert.RowVersion(
                entity.RowVersionId,
                command.EntityRowVersionId);
            
            Assert.EntityNature(
                [ EntityNature.HealthCenter, EntityNature.Office],
                entity.Nature);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                entity.Id,
                ct);

            var patient = await _patients.GetByPublicIdAsync(
                command.PatientId,
                ct);

            Assert.Found(patient);
            
            Assert.RowVersion(
                patient.RowVersionId,
                command.PatientRowVersionId);

            await Assert.PatientEntityAccessAsync(
                _patientEntities,
                patient.Id,
                entity.Id,
                ct);

            var patientLegalRepresentative = await _patientLegalRepresentatives.GetByPatientIdAsync(
                patient.Id,
                ct);

            var processExpirationInDays = await _parameterManager.GetProcessExpirationInDaysAsync(
                ct);

            var processNumber = await GenerateProcessNumberAsync(
                command,
                entity,
                ct);

            var process = _processes.Add(new Process()
            {
                PublicId = Guid.NewGuid(),
                RowVersionId = Guid.NewGuid(),
                Entity = entity,
                Patient = patient,
                User = user,
                Number = processNumber,
                Status = ProcessStatus.Capture,
                Creation = command.Creation,
                Expiration = command.Creation.AddDays(processExpirationInDays).UtcDateTime,
                Touch = command.Creation
            });

            CreateProcessConfiguration(process);

            CreateProcessPatient(process, patient);

            if (patientLegalRepresentative is not null)
                CreateProcessPatientLegalRepresentative(process, patientLegalRepresentative);

            return new ProcessCreationResult(
                process.PublicId,
                process.RowVersionId,
                process.Number);
        }

        private ProcessConfiguration CreateProcessConfiguration(Process process) =>

             _processSettings.Add(new ProcessConfiguration()
             {
                 Process = process,
                 DocumentIssueDateBypassEnabled = false,
                 MachadoJosephEnabled = false,
                 ReimbursementLimitBypassEnabled = false,
                 UnemploymentEnabled = false
             });

        private void CreateProcessPatient(Process process, Patient patient) => 
            
            _processPatients.Add(new ProcessPatient()
            {
                Process = process,
                Name = patient.Name,
                Birth = patient.Birth,
                Gender = patient.Gender,
                HealthNumber = patient.HealthNumber,
                TaxNumber = patient.TaxNumber,
                AddressLine1 = patient.AddressLine1,
                AddressLine2 = patient.AddressLine2,
                AddressLine3 = patient.AddressLine3,
                PostalCode = patient.PostalCode,
                PostalCodeArea = patient.PostalCodeArea,
                EmailAddress = patient.EmailAddress,
                FaxNumber = patient.FaxNumber,
                MobileNumber = patient.MobileNumber,
                PhoneNumber = patient.PhoneNumber,
                Death = patient.Death
            });

        private void CreateProcessPatientLegalRepresentative(Process process, PatientLegalRepresentative patientLegalRepresentative) =>

            _processPatientLegalRepresentatives.Add(new ProcessPatientLegalRepresentative()
            {
                Process = process,
                Name = patientLegalRepresentative.Name,
                TaxNumber = patientLegalRepresentative.TaxNumber,
                EmailAddress = patientLegalRepresentative.EmailAddress,
                FaxNumber = patientLegalRepresentative.FaxNumber,
                MobileNumber = patientLegalRepresentative.MobileNumber,
                PhoneNumber = patientLegalRepresentative.PhoneNumber
            });

        private async Task<string> GenerateProcessNumberAsync(ProcessCreationCommand command, Entity entity, CancellationToken ct)
        {
            var processNumberSequenceNameTokens = await _entities.GetAllParentEntityCodeByEntityIdAsync(
                entity.Id, 
                ct);

            var processNumberStringBuilder = new StringBuilder();
            
            processNumberStringBuilder.Append(command.Creation.Year);

            foreach (var token in processNumberSequenceNameTokens)
            {
                processNumberStringBuilder
                    .Append('/')
                    .Append(token.ToUpper());
            }
            
            processNumberStringBuilder
                .Append('/')
                .Append(entity.Code.ToUpper());

            var processNumberSequenceName = processNumberStringBuilder.ToString();

            var processNumberSequence = await GetOrCreateSequenceAsync(
                processNumberSequenceName,
                ct);

            processNumberStringBuilder
                .Append('/')
                .Append(processNumberSequence.NextValue);

            ++processNumberSequence.NextValue;

            return processNumberStringBuilder.ToString();
        }

        private async Task<Sequence> GetOrCreateSequenceAsync(string name, CancellationToken ct)
        {
            var sequence = await _sequences.GetByNameAsync(
                name, 
                ct);

            sequence ??= _sequences.Add(new Sequence()
            {
                Name = name,
                NextValue = 1
            });

            return sequence;
        }
    }
}