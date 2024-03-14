namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResultModel
    {
        public ProcessCaptureOptionsResultModel(ProcessCaptureOptionsConfigurationResultModel? configuration, ProcessCaptureOptionsEntityResultModel entity, ProcessCaptureOptionsEntityResultModel? parentEntity, ProcessCaptureOptionsPatientResultModel patient, ProcessCaptureOptionsPatientLegalRepresentativeResultModel? patientLegalRepresentative, ProcessCaptureOptionsProcessResultModel process)
        {
            Configuration = configuration;
            Entity = entity;
            ParentEntity = parentEntity;
            Patient = patient;
            PatientLegalRepresentative = patientLegalRepresentative;
            Process = process;
        }

        public ProcessCaptureOptionsConfigurationResultModel? Configuration { get; }

        public ProcessCaptureOptionsEntityResultModel Entity { get; } 
        
        public ProcessCaptureOptionsEntityResultModel? ParentEntity { get; }
        
        public ProcessCaptureOptionsPatientResultModel Patient { get; }
        
        public ProcessCaptureOptionsPatientLegalRepresentativeResultModel? PatientLegalRepresentative { get; }
        
        public ProcessCaptureOptionsProcessResultModel Process { get; }
    }
}