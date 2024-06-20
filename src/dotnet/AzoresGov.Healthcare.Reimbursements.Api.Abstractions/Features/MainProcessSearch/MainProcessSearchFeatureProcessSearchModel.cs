using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessSearch
{
    public sealed class MainProcessSearchFeatureProcessSearchModel
    {
        public MainProcessSearchFeatureProcessSearchModel(string? filter, bool useFullSearchCriteria, int? skip, int? take)
        {
            Filter = filter;
            UseFullSearchCriteria = useFullSearchCriteria;
            Skip = skip;
            Take = take;
        }

        public string? Filter { get; }

        public bool UseFullSearchCriteria { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}
