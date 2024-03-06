namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResultModel
    {
        public ProcessCaptureOptionsResultModel(ProcessCaptureOptionsEntityResultModel entity, ProcessCaptureOptionsEntityResultModel? parentEntity, ProcessCaptureOptionsPatientResultModel patient, ProcessCaptureOptionsProcessResultModel process)
        {
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            Process = process;
        }

        public ProcessCaptureOptionsEntityResultModel Entity { get; } 
        
        public ProcessCaptureOptionsEntityResultModel? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResultModel Patient { get; }
        
        public ProcessCaptureOptionsProcessResultModel Process { get; }
    }
}