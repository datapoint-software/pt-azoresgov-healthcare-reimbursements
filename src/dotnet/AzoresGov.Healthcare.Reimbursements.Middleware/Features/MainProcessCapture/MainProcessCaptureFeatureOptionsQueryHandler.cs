using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureOptionsQueryHandler : IQueryHandler<MainProcessCaptureFeatureOptionsQuery, MainProcessCaptureFeatureOptionsResult>
    {
        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IUserEntityRepository _userEntities;

        private readonly IUserRepository _users;

        public MainProcessCaptureFeatureOptionsQueryHandler(IEntityRepository entities, IPatientRepository patients, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _entities = entities;
            _patients = patients;
            _processes = processes;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<MainProcessCaptureFeatureOptionsResult> HandleQueryAsync(MainProcessCaptureFeatureOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var process = await _processes.GetByPublicIdAsync(
                query.ProcessId,
                ct);

            Assert.Found(process);

            var processEntity = await _entities.GetByIdAsync(
                process.EntityId,
                ct);

            Assert.Found(processEntity);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user,
                processEntity,
                ct);

            var patient = await _patients.GetByIdAsync(
                process.PatientId,
                ct);

            Assert.Found(patient);

            var patientEntity = await _entities.GetByIdAsync(
                patient.EntityId,
                ct);

            Assert.Found(patientEntity);

            var parentEntities = (await _entities.GetAllByIdAsync(
                processEntity.Node
                    .Split('/', StringSplitOptions.RemoveEmptyEntries)
                    .Union(patientEntity.Node
                        .Split('/', StringSplitOptions.RemoveEmptyEntries))
                    .Select(long.Parse)
                    .Distinct()
                    .ToArray(),
                ct))
                
                .ToDictionary(e => string.Join('/', e.Node, e.Id));

            var entities = parentEntities.Values
                .ToDictionary(e => e.Id);

            return new MainProcessCaptureFeatureOptionsResult(
                parentEntities.Values.Union([patientEntity, processEntity])
                    .Select(e => new MainProcessCaptureFeatureEntity(
                        e.PublicId,
                        e.RowVersionId,
                        parentEntities.TryGetValue(e.Node, out var parentEntity)
                            ? parentEntity.PublicId
                            : null,
                        e.Code,
                        e.Name,
                        e.Nature))
                    .ToArray(),
                new MainProcessCaptureFeaturePatient(
                    patient.PublicId,
                    patient.RowVersionId,
                    patientEntity.PublicId,
                    patient.Number,
                    patient.TaxNumber,
                    patient.Name,
                    patient.Birth,
                    patient.Death,
                    patient.FaxNumber,
                    patient.MobileNumber,
                    patient.PhoneNumber,
                    patient.EmailAddress,
                    patient.PostalAddressArea,
                    patient.PostalAddressAreaCode,
                    patient.PostalAddressLine1,
                    patient.PostalAddressLine2,
                    patient.PostalAddressLine3),
                new MainProcessCaptureFeatureProcess(
                    process.PublicId,
                    process.RowVersionId,
                    processEntity.PublicId,
                    process.Number,
                    process.Creation));
        }
    }
}
