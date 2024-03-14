namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResult
    {
        public ProcessCaptureOptionsResult(ProcessCaptureOptionsConfigurationResult? configuration, ProcessCaptureOptionsEntityResult entity, ProcessCaptureOptionsEntityResult? parentEntity, ProcessCaptureOptionsPatientResult patient, ProcessCaptureOptionsPatientLegalRepresentativeResult? patientLegalRepresentative, ProcessCaptureOptionsProcessResult process)
        {
            Configuration = configuration;
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            PatientLegalRepresentative = patientLegalRepresentative;
            Process = process;
        }

        public ProcessCaptureOptionsConfigurationResult? Configuration { get; }

        public ProcessCaptureOptionsEntityResult Entity { get; } 
        
        public ProcessCaptureOptionsEntityResult? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResult Patient { get; }
        
        public ProcessCaptureOptionsPatientLegalRepresentativeResult? PatientLegalRepresentative { get; }
        
        public ProcessCaptureOptionsProcessResult Process { get; }
    }
}