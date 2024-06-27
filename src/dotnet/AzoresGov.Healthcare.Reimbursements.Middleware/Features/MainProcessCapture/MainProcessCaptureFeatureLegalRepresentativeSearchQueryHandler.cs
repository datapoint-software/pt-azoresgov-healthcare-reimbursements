using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCapture
{
    public sealed class MainProcessCaptureFeatureLegalRepresentativeSearchQueryHandler : IQueryHandler<MainProcessCaptureFeatureLegalRepresentativeSearchQuery, MainProcessCaptureFeatureLegalRepresentativeSearchResult>
    {
        private static readonly MainProcessCaptureFeatureLegalRepresentativeSearchResult EmptyResult = new MainProcessCaptureFeatureLegalRepresentativeSearchResult(null);

        private readonly ILegalRepresentativeRepository _legalRepresentatives;

        private readonly IUserRepository _users;

        public MainProcessCaptureFeatureLegalRepresentativeSearchQueryHandler(ILegalRepresentativeRepository legalRepresentatives, IUserRepository users)
        {
            _legalRepresentatives = legalRepresentatives;
            _users = users;
        }

        public async Task<MainProcessCaptureFeatureLegalRepresentativeSearchResult> HandleQueryAsync(MainProcessCaptureFeatureLegalRepresentativeSearchQuery query, CancellationToken ct)
        {
            var user = await _users.GetByPublicIdAsync(
                query.UserId,
                ct);

            Assert.Found(user);

            var legalRepresentative = await _legalRepresentatives.GetByTaxNumberAsync(
                query.TaxNumber,
                ct);

            if (legalRepresentative is null)
                return EmptyResult;

            return new MainProcessCaptureFeatureLegalRepresentativeSearchResult(
                new MainProcessCaptureFeatureLegalRepresentative(
                    legalRepresentative.PublicId,
                    legalRepresentative.RowVersionId,
                    legalRepresentative.TaxNumber,
                    legalRepresentative.Name,
                    legalRepresentative.FaxNumber,
                    legalRepresentative.MobileNumber,
                    legalRepresentative.PhoneNumber,
                    legalRepresentative.EmailAddress,
                    legalRepresentative.PostalAddressArea,
                    legalRepresentative.PostalAddressAreaCode,
                    legalRepresentative.PostalAddressLine1,
                    legalRepresentative.PostalAddressLine2,
                    legalRepresentative.PostalAddressLine3));
        }
    }
}
