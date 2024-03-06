namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCaptureOptionsResult
    {
        public ProcessCaptureOptionsResult(ProcessCaptureOptionsEntityResult entity, ProcessCaptureOptionsEntityResult? parentEntity, ProcessCaptureOptionsPatientResult patient, ProcessCaptureOptionsProcessResult process)
        {
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            Process = process;
        }

        public ProcessCaptureOptionsEntityResult Entity { get; } 
        
        public ProcessCaptureOptionsEntityResult? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResult Patient { get; }
        
        public ProcessCaptureOptionsProcessResult Process { get; }
    }
}