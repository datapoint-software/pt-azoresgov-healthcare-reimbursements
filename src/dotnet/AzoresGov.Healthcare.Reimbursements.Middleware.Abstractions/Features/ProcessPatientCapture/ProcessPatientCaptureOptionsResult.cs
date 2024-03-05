namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessPatientCaptureOptionsResult
    {
        public ProcessPatientCaptureOptionsResult(ProcessPatientCaptureOptionsEntityResult entity, ProcessPatientCaptureOptionsEntityResult? parentEntity, ProcessPatientCaptureOptionsPatientResult patient, ProcessPatientCaptureOptionsProcessResult process)
        {
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            Process = process;
        }

        public ProcessPatientCaptureOptionsEntityResult Entity { get; } 
        
        public ProcessPatientCaptureOptionsEntityResult? ParentEntity { get; }
        
        public ProcessPatientCaptureOptionsPatientResult Patient { get; }
        
        public ProcessPatientCaptureOptionsProcessResult Process { get; }
    }
}