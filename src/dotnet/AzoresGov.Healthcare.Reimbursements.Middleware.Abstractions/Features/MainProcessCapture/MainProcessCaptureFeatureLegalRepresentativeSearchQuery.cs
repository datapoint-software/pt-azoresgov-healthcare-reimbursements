using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSearchQuery : Query<MainProcessCaptureFeatureLegalRepresentativeSearchResult>
    {
        public MainProcessCaptureFeatureLegalRepresentativeSearchQuery(Guid userId, string taxNumber)
        {
            UserId = userId;
            TaxNumber = taxNumber;
        }

        public Guid UserId { get; }

        public string TaxNumber { get; }
    }
}
