using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeRemoveCommandHandler : ICommandHandler<MainProcessCaptureFeatureLegalRepresentativeRemoveCommand, MainProcessCaptureFeatureLegalRepresentativeRemoveResult>
    {
        private readonly IEntityRepository _entities;

        private readonly ILegalRepresentativeRepository _legalRepresentatives;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessCaptureFeatureLegalRepresentativeRemoveCommandHandler(IEntityRepository entities, ILegalRepresentativeRepository legalRepresentatives, IPatientRepository patients, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _legalRepresentatives = legalRepresentatives;
            _patients = patients;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessCaptureFeatureLegalRepresentativeRemoveResult> HandleCommandAsync(MainProcessCaptureFeatureLegalRepresentativeRemoveCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                command.UserId,
                ct);

            Assert.Found(user);

            var process = await _processes.GetByPublicIdAsync(
                command.ProcessId,
                ct);

            Assert.Found(process, command.ProcessRowVersionId);

            var entity = await _entities.GetByIdAsync(
                process.EntityId,
                ct);

            Assert.Found(entity);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user,
                entity,
                ct);

            var patient = await _patients.GetByIdAsync(
                process.PatientId,
                ct);

            Assert.Found(patient, command.PatientRowVersionId);

            var legalRepresentative = process.LegalRepresentativeId.HasValue
                ? (await _legalRepresentatives.GetByIdAsync(process.LegalRepresentativeId.Value, ct))
                : null;

            Assert.Found(legalRepresentative, command.LegalRepresentativeRowVersionId);

            patient.LegalRepresentative = null;
            process.LegalRepresentative = null;

            patient.RowVersionId = Guid.NewGuid();
            process.RowVersionId = Guid.NewGuid();

            return new MainProcessCaptureFeatureLegalRepresentativeRemoveResult(
                process.RowVersionId,
                patient.RowVersionId);
        }
    }
}
