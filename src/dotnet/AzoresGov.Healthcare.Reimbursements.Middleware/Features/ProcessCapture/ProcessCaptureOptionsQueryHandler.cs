using AzoresGov.Healthcare.Reimbursements.Enumerations;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsQueryHandler : IQueryHandler<ProcessCaptureOptionsQuery, ProcessCaptureOptionsResult>
    {
        private readonly IIasConfigurationRepository _iasSettings;

        private readonly IEntityRepository _entities;

        private readonly IPatientRepository _patients;

        private readonly IPatientFamilyIncomeStatementRepository _patientFamilyIncomeStatements;

        private readonly IPatientLegalRepresentativeRepository _patientLegalRepresentatives;
        
        private readonly IProcessRepository _processes;

        private readonly IProcessPatientFamilyIncomeStatementRepository _processPatientFamilyIncomeStatements;

        private readonly IProcessPatientLegalRepresentativeRepository _processPatientLegalRepresentatives;

        private readonly IProcessPatientRepository _processPatients;

        private readonly IProcessPaymentConfigurationRepository _processPaymentSettings;

        private readonly IProcessPaymentWireTransferConfigurationRepository _processPaymentWireTransferSettings;

        private readonly IProcessConfigurationRepository _processSettings;

        private readonly IUserEntityRepository _userEntities;
        
        private readonly IUserRepository _users;

        public ProcessCaptureOptionsQueryHandler(IIasConfigurationRepository iasSettings, IEntityRepository entities, IPatientRepository patients, IPatientFamilyIncomeStatementRepository patientFamilyIncomeStatements, IPatientLegalRepresentativeRepository patientLegalRepresentatives, IProcessRepository processes, IProcessPatientFamilyIncomeStatementRepository processPatientFamilyIncomeStatements, IProcessPatientLegalRepresentativeRepository processPatientLegalRepresentatives, IProcessPatientRepository processPatients, IProcessPaymentConfigurationRepository processPaymentSettings, IProcessPaymentWireTransferConfigurationRepository processPaymentWireTransferSettings, IProcessConfigurationRepository processSettings, IUserEntityRepository userEntities, IUserRepository users)
        {
            _iasSettings = iasSettings;
            _entities = entities;
            _patients = patients;
            _patientFamilyIncomeStatements = patientFamilyIncomeStatements;
            _patientLegalRepresentatives = patientLegalRepresentatives;
            _processes = processes;
            _processPatientFamilyIncomeStatements = processPatientFamilyIncomeStatements;
            _processPatientLegalRepresentatives = processPatientLegalRepresentatives;
            _processPatients = processPatients;
            _processPaymentSettings = processPaymentSettings;
            _processPaymentWireTransferSettings = processPaymentWireTransferSettings;
            _processSettings = processSettings;
            _userEntities = userEntities;
            _users = users;
        }

        public async Task<ProcessCaptureOptionsResult> HandleQueryAsync(ProcessCaptureOptionsQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var process = await _processes.GetByPublicIdAsync(
                query.ProcessId,
                ct);

            Assert.Found(process);

            Assert.ProcessStatus(
                ProcessStatus.Capture,
                process.Status);

            await Assert.UserEntityAccessAsync(
                _userEntities,
                user.Id,
                process.EntityId,
                ct);

            var iasConfiguration = await _iasSettings.GetByYearAsync(
                query.Creation.Year,
                ct);

            Assert.Found(iasConfiguration);

            var entity = await _entities.GetByIdAsync(
                process.EntityId,
                ct);

            Assert.Found(entity);

            var parentEntity = await _entities.GetParentEntityByEntityIdAsync(
                process.EntityId,
                0,
                ct);

            var processConfiguration = await _processSettings.GetByProcessIdAsync(
                process.Id,
                ct);

            Assert.Found(processConfiguration);

            var processPatient = await _processPatients.GetByProcessIdAsync(
                process.Id,
                ct);

            Assert.Found(processPatient);

            var processPatientFamilyIncomeStatement = await _processPatientFamilyIncomeStatements.GetByProcessIdAsync(
                process.Id,
                ct);

            var processPatientLegalRepresentative = await _processPatientLegalRepresentatives.GetByProcessIdAsync(
                process.Id,
                ct);

            var processPaymentConfiguration = await _processPaymentSettings.GetByProcessIdAsync(
                process.Id,
                ct);
            
            var processPaymentWireTransferSettings = (
                processPaymentConfiguration is null || 
                processPaymentConfiguration.Method is not PaymentMethod.WireTransfer)
                
                ? null 
                : await _processPaymentWireTransferSettings.GetByProcessIdAsync(
                    process.Id,
                    ct);

            return new ProcessCaptureOptionsResult(
                new ProcessCaptureOptionsConfigurationResult(
                    processConfiguration.RowVersionId,
                    processConfiguration.MachadoJosephEnabled,
                    processConfiguration.DocumentIssueDateBypassEnabled,
                    processConfiguration.ReimbursementLimitBypassEnabled,
                    processConfiguration.UnemploymentEnabled),
                new ProcessCaptureOptionsEntityResult(
                    entity.PublicId,
                    entity.RowVersionId,
                    entity.Code,
                    entity.Name,
                    entity.Nature),
                processPatientFamilyIncomeStatement is not null
                    ? new ProcessCaptureOptionsFamilyIncomeStatementResult(
                        processPatientFamilyIncomeStatement.RowVersionId,
                        processPatientFamilyIncomeStatement.Year,
                        processPatientFamilyIncomeStatement.AccessCode,
                        processPatientFamilyIncomeStatement.FamilyMemberCount,
                        processPatientFamilyIncomeStatement.FamilyIncome)
                    : null,
                new ProcessCaptureOptionsIasConfigurationResult(
                    iasConfiguration.Year,
                    iasConfiguration.Amount),
                parentEntity is not null 
                    ? new ProcessCaptureOptionsEntityResult(
                        parentEntity.PublicId,
                        parentEntity.RowVersionId,
                        parentEntity.Code,
                        parentEntity.Name,
                        parentEntity.Nature)
                    : null,
                new ProcessCaptureOptionsPatientResult(
                    processPatient.RowVersionId,
                    processPatient.Name,
                    processPatient.Birth,
                    processPatient.Gender,
                    processPatient.HealthNumber,
                    processPatient.TaxNumber,
                    processPatient.AddressLine1,
                    processPatient.AddressLine2,
                    processPatient.AddressLine3,
                    processPatient.PostalCode,
                    processPatient.PostalCodeArea,
                    processPatient.EmailAddress,
                    processPatient.FaxNumber,
                    processPatient.MobileNumber,
                    processPatient.PhoneNumber,
                    processPatient.Death),
                processPatientLegalRepresentative is not null
                    ? new ProcessCaptureOptionsPatientLegalRepresentativeResult(
                        processPatientLegalRepresentative.RowVersionId,
                        processPatientLegalRepresentative.Name,
                        processPatientLegalRepresentative.TaxNumber,
                        processPatientLegalRepresentative.EmailAddress,
                        processPatientLegalRepresentative.FaxNumber,
                        processPatientLegalRepresentative.MobileNumber,
                        processPatientLegalRepresentative.PhoneNumber)
                    : null,
                processPaymentConfiguration is not null 
                    ? new ProcessCaptureOptionsPaymentResult(
                        processPaymentConfiguration.RowVersionId,
                        processPaymentWireTransferSettings?.RowVersionId,
                        processPaymentConfiguration.Method,
                        processPaymentConfiguration.Receiver,
                        processPaymentWireTransferSettings?.Iban,
                        processPaymentWireTransferSettings?.Swift)
                    : null,
                new ProcessCaptureOptionsProcessResult(
                    process.PublicId,
                    process.RowVersionId,
                    process.Number,
                    process.Status));
        }
    }
}