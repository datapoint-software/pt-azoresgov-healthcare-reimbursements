using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Entities;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCapturePatientCommandHandler : ICommandHandler<ProcessCapturePatientCommand, ProcessCapturePatientResult>
    {
        private readonly IPatientRepository _patients;
        
        private readonly IProcessRepository _processes;

        private readonly IProcessPatientRepository _processPatients;
        
        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCapturePatientCommandHandler(IPatientRepository patients, IProcessRepository processes, IProcessPatientRepository processPatients, IUserEntityRepository userEntities, IUserRepository users)
        {
            _patients = patients;
            _processes = processes;
            _processPatients = processPatients;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCapturePatientResult> HandleCommandAsync(ProcessCapturePatientCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowBusinessExceptionAsync(
                command.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowBusinessExceptionAsync(
                command.ProcessId,
                ct);

            Assert.ProcessStatus(
                ProcessStatus.Capture,
                process.Status);
            
            Assert.RowVersion(
                process.RowVersionId,
                command.ProcessRowVersionId);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var patient = await _patients.GetByIdOrThrowExceptionAsync(
                process.PatientId,
                ct);

            var processPatient = await GetOrCreateProcessPatientAsync(
                process,
                command.ProcessPatientRowVersionId,
                ct);

            processPatient.Name = patient.Name;
            processPatient.Birth = patient.Birth;
            processPatient.Death = patient.Death;
            processPatient.Gender = patient.Gender;
            processPatient.HealthNumber = patient.HealthNumber;
            processPatient.TaxNumber = patient.TaxNumber;
            
            processPatient.AddressLine1 = command.AddressLine1;
            processPatient.AddressLine2 = command.AddressLine2;
            processPatient.AddressLine3 = command.AddressLine3;
            processPatient.PostalCode = command.PostalCode;
            processPatient.PostalCodeArea = command.PostalCodeArea;
            processPatient.EmailAddress = command.EmailAddress;
            processPatient.FaxNumber = command.FaxNumber;
            processPatient.MobileNumber = command.MobileNumber;
            processPatient.PhoneNumber = command.PhoneNumber;
            
            process.Touch = command.Creation;

            process.RowVersionId = Guid.NewGuid();
            processPatient.RowVersionId = Guid.NewGuid();

            return new ProcessCapturePatientResult(
                process.RowVersionId,
                processPatient.RowVersionId);
        }

        private async Task<ProcessPatient> GetOrCreateProcessPatientAsync(
            Process process,
            Guid? processPatientRowVersionId,
            CancellationToken ct)
        {
            var processPatient = await _processPatients.GetByProcessIdAsync(
                process.Id,
                ct);

            if (processPatient is null)
            {
                return _processPatients.Add(new ProcessPatient()
                {
                    Process = process
                });
            }
            
            Assert.RowVersion(
                processPatient.RowVersionId, 
                processPatientRowVersionId);

            return processPatient;
        }
    }
}