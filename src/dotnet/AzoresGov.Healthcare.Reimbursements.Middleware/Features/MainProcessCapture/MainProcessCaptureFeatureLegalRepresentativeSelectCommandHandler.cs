using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSelectCommandHandler : ICommandHandler<MainProcessCaptureFeatureLegalRepresentativeSelectCommand, MainProcessCaptureFeatureLegalRepresentativeSelectResult>
    {
        private readonly IEntityRepository _entities;

        private readonly ILegalRepresentativeRepository _legalRepresentatives;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessCaptureFeatureLegalRepresentativeSelectCommandHandler(IEntityRepository entities, ILegalRepresentativeRepository legalRepresentatives, IPatientRepository patients, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _legalRepresentatives = legalRepresentatives;
            _patients = patients;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessCaptureFeatureLegalRepresentativeSelectResult> HandleCommandAsync(MainProcessCaptureFeatureLegalRepresentativeSelectCommand command, CancellationToken ct)
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

            var legalRepresentative = await _legalRepresentatives.GetByTaxNumberAsync(
                command.TaxNumber,
                ct);

            if (legalRepresentative is null)
                return new MainProcessCaptureFeatureLegalRepresentativeSelectResult(
                    process.RowVersionId,
                    patient.RowVersionId,
                    null);

            process.LegalRepresentative = legalRepresentative;
            patient.LegalRepresentative = legalRepresentative;

            process.RowVersionId = Guid.NewGuid();
            patient.RowVersionId = Guid.NewGuid();

            return new MainProcessCaptureFeatureLegalRepresentativeSelectResult(
                process.RowVersionId,
                patient.RowVersionId,
                new MainProcessCaptureFeatureLegalRepresentative(
                    legalRepresentative.PublicId,
                    legalRepresentative.RowVersionId,
                    legalRepresentative.TaxNumber,
                    legalRepresentative.Name,
                    legalRepresentative.FaxNumber,
                    legalRepresentative.MobileNumber,
                    legalRepresentative.PhoneNumber,
                    legalRepresentative.EmailAddress,
                    legalRepresentative.PostalAddressArea,
                    legalRepresentative.PostalAddressAreaCode,
                    legalRepresentative.PostalAddressLine1,
                    legalRepresentative.PostalAddressLine2,
                    legalRepresentative.PostalAddressLine3));
        }
    }
}
