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
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSubmitCommandHandler : ICommandHandler<MainProcessCaptureFeatureLegalRepresentativeSubmitCommand, MainProcessCaptureFeatureLegalRepresentativeSubmitResult>
    {
        private readonly ILegalRepresentativeRepository _legalRepresentatives;

        private readonly IPatientRepository _patients;

        private readonly IProcessRepository _processes;

        private readonly IUserRepository _users;

        public MainProcessCaptureFeatureLegalRepresentativeSubmitCommandHandler(ILegalRepresentativeRepository legalRepresentatives, IPatientRepository patients, IProcessRepository processes, IUserRepository users)
        {
            _legalRepresentatives = legalRepresentatives;
            _patients = patients;
            _processes = processes;
            _users = users;
        }

        public async Task<MainProcessCaptureFeatureLegalRepresentativeSubmitResult> HandleCommandAsync(
            MainProcessCaptureFeatureLegalRepresentativeSubmitCommand command, 
            CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                command.UserId,
                ct);

            Assert.Found(user);

            var process = await _processes.GetByPublicIdAsync(
                command.ProcessId,
                ct);

            Assert.Found(process, command.ProcessRowVersionId);

            var patient = await _patients.GetByIdAsync(
                process.PatientId,
                ct);

            Assert.Found(patient, command.PatientRowVersionId);

            var legalRepresentative = command.LegalRepresentativeId.HasValue
                ? (await _legalRepresentatives.GetByPublicIdAsync(command.LegalRepresentativeId.Value, ct))
                : null;

            if (command.LegalRepresentativeId.HasValue)
                Assert.Found(legalRepresentative, command.LegalRepresentativeRowVersionId!.Value);

            legalRepresentative ??= _legalRepresentatives.Add(new());

            if (command.LegalRepresentativeId is null)
            {
                legalRepresentative.PublicId = Guid.NewGuid();
                legalRepresentative.TaxNumber = command.TaxNumber!;
                legalRepresentative.Name = command.Name!;
            }

            legalRepresentative.FaxNumber = command.FaxNumber;
            legalRepresentative.MobileNumber = command.MobileNumber;
            legalRepresentative.PhoneNumber = command.PhoneNumber;
            legalRepresentative.EmailAddress = command.EmailAddress;
            legalRepresentative.PostalAddressArea = command.PostalAddressArea;
            legalRepresentative.PostalAddressAreaCode = command.PostalAddressAreaCode;
            legalRepresentative.PostalAddressLine1 = command.PostalAddressLine1;
            legalRepresentative.PostalAddressLine2 = command.PostalAddressLine2;
            legalRepresentative.PostalAddressLine3 = command.PostalAddressLine3;

            patient.LegalRepresentative = legalRepresentative;
            process.LegalRepresentative = legalRepresentative;

            patient.RowVersionId = Guid.NewGuid();
            process.RowVersionId = Guid.NewGuid();
            legalRepresentative.RowVersionId = Guid.NewGuid();

            return new MainProcessCaptureFeatureLegalRepresentativeSubmitResult(
                process.RowVersionId,
                patient.RowVersionId,
                command.LegalRepresentativeId.HasValue
                    ? null
                    : legalRepresentative.PublicId,
                legalRepresentative.RowVersionId);
        }
    }
}
