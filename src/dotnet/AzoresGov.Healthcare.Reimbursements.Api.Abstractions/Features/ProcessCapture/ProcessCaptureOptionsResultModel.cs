namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResultModel
    {
        public ProcessCaptureOptionsResultModel(ProcessCaptureOptionsConfigurationResultModel? configuration, ProcessCaptureOptionsEntityResultModel entity, ProcessCaptureOptionsEntityResultModel? parentEntity, ProcessCaptureOptionsPatientResultModel patient, ProcessCaptureOptionsProcessResultModel process)
        {
            Configuration = configuration;
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            Process = process;
        }
        
        public ProcessCaptureOptionsConfigurationResultModel? Configuration { get; }

        public ProcessCaptureOptionsEntityResultModel Entity { get; } 
        
        public ProcessCaptureOptionsEntityResultModel? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResultModel Patient { get; }
        
        public ProcessCaptureOptionsProcessResultModel Process { get; }
    }
}