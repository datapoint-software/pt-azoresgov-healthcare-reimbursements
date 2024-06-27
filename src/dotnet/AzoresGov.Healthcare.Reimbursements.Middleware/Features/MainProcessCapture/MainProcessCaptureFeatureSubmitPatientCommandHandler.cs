using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureSubmitPatientCommandHandler : ICommandHandler<MainProcessCaptureFeatureSubmitPatientCommand, MainProcessCaptureFeatureSubmitPatientResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessCaptureFeatureSubmitPatientCommandHandler(IEntityRepository entities, IPatientRepository patients, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessCaptureFeatureSubmitPatientResult> HandleCommandAsync(MainProcessCaptureFeatureSubmitPatientCommand command, CancellationToken ct)
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

            var patient = await _patients.GetByPublicIdAsync(
                command.PatientId,
                ct);

            Assert.Found(patient, command.PatientRowVersionId);

            patient.FaxNumber = command.FaxNumber;
            patient.MobileNumber = command.MobileNumber;
            patient.PhoneNumber = command.PhoneNumber;
            patient.EmailAddress = command.EmailAddress;
            patient.PostalAddressArea = command.PostalAddressArea;
            patient.PostalAddressAreaCode = command.PostalAddressAreaCode;
            patient.PostalAddressLine1 = command.PostalAddressLine1;
            patient.PostalAddressLine2 = command.PostalAddressLine2;
            patient.PostalAddressLine3 = command.PostalAddressLine3;

            patient.RowVersionId = Guid.NewGuid();
            process.RowVersionId = Guid.NewGuid();

            return new MainProcessCaptureFeatureSubmitPatientResult(
                process.RowVersionId,
                patient.RowVersionId);
        }
    }
}
