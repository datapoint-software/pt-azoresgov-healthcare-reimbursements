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
        private readonly IPatientRepository _patients;
        
        private readonly IProcessEntityRepository _processEntities;

        private readonly IProcessRepository _processes;
        
        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCapturePatientCommandHandler(IPatientRepository patients, IProcessEntityRepository processEntities, IProcessRepository processes, IUserEntityRepository userEntities, IUserRepository users)
        {
            _patients = patients;
            _processEntities = processEntities;
            _processes = processes;
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

            if (!await _userEntities.AnyByUserIdAndEntityIdAsync(user.Id, process.EntityId, ct))
            {
                throw new BusinessException("Process does not belong to this entity.")
                    .WithErrorCode("Este processo não pertence a esta entidade.");
            }

            var patient = await _patients.GetByIdOrThrowExceptionAsync(
                process.PatientId,
                command.PatientRowVersionId,
                ct);

            patient.AddressLine1 = command.AddressLine1;
            patient.AddressLine2 = command.AddressLine2;
            patient.AddressLine3 = command.AddressLine3;
            patient.PostalCode = command.PostalCode;
            patient.PostalCodeArea = command.PostalCodeArea;
            patient.EmailAddress = command.EmailAddress;
            patient.FaxNumber = command.FaxNumber;
            patient.MobileNumber = command.MobileNumber;
            patient.PhoneNumber = command.PhoneNumber;

            patient.RowVersionId = Guid.NewGuid();
            
            process.RowVersionId = Guid.NewGuid();
            process.Touch = command.Creation;

            return new ProcessCapturePatientResult(
                process.RowVersionId,
                patient.RowVersionId);
        }
    }
}