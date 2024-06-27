using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSearchResult
    {
        public MainProcessCaptureFeatureLegalRepresentativeSearchResult(MainProcessCaptureFeatureLegalRepresentative? legalRepresentative)
        {
            LegalRepresentative = legalRepresentative;
        }

        public MainProcessCaptureFeatureLegalRepresentative? LegalRepresentative { get; }
    }
}