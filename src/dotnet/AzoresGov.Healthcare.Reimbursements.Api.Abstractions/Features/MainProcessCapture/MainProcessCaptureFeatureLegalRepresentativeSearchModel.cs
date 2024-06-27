using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSearchModel
    {
        public MainProcessCaptureFeatureLegalRepresentativeSearchModel(string taxNumber)
        {
            TaxNumber = taxNumber;
        }

        public string TaxNumber { get; }
    }
}
