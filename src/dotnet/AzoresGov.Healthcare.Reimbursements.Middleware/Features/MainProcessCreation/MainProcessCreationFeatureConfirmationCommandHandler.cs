using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureConfirmationCommandHandler : ICommandHandler<MainProcessCreationFeatureConfirmationCommand, MainProcessCreationFeatureConfirmationResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly ISequenceRepository _sequences;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessCreationFeatureConfirmationCommandHandler(IEntityRepository entities, IPatientRepository patients, IProcessRepository processes, ISequenceRepository sequences, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _processes = processes;
            _sequences = sequences;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessCreationFeatureConfirmationResult> HandleCommandAsync(MainProcessCreationFeatureConfirmationCommand command, CancellationToken ct)
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

            // Ensure entity match for non-office process registrations.
            if (entity.Nature is not Enumerations.EntityNature.Office &&  entity.Id != patient.EntityId)
            {
                throw new BusinessException("User entity is not an office and differs from the patient entity.")
                    .WithErrorMessage("A entidade do utente não corresponde à entidade do utilizador.");
            }

            // Generate the process number sequence name.
            var parentEntityIds = entity.Node
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();

            var parentEntities = (await _entities.GetAllByIdAsync(
                entity.Node
                    .Split('/', StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToArray(),
                ct))
                
                .ToDictionary(e => e.Id);

            var processNumberSequenceNameBuilder = new StringBuilder()
                .Append(command.Creation.LocalDateTime.Year)
                .Append('/');

            foreach (var entityId in parentEntityIds)
            {
                processNumberSequenceNameBuilder.Append(parentEntities[entityId].Code.ToUpper());
                processNumberSequenceNameBuilder.Append('/');
            }

            processNumberSequenceNameBuilder.Append(entity.Code.ToUpper());

            var processNumberSequenceName = processNumberSequenceNameBuilder.ToString();

            // Get or create the process number sequence.
            var processNumberSequence = await _sequences.GetByNameAsync(
                processNumberSequenceName,
                ct);

            processNumberSequence ??= _sequences.Add(new Sequence()
            {
                Name = processNumberSequenceName,
                NextValue = 1
            });

            // Create the actual process.
            var process = _processes.Add(new Process()
            {
                PublicId = Guid.NewGuid(),
                RowVersionId = Guid.NewGuid(),
                Entity = entity,
                Patient = patient,
                Number = string.Join('/', processNumberSequence.Name, processNumberSequence.NextValue),
                Status = ProcessStatus.Capture,
                Creation = command.Creation.UtcDateTime
            });

            processNumberSequence.NextValue += 1;

            return new MainProcessCreationFeatureConfirmationResult(
                new MainProcessCreationFeatureProcess(
                    process.PublicId,
                    process.RowVersionId,
                    process.Number,
                    process.Status,
                    process.Creation));
            
        }
    }
}
