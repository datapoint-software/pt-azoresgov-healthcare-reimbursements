namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResultModel
    {
        public ProcessCaptureOptionsResultModel(ProcessCaptureOptionsConfigurationResultModel? configuration, ProcessCaptureOptionsEntityResultModel entity, ProcessCaptureOptionsFamilyIncomeStatementResultModel? familyIncomeStatement, ProcessCaptureOptionsIasConfigurationResultModel iasConfiguration, ProcessCaptureOptionsEntityResultModel? parentEntity, ProcessCaptureOptionsPatientResultModel patient, ProcessCaptureOptionsPatientLegalRepresentativeResultModel? patientLegalRepresentative, ProcessCaptureOptionsPaymentResultModel? payment, ProcessCaptureOptionsProcessResultModel process)
        {
            Configuration = configuration;
            Entity = entity;
            FamilyIncomeStatement = familyIncomeStatement;
            IasConfiguration = iasConfiguration;
            ParentEntity = parentEntity;
            Patient = patient;
            PatientLegalRepresentative = patientLegalRepresentative;
            Payment = payment;
            Process = process;
        }

        public ProcessCaptureOptionsConfigurationResultModel? Configuration { get; }

        public ProcessCaptureOptionsEntityResultModel Entity { get; } 
        
        public ProcessCaptureOptionsFamilyIncomeStatementResultModel? FamilyIncomeStatement { get; }

        public ProcessCaptureOptionsIasConfigurationResultModel IasConfiguration { get; }

        public ProcessCaptureOptionsEntityResultModel? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResultModel Patient { get; }
        
        public ProcessCaptureOptionsPatientLegalRepresentativeResultModel? PatientLegalRepresentative { get; }
        
        public ProcessCaptureOptionsPaymentResultModel? Payment { get; }
        
        public ProcessCaptureOptionsProcessResultModel Process { get; }
    }
}