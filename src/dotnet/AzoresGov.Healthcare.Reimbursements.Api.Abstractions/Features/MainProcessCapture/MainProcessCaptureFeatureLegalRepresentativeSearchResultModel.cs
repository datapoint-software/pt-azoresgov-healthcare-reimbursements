using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSearchResultModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeSearchResultModel(MainProcessCaptureFeatureLegalRepresentativeModel? legalRepresentative)
        {
            LegalRepresentative = legalRepresentative;
        }

        public MainProcessCaptureFeatureLegalRepresentativeModel? LegalRepresentative { get; }
    }
}