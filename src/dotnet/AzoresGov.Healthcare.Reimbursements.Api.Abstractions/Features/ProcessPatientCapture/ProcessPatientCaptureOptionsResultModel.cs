namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessPatientCapture
{
    public sealed class ProcessPatientCaptureOptionsResultModel
    {
        public ProcessPatientCaptureOptionsResultModel(ProcessPatientCaptureOptionsEntityResultModel entity, ProcessPatientCaptureOptionsEntityResultModel? parentEntity, ProcessPatientCaptureOptionsPatientResultModel patient, ProcessPatientCaptureOptionsProcessResultModel process)
        {
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            Process = process;
        }

        public ProcessPatientCaptureOptionsEntityResultModel Entity { get; } 
        
        public ProcessPatientCaptureOptionsEntityResultModel? ParentEntity { get; }
        
        public ProcessPatientCaptureOptionsPatientResultModel Patient { get; }
        
        public ProcessPatientCaptureOptionsProcessResultModel Process { get; }
    }
}