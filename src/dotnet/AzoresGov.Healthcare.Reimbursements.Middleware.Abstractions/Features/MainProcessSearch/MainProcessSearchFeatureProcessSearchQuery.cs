using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessSearchQuery : Query<MainProcessSearchFeatureProcessSearchResult>
    {
        public MainProcessSearchFeatureProcessSearchQuery(Guid userId, string? filter, bool useFullSearchCriteria, int? skip, int? take)
        {
            UserId = userId;
            Filter = filter;
            UseFullSearchCriteria = useFullSearchCriteria;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }

        public string? Filter { get; }

        public bool UseFullSearchCriteria { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}
