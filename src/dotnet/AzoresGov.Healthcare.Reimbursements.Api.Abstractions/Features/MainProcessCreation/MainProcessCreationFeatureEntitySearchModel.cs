namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureEntitySearchModel
    {
        public MainProcessCreationFeatureEntitySearchModel(string filter, int? skip, int? take)
        {
            Filter = filter;
            Skip = skip;
            Take = take;
        }

        public string Filter { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}
