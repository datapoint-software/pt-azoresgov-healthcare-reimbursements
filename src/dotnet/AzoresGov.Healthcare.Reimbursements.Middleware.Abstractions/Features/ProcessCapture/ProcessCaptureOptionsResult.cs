namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCaptureOptionsResult
    {
        public ProcessCaptureOptionsResult(ProcessCaptureOptionsConfigurationResult? configuration, ProcessCaptureOptionsEntityResult entity, ProcessCaptureOptionsEntityResult? parentEntity, ProcessCaptureOptionsPatientResult patient, ProcessCaptureOptionsProcessResult process)
        {
            Configuration = configuration;
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            Process = process;
        }

        public ProcessCaptureOptionsConfigurationResult? Configuration { get; }

        public ProcessCaptureOptionsEntityResult Entity { get; } 
        
        public ProcessCaptureOptionsEntityResult? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResult Patient { get; }
        
        public ProcessCaptureOptionsProcessResult Process { get; }
    }
}