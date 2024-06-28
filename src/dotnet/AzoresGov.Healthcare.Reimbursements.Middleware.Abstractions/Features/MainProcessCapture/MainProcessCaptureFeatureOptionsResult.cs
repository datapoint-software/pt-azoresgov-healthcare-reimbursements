using System.Collections.Generic;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureOptionsResult
    {
        public MainProcessCaptureFeatureOptionsResult(IReadOnlyCollection<MainProcessCaptureFeatureEntity> entities, MainProcessCaptureFeaturePatient patient, MainProcessCaptureFeatureProcess process, MainProcessCaptureFeatureLegalRepresentative? legalRepresentative)
        {
            Entities = entities;
            Patient = patient;
            Process = process;
            LegalRepresentative = legalRepresentative;
        }

        public IReadOnlyCollection<MainProcessCaptureFeatureEntity> Entities { get; }

        public MainProcessCaptureFeaturePatient Patient { get; }

        public MainProcessCaptureFeatureProcess Process { get; }

        public MainProcessCaptureFeatureLegalRepresentative? LegalRepresentative { get; }
    }
}