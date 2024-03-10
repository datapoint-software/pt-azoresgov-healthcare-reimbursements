using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.Middleware.Helpers;
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
        private readonly IProcessRepository _processes;

        private readonly IProcessPatientRepository _processPatients;
        
        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCapturePatientCommandHandler(IProcessRepository processes, IProcessPatientRepository processPatients, IUserEntityRepository userEntities, IUserRepository users)
        {
            _processes = processes;
            _processPatients = processPatients;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCapturePatientResult> HandleCommandAsync(ProcessCapturePatientCommand command, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdOrThrowExceptionAsync(
                command.UserId,
                ct);

            var process = await _processes.GetByPublicIdOrThrowExceptionAsync(
                command.ProcessId, 
                command.ProcessRowVersionId,
                ct);

            Assert.ProcessStatus(
                ProcessStatus.Capture,
                process.Status);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var processPatient = await _processPatients.GetByProcessIdOrThrowExceptionAsync(
                process.Id,
                command.PatientRowVersionId,
                ct);

            processPatient.AddressLine1 = command.AddressLine1;
            processPatient.AddressLine2 = command.AddressLine2;
            processPatient.AddressLine3 = command.AddressLine3;
            processPatient.PostalCode = command.PostalCode;
            processPatient.PostalCodeArea = command.PostalCodeArea;
            processPatient.EmailAddress = command.EmailAddress;
            processPatient.FaxNumber = command.FaxNumber;
            processPatient.MobileNumber = command.MobileNumber;
            processPatient.PhoneNumber = command.PhoneNumber;

            processPatient.RowVersionId = Guid.NewGuid();
            
            process.RowVersionId = Guid.NewGuid();
            process.Touch = command.Creation;

            return new ProcessCapturePatientResult(
                process.RowVersionId,
                processPatient.RowVersionId);
        }
    }
}