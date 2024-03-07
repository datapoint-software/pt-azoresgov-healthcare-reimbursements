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
        
        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly ISequenceRepository _sequences;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCreationCommandHandler(IParameterManager parameterManager, IEntityRepository entities, IPatientEntityRepository patientEntities, IPatientRepository patients, IProcessRepository processes, ISequenceRepository sequences, IUserEntityRepository userEntities, IUserRepository users)
        {
            _parameterManager = parameterManager;
            _entities = entities;
            _patientEntities = patientEntities;
            _patients = patients;
            _processes = processes;
            _sequences = sequences;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCreationResult> HandleCommandAsync(ProcessCreationCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                command.UserId,
                ct);

            var entity = await _entities.GetByPublicIdOrThrowExceptionAsync(
                command.EntityId,
                command.EntityRowVersionId,
                ct);

            if (entity.Nature is not EntityNature.HealthCenter and not EntityNature.Office)
            {
                throw new BusinessException("Processes can not be registered for entities of this nature.")
                    .WithErrorMessage("O registo de processos de reembolso não é permitido para este tipo de entidades.");
            }
            
            if (!await _userEntities.AnyByUserIdAndEntityIdAsync(user.Id, entity.Id, ct))
            {
                throw new BusinessException("User is not linked to this entity.")
                    .WithErrorMessage("O perfil do utilizador não está associado a esta entidade.");
            }

            var patient = await _patients.GetByPublicIdOrThrowExceptionAsync(
                command.PatientId,
                command.PatientRowVersionId,
                ct);

            if (!await _patientEntities.AnyByPatientIdAndEntityIdAsync(patient.Id, entity.Id, ct))
            {
                throw new BusinessException("Patient is not linked to this entity.")
                    .WithErrorMessage("O perfil do utente não está associado a esta entidade.");
            }

            var processExpirationInDays = await _parameterManager.GetProcessExpirationInDaysAsync(
                ct);

            var processNumber = await GetEntityProcessNumberAsync(
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

            return new ProcessCreationResult(
                process.PublicId,
                process.RowVersionId,
                process.Number);
        }

        private async Task<string> GetEntityProcessNumberAsync(ProcessCreationCommand command, Entity entity, CancellationToken ct)
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