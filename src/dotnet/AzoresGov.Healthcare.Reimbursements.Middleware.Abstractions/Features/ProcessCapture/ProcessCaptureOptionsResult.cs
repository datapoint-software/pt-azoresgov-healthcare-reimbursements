namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResult
    {
        public ProcessCaptureOptionsResult(ProcessCaptureOptionsConfigurationResult? configuration, ProcessCaptureOptionsEntityResult entity, ProcessCaptureOptionsFamilyIncomeStatementResult? familyIncomeStatement, ProcessCaptureOptionsEntityResult? parentEntity, ProcessCaptureOptionsPatientResult patient, ProcessCaptureOptionsPatientLegalRepresentativeResult? patientLegalRepresentative, ProcessCaptureOptionsProcessResult process)
        {
            Configuration = configuration;
            Entity = entity;
            FamilyIncomeStatement = familyIncomeStatement;
            ParentEntity = parentEntity;
            Patient = patient;
            PatientLegalRepresentative = patientLegalRepresentative;
            Process = process;
        }

        public ProcessCaptureOptionsConfigurationResult? Configuration { get; }

        public ProcessCaptureOptionsEntityResult Entity { get; } 
        
        public ProcessCaptureOptionsFamilyIncomeStatementResult? FamilyIncomeStatement { get; }
        
        public ProcessCaptureOptionsEntityResult? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResult Patient { get; }
        
        public ProcessCaptureOptionsPatientLegalRepresentativeResult? PatientLegalRepresentative { get; }
        
        public ProcessCaptureOptionsProcessResult Process { get; }
    }
}