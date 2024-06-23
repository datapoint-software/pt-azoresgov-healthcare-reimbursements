using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureOptionsResultModel
    {
        public MainProcessCaptureFeatureOptionsResultModel(IReadOnlyCollection<MainProcessCaptureFeatureEntityModel> entities, MainProcessCaptureFeaturePatientModel patient, MainProcessCaptureFeatureProcessModel process)
        {
            Entities = entities;
            Patient = patient;
            Process = process;
        }

        public IReadOnlyCollection<MainProcessCaptureFeatureEntityModel> Entities { get; }

        public MainProcessCaptureFeaturePatientModel Patient { get; }

        public MainProcessCaptureFeatureProcessModel Process { get; }
    }
}