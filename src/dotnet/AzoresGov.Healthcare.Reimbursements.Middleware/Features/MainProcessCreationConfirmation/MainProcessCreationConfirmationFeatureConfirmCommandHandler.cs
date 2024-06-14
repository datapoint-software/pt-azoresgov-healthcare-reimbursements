using AzoresGov.Healthcare.Reimbursements.Management;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationConfirmation
{
    public sealed class MainProcessCreationConfirmationFeatureConfirmCommandHandler : ICommandHandler<MainProcessCreationConfirmationFeatureConfirmCommand, MainProcessCreationConfirmationFeatureConfirmResult>
    {
        private readonly IEntityManager _entityManager;

        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;

        private readonly IPatientEntityRepository _patientEntities;

        private readonly IProcessRepository _processes;

        private readonly ISequenceRepository _sequences;

        private readonly IUserRepository _users;

        private readonly IUserEntityRepository _userEntities;

        public MainProcessCreationConfirmationFeatureConfirmCommandHandler(
            IEntityManager entityManager, 
            IEntityRepository entities, 
            IPatientRepository patients, 
            IPatientEntityRepository patientEntities, 
            IProcessRepository processes, 
            ISequenceRepository sequences, 
            IUserRepository users, 
            IUserEntityRepository userEntities)
        {
            _entityManager = entityManager;
            _entities = entities;
            _patients = patients;
            _patientEntities = patientEntities;
            _processes = processes;
            _sequences = sequences;
            _users = users;
            _userEntities = userEntities;
        }

        public async Task<MainProcessCreationConfirmationFeatureConfirmResult> HandleCommandAsync(MainProcessCreationConfirmationFeatureConfirmCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                command.UserId, 
                ct);

            Assert.Found(user);

            var entity = await _entities.GetByPublicIdAsync(
                command.EntityId, 
                ct);

            Assert.Found(entity, command.EntityRowVersionId);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user,
                entity,
                ct);

            var patient = await _patients.GetByPublicIdAsync(
                command.PatientId,
                ct);

            Assert.Found(patient, command.PatientRowVersionId);

            await Assert.PatientEntityAccessAsync(
                _patientEntities,
                patient,
                entity,
                ct);

            var sequenceName = await _entityManager.GetProcessNumberSequenceNameAsync(
                entity,
                command.Creation.LocalDateTime.Year,
                ct);

            var sequence = await _sequences.GetByNameAsync(
                sequenceName,
                ct);

            if (sequence is null)
            {
                sequence = _sequences.Add(new()
                {
                    Name = sequenceName,
                    NextValue = 1
                });
            }

            var process = _processes.Add(new()
            {
                PublicId = Guid.NewGuid(),
                RowVersionId = Guid.NewGuid(),
                Entity = entity,
                Patient = patient,
                Number = $"{sequenceName}/{sequence.NextValue}",
                Status = Enumerations.ProcessStatus.Registration,
                Creation = command.Creation
            });

            sequence.NextValue += 1;

            return new MainProcessCreationConfirmationFeatureConfirmResult(
                process.PublicId,
                process.RowVersionId);
        }
    }
}
